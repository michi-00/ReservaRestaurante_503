using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservaBL;
using ReservaEN;

namespace ReservaUI.Controllers
{
    public class ReservacionController : Controller
    {

        public IActionResult MostrarReservacion()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var reservacionBL = new ReservacionBL();
                var lista = ReservacionBL.MostrarReservacion();

                if (lista == null)
                    lista = new List<ReservacionEN>();


                return View("MostrarReservacion", lista);
            }
        }
        public IActionResult MostrarMesasU()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            var listaMesas = MesaBL.MostrarMesa();
            if (listaMesas == null)
                listaMesas = new List<MesaEN>();

            return View("MostrarMesasU", listaMesas);
        }

        // GET: Seleccionar número de mesa disponible para una mesa específica
        [HttpGet]
        public IActionResult SeleccionarNumeroDeMesa(int idMesa)
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            var todosNumeros = NumeroDeMesaBL.MostrarNumeroDeMesa();

            // Obtener los números de mesa ya reservados
            var reservados = ReservacionBL.MostrarReservacion()
                              .Select(r => r.IdNumeroDeMesa)
                              .ToList();

            // Filtrar los disponibles
            var disponibles = todosNumeros
                              .Where(n => !reservados.Contains(n.Id))
                              .ToList();

            ViewBag.IdMesa = idMesa; // Para enviar el idMesa al formulario
            return View("SeleccionarNumeroDeMesa", disponibles);
        }

        // GET: Confirmar reservación
        [HttpGet]
        public IActionResult ConfirmarReservacion(int idMesa, int idNumeroDeMesa)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
            {
                TempData["Error"] = "Debes iniciar sesión para reservar.";
                return RedirectToAction("Login", "Login");
            }

            var mesa = MesaBL.ObtenerMesaPorId(idMesa);
            var numeroMesa = NumeroDeMesaBL.ObtenerNumeroDeMesaPorId(idNumeroDeMesa);
            var usuario = UsuarioBL.ObtenerUsuarioPorId(idUsuario.Value);

            if (mesa == null || numeroMesa == null || usuario == null)
            {
                TempData["Error"] = "La mesa, el número de mesa o el usuario no existen.";
                return RedirectToAction("MostrarMesasU");
            }

            var reservacionEN = new ReservacionEN
            {
                IdMesa = mesa.Id,
                Nombre_Mesa = mesa.Nombre,
                IdNumeroDeMesa = numeroMesa.Id,
                Nombre_NumeroDeMesa = numeroMesa.Nombre,
                IdUsuario = usuario.Id,
                Nombre_Usuario = usuario.Nombre,
                Apellido_Usuario = usuario.Apellido,
                Numero_Celular = usuario.Celular,
                FechaDeReservacion = DateTime.Now
            };

            return View("ConfirmarReservacion", reservacionEN);
        }

        // POST: Guardar reservación confirmada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuardarReservacionConfirmada(ReservacionEN reservacionEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
            {
                TempData["Error"] = "Debes iniciar sesión para reservar.";
                return RedirectToAction("Login", "Login");
            }

            // Validar si ya existe reservación para este número de mesa por este usuario
            var listaReservaciones = ReservacionBL.MostrarReservacion();
            var listadenumero = ReservacionBL.MostrarReservacion();
            bool existe = listaReservaciones.Any(r => r.IdUsuario == reservacionEN.IdUsuario);
               
            bool numeroexiste = listadenumero.Any(n => n.IdNumeroDeMesa ==reservacionEN.IdNumeroDeMesa);

            if (existe||numeroexiste)
            {
                TempData["YaTieneReservacion"] = "Ya tienes una reservación .";
                return RedirectToAction("MostrarMesasU");
            }
           

                ReservacionBL.GuardarReservacion(reservacionEN);
            TempData["Mensaje"] = "¡Reservación realizada con éxito!";
            return RedirectToAction("MostrarMesasU");
        }

        // GET: Mostrar todas las reservaciones de un usuario
        public IActionResult MisReservaciones()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
                return RedirectToAction("Login", "Login");

            var lista = ReservacionBL.MostrarReservacion()
                                     .Where(r => r.IdUsuario == idUsuario.Value)
                                     .ToList();

            return View("MisReservaciones", lista);
        }
        [HttpGet]
        public IActionResult ModificarReservacion(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var reserva = ReservacionBL.MostrarReservacion().FirstOrDefault(r => r.Id == Id);
            if (reserva == null) return NotFound();
            var mesaBL = MesaBL.MostrarMesa();
            ViewBag.Mesas = new SelectList(mesaBL, "Id", "Nombre", reserva.IdMesa);
            var numerodemesa = NumeroDeMesaBL.MostrarNumeroDeMesa();
            ViewBag.Numeros = new SelectList(numerodemesa, "Id", "Nombre", reserva.IdNumeroDeMesa);
          
            return View(reserva);
        }

        // POST: Tarea/ModificarTarea/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarReservacion(ReservacionEN reservacionEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                var reserva = ReservacionBL.MostrarReservacion();
                bool existe = reserva.Any(e =>
                    e.IdUsuario.ToString().Trim() == reservacionEN.IdUsuario.ToString().Trim()
                    && e.Id != reservacionEN.Id); //  evitar que choque con su propio nombre

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "Este usuario  ya tiene una Reservacion.";
                    return RedirectToAction(nameof(MostrarReservacion));
                }


                ReservacionBL.ModificarReservacion(reservacionEN);
                var mesaBL = MesaBL.MostrarMesa();
                ViewBag.Mesas = new SelectList(mesaBL, "Id", "Nombre", reservacionEN.IdMesa);
                var numerodemesa = NumeroDeMesaBL.MostrarNumeroDeMesa();
                ViewBag.Numeros = new SelectList(numerodemesa, "Id", "Nombre", reservacionEN.IdNumeroDeMesa);
               

                TempData["ExitoModificar"] = "Reservacion modificada correctamente.";
                return RedirectToAction(nameof(MostrarReservacion));


            }

            return View(reservacionEN);
        }
        [HttpGet]
        public IActionResult EliminarReservacion(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var reservacion = ReservacionBL.MostrarReservacion().FirstOrDefault(r => r.Id == Id);
            if (reservacion == null) return NotFound();
            return View(reservacion);
        }
        [HttpPost, ActionName("EliminarReservacion")]
        public IActionResult EliminarReservacionConfirmado(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            ReservacionBL.EliminarReservacion(Id);
            TempData["ExitoEliminar"] = "Reservacion eliminada correctamente.";
            return RedirectToAction(nameof(MostrarReservacion));
        }
    }
}
