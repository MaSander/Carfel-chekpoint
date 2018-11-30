using System;
using Carfel.CheckPoint.Web.Interfaces;
using Carfel.CheckPoint.Web.Models;
using Carfel.CheckPoint.Web.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfel.CheckPoint.Web.Controllers
{
    public class ComentarioController : Controller
    {

        public IComentario ComentarioRepositorio{get; set;}
        public ComentarioController ()
        {
            ComentarioRepositorio = new ComentarioRepositorioSerializado();
        }

        [HttpPost]
        public ActionResult Cadastro(IFormCollection form)
        {
            
            ComentarioModel comentarioModel = new ComentarioModel(
                nome: HttpContext.Session.GetString("UsuarioNome"),
                texto: form["texto"],
                horario: DateTime.Now
            );

            ComentarioRepositorio.Cadastrar(comentarioModel);

            return RedirectToAction("Home", "Pages");
        }
    }
}