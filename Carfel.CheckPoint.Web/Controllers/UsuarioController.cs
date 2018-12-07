using Carfel.CheckPoint.Web.Interfaces;
using Carfel.CheckPoint.Web.Models;
using Carfel.CheckPoint.Web.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfel.CheckPoint.Web.Controllers {
    public class UsuarioController : Controller {

        public IUsuario UsuarioRepositorio { get; set; }

        public UsuarioController () {
            UsuarioRepositorio = new UsuarioRepositorioSerializacao ();
        }

        #region cadastro de usuario

        [HttpGet]
        public IActionResult Cadastro () {

            UsuarioRepositorioSerializacao repositorioSerializacao = new UsuarioRepositorioSerializacao ();

            ViewBag.UsuarioTipo = HttpContext.Session.GetString("UsuarioTipo");
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");
            // repositorioSerializacao.CadastraADM ();

            return View ();
        }

        [HttpPost]
        public IActionResult Cadastro (IFormCollection form) {

            UsuarioModel usuarioModel = new UsuarioModel (
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"]
            );

            if (usuarioModel.Email.Contains ('@') && usuarioModel.Email.Contains (".com")) {
                if (usuarioModel.Senha.Length >= 6) {

                    usuarioModel.Status = EnTiposUsuario.Comum.ToString();
                    UsuarioRepositorio.Cadastrar (usuarioModel);

                    ViewBag.Mensagem = "Usuario cadastrado !";
                } else {
                    ViewBag.Mensagem = "Senha invalidas";
                }
            } else {
                ViewBag.Mensagem = "E-mail invalido";
            }

            return View ();
        }
        #endregion

        #region Login de usuario

        [HttpGet]
        public IActionResult Login () {
            ViewBag.UsuarioTipo = HttpContext.Session.GetString("UsuarioTipo");
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");
            return View ();
        }

        [HttpPost]
        public IActionResult Login (IFormCollection form) {

            UsuarioModel usuarioModel = UsuarioRepositorio.BuscarEmailSenha(form["email"],form["senha"]);

            if (usuarioModel != null) {

                HttpContext.Session.SetString("UsuarioTipo", usuarioModel.Status);
                HttpContext.Session.SetString("UsuarioNome", usuarioModel.Nome);
                HttpContext.Session.SetString("UsuarioEmail", usuarioModel.Email);
                return RedirectToAction ("Home","Pages");
            }

            ViewBag.Mensagem = "Usuario Incorreto";
            return View ();
        }
        #endregion

            // controler do adiministrador

        [HttpGet]
        public IActionResult Aprovar(string status){

            ComentarioRepositorioSerializado comentarioRepositorio = new ComentarioRepositorioSerializado();
            
            if(status == null)
                TempData["StatusComentario"] = status;
            else
                TempData["StatusCmentario"] = "aprovado";

            ViewData["Comentarios"] = comentarioRepositorio.Listar();

            return View();
        }

        [HttpPost]
        public IActionResult Aprovar(IFormCollection form){

            ComentarioRepositorioSerializado comentario = new ComentarioRepositorioSerializado();

            if(form["choice"] == "aprovar")
            {
                comentario.Editar(EnTiposComentarios.aprovado.ToString(), int.Parse(form["id"]));
            }
            if(form["choice"] == "rejeitar")
            {
                comentario.Editar(EnTiposComentarios.rejeitado.ToString(), int.Parse(form["id"]));
            }

            return View();
        }

        [HttpPost]
        public IActionResult TipoEscolha(IFormCollection form){

            TempData["StatusComentario"] = form["choice"];

            return RedirectToAction("Aprovar");
        }
    }
}