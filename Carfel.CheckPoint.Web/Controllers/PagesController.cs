using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfel.CheckPoint.Web.Controllers {
    public class PagesController : Controller {
        [HttpGet]
        public ActionResult Login () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.Tipo = HttpContext.Session.GetString ("Tipo");
            return View ();
        }

        [HttpGet]
        public ActionResult Sobre () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.Tipo = HttpContext.Session.GetString ("Tipo");
            return View ();
        }

        [HttpGet]
        public ActionResult Home () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioEmail");
            ViewBag.Tipo = HttpContext.Session.GetString ("Tipo");
            return View ();
        }
    }
}