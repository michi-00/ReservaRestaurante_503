using Microsoft.AspNetCore.Mvc;

namespace ReservaUI.Controllers
{
    public class InicioUsuarioController : Controller
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
            return View();
        }
    }
}
