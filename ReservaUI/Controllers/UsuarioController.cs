using Microsoft.AspNetCore.Mvc;
using ReservaBL;
using ReservaEN;

namespace ReservaUI.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: Usuarios
        public IActionResult MostrarUsuario()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var lista = UsuarioBL.MostrarUsuario();
            return View("MostrarUsuario", lista); // Vista: MostrarUsuarios.cshtml
        }

        // GET: Usuarios/GuardarUsuario
        public IActionResult GuardarUsuario()
        {

            return View("GuardarUsuario"); // Vista: GuardarUsuario.cshtml
        }

        // POST: Usuarios/GuardarUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuardarUsuario(UsuarioEN pusuarioEN)
        {
            if (ModelState.IsValid)
            {
                //  Validar si ya existe un estado con el mismo nombre
                var listaU = UsuarioBL.MostrarUsuario();
                bool existeC = listaU.Any(u => u.Cuenta.ToLower().Trim() == pusuarioEN.Cuenta.ToLower().Trim());

                if (existeC)
                {
                    TempData["ErrorDuplicado"] = "La cuenta ingresada ya esta siendo utilizada por  alguien mas.";
                    return RedirectToAction(nameof(GuardarUsuario));
                }
                UsuarioBL.GuardarUsuario(pusuarioEN);
                TempData["ExitoGuardar"] = "Tu usuario sea registrado correctamente";
            }
            return View("GuardarUsuario", pusuarioEN);
        }

        // GET: Usuarios/ModificarUsuario/5
        public IActionResult ModificarUsuario(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var pusuarioEN = UsuarioBL.MostrarUsuario().FirstOrDefault(u => u.Id == id);
            if (pusuarioEN == null) return NotFound();
            return View("ModificarUsuario", pusuarioEN); // Vista: ModificarUsuario.cshtml
        }

        // POST: Usuarios/ModificarUsuario/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificarUsuario(UsuarioEN pusuarioEN)
        {

            if (ModelState.IsValid)
            {

                var listaU = UsuarioBL.MostrarUsuario();
                bool existe = listaU.Any(u => u.Cuenta.ToLower().Trim() == pusuarioEN.Cuenta.ToLower().Trim()
                && u.Id != pusuarioEN.Id); // evitar que choque con su propio nombre

                bool existeN = listaU.Any(c => c.Celular.ToLower().Trim() == pusuarioEN.Celular.ToLower().Trim()
                 && c.Id != pusuarioEN.Id); // evitar que choque con su propio celular
                if (existe || existeN)
                {
                    TempData["ErrorDuplicadoModificado"] = "Algunos datos que intentas guardar ya existen.";
                    return RedirectToAction(nameof(MostrarUsuario));
                }
                TempData["ExitoModificar"] = "Usuario Modificado correctamente";
                UsuarioBL.ModificarUsuario(pusuarioEN);
                return RedirectToAction(nameof(MostrarUsuario));

            }
            return View("ModificarUsuario", pusuarioEN);
        }

        [HttpGet]
        public IActionResult EliminarUsuario(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var usuario = UsuarioBL.MostrarUsuario().FirstOrDefault(u => u.Id == Id);

            if (usuario == null) return NotFound();
            return View(usuario);
        }
        [HttpPost, ActionName("EliminarUsuario")]
        public IActionResult EliminarUsuarioConfirmado(int Id)
        {
            Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            var idUsuarioLogueado = HttpContext.Session.GetInt32("IdUsuario");

            // ✅ Validar si intenta eliminarse a sí mismo
            if (idUsuarioLogueado != null && Id == idUsuarioLogueado)
            {
                TempData["ErrorEliminar"] = "No puedes eliminar el usuario con el que has iniciado sesión.";
                return RedirectToAction(nameof(MostrarUsuario));
            }

            UsuarioBL.EliminarUsuario(Id);
            TempData["ExitoEliminar"] = "Usuario eliminado correctamente.";
            return RedirectToAction(nameof(MostrarUsuario));
        }
       
    }
}

