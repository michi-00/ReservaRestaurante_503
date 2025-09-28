using Microsoft.AspNetCore.Mvc;
using ReservaBL;
using ReservaEN;

namespace ReservaUI.Controllers
{
    public class RolController : Controller
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
                var rolBL = new RolBL();
                var lista = RolBL.MostrarRol();
                if (lista == null)
                    lista = new List<RolEN>();

                return View("Index", lista);
            }
        }

        [HttpGet]
        public IActionResult GuardarRol()
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
        public IActionResult GuardarRol(RolEN rolEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                //  Validar si ya existe un estado con el mismo nombre
                var listaRol = RolBL.MostrarRol();
                bool existe = listaRol.Any(r => r.Nombre.ToLower().Trim() == rolEN.Nombre.ToLower().Trim());

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "El rol que intentas guardar ya existe.";
                    return RedirectToAction(nameof(Index));
                }
                RolBL.GuardarRol(rolEN);
                TempData["ExitoGuardar"] = "Rol guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(rolEN);
        }
        [HttpGet]
        public IActionResult ModificarRol(int Id)
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
                var rol = RolBL.MostrarRol().FirstOrDefault(r => r.Id == Id);
                if (rol == null) return NotFound();
                return View(rol);
            }
        }

        // POST: Tarea/ModificarTarea/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarRol(RolEN rolEN)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            if (ModelState.IsValid)
            {
                //  Validar si ya existe un estado con el mismo nombre
                var listaRol = RolBL.MostrarRol();
                bool existe = listaRol.Any(r =>
                r.Nombre.ToLower().Trim() == rolEN.Nombre.ToLower().Trim()
                && r.Id != rolEN.Id); //  evitar que choque con su propio nombre

                if (existe)
                {
                    TempData["ErrorDuplicado"] = "El rol que intentas modificar ya existe.";
                    return RedirectToAction(nameof(Index));
                }
                RolBL.ModificarRol(rolEN);
                TempData["ExitoModificar"] = "Rol modificado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(rolEN);
        }
        [HttpGet]
        public IActionResult EliminarRol(int Id)
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
                var rol = RolBL.MostrarRol().FirstOrDefault(r => r.Id == Id);
                if (rol == null) return NotFound();
                return View(rol);
            }
        }
        [HttpPost, ActionName("EliminarRol")]
        public IActionResult EliminarRolConfirmado(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            RolBL.EliminarRol(Id);
            TempData["ExitoEliminar"] = "Rol eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }


    }
}

