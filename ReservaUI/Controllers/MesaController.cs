using Microsoft.AspNetCore.Mvc;
using ReservaBL;
using ReservaEN;

namespace ReservaUI.Controllers
{
    public class MesaController : Controller
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
            else
            {
                var lista = MesaBL.MostrarMesa();
                if (lista == null)
                    lista = new List<MesaEN>();

                return View("Index", lista);
            }
        }

        [HttpGet]
        public IActionResult GuardarMesa()
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
        public IActionResult GuardarMesa(MesaEN mesaEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                // ✅ Validar duplicados (por nombre)
                var listaMesa = MesaBL.MostrarMesa();
                bool existe = listaMesa.Any(m => m.Nombre.ToLower().Trim() == mesaEN.Nombre.ToLower().Trim());

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "La mesa que intentas guardar ya existe.";
                    return RedirectToAction(nameof(Index));
                }

                MesaBL.GuardarMesa(mesaEN);
                TempData["ExitoGuardar"] = "Mesa guardada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(mesaEN);
        }

        [HttpGet]
        public IActionResult ModificarMesa(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var mesa = MesaBL.MostrarMesa().FirstOrDefault(m => m.Id == Id);
                if (mesa == null) return NotFound();
                return View(mesa);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarMesa(MesaEN mesaEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                var listaMesa = MesaBL.MostrarMesa();
                bool existe = listaMesa.Any(m =>
                    m.Nombre.ToLower().Trim() == mesaEN.Nombre.ToLower().Trim()
                    && m.Id != mesaEN.Id);

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "La mesa que intentas modificar ya existe.";
                    return RedirectToAction(nameof(Index));
                }

                MesaBL.ModificarMesa(mesaEN);
                TempData["ExitoModificar"] = "Mesa modificada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(mesaEN);
        }

        [HttpGet]
        public IActionResult EliminarMesa(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var mesa = MesaBL.MostrarMesa().FirstOrDefault(m => m.Id == Id);
                if (mesa == null) return NotFound();
                return View(mesa);
            }
        }

        [HttpPost, ActionName("EliminarMesa")]
        public IActionResult EliminarMesaConfirmado(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            MesaBL.EliminarMesa(Id);
            TempData["ExitoEliminar"] = "Mesa eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}

