using Microsoft.AspNetCore.Mvc;
using ReservaBL;
using ReservaEN;

namespace ReservaUI.Controllers
{
    public class NumeroDeMesaController : Controller
    {
        public IActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var lista = NumeroDeMesaBL.MostrarNumeroDeMesa();
            if (lista == null)
                lista = new List<NumeroDeMesaEN>();

            return View("Index", lista);
        }

        [HttpGet]
        public IActionResult GuardarNumeroDeMesa()
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GuardarNumeroDeMesa(NumeroDeMesaEN numeroDeMesaEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                // ✅ Validar duplicados por nombre
                var lista = NumeroDeMesaBL.MostrarNumeroDeMesa();
                bool existe = lista.Any(m => m.Nombre.ToLower().Trim() == numeroDeMesaEN.Nombre.ToLower().Trim());

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "El número de mesa que intentas guardar ya existe.";
                    return RedirectToAction(nameof(Index));
                }

                NumeroDeMesaBL.GuardarNumeroDeMesa(numeroDeMesaEN);
                TempData["ExitoGuardar"] = "Número de mesa guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(numeroDeMesaEN);
        }

        [HttpGet]
        public IActionResult ModificarNumeroDeMesa(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var numeroMesa = NumeroDeMesaBL.MostrarNumeroDeMesa().FirstOrDefault(m => m.Id == Id);
            if (numeroMesa == null) return NotFound();
            return View(numeroMesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarNumeroDeMesa(NumeroDeMesaEN numeroDeMesaEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                var lista = NumeroDeMesaBL.MostrarNumeroDeMesa();
                bool existe = lista.Any(m =>
                    m.Nombre.ToLower().Trim() == numeroDeMesaEN.Nombre.ToLower().Trim()
                    && m.Id != numeroDeMesaEN.Id);

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "El número de mesa que intentas modificar ya existe.";
                    return RedirectToAction(nameof(Index));
                }

                NumeroDeMesaBL.ModificarNumeroDeMesa(numeroDeMesaEN);
                TempData["ExitoModificar"] = "Número de mesa modificado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(numeroDeMesaEN);
        }

        [HttpGet]
        public IActionResult EliminarNumeroDeMesa(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var numeroMesa = NumeroDeMesaBL.MostrarNumeroDeMesa().FirstOrDefault(m => m.Id == Id);
            if (numeroMesa == null) return NotFound();
            return View(numeroMesa);
        }

        [HttpPost, ActionName("EliminarNumeroDeMesa")]
        public IActionResult EliminarNumeroDeMesaConfirmado(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            NumeroDeMesaBL.EliminarNumeroDeMesa(Id);
            TempData["ExitoEliminar"] = "Número de mesa eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
