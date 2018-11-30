using System;

namespace Carfel.CheckPoint.Web.Models
{
    [Serializable]
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    
        public UsuarioModel (string nome, string email, string senha)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }

        public UsuarioModel (string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;
        }

        public UsuarioModel()
        {
        }
    }
}