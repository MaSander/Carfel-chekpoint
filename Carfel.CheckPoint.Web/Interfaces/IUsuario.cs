using Carfel.CheckPoint.Web.Models;

namespace Carfel.CheckPoint.Web.Interfaces
{
    public interface IUsuario
    {
        UsuarioModel Cadastrar(UsuarioModel usuario);

        UsuarioModel BuscarEmailSenha(string email, string senha);
    }
}