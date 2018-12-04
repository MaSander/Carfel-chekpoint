using Carfel.CheckPoint.Web.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfel.CheckPoint.Web.Controllers {
    public class PagesController : Controller {
        [HttpGet]
        public ActionResult Login () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.UsuarioTipo = HttpContext.Session.GetString ("UsuarioTipo");
            return View ();
        }

        [HttpGet]
        public ActionResult Sobre () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.UsuarioTipo = HttpContext.Session.GetString ("UsuarioTipo");
            return View ();
        }

        [HttpGet]
        public ActionResult Home () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioNome");
            ViewBag.UsuarioNome = HttpContext.Session.GetString ("UsuarioEmail");
            ViewBag.UsuarioTipo = HttpContext.Session.GetString ("UsuarioTipo");

            ComentarioRepositorioSerializado comentarioRepositorio = new ComentarioRepositorioSerializado ();

            ViewData["Comentarios"] = comentarioRepositorio.Listar ();

            return View ();
        }
    }
}