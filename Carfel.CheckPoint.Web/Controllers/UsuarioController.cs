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
        public ActionResult Cadastro () {

            UsuarioRepositorioSerializacao repositorioSerializacao = new UsuarioRepositorioSerializacao ();

            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");
            // repositorioSerializacao.CadastraADM ();

            return View ();
        }

        [HttpPost]
        public ActionResult Cadastro (IFormCollection form) {

            UsuarioModel usuarioModel = new UsuarioModel (
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"]
            );

            if (usuarioModel.Email.Contains ('@') && usuarioModel.Email.Contains (".com")) {
                if (usuarioModel.Senha.Length >= 6) {

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
        public ActionResult Login () {
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome");
            return View ();
        }

        [HttpPost]
        public ActionResult Login (IFormCollection form) {

            UsuarioModel usuario = new UsuarioModel (email: form["email"],senha: form["senha"]);

            UsuarioModel usuarioModel = UsuarioRepositorio.BuscarEmailSenha(usuario.Email,usuario.Senha);


            if (usuarioModel != null) {

                if(usuarioModel.Email == "adm@email.com" && usuarioModel.Senha == "senha123")
                {
                    HttpContext.Session.SetString("UsuarioNome", usuarioModel.Nome);
                    HttpContext.Session.SetString("Tipo", TiposUsuario.Administrador.ToString());
                    return View();
                }

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
        public ActionResult Aprovacao() => View();

        [HttpPost]
        public ActionResult Aprovacao(IFormCollection form){

            return View();
        }
    }
}