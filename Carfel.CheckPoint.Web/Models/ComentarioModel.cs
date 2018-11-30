using System;

namespace Carfel.CheckPoint.Web.Models
{
    [Serializable]
    public class ComentarioModel
    {
        public int Id { get; set; }
        public string Nome {get; set;}
        public string Texto { get; set; }
        public DateTime Horaria { get; set; }
        public ComentarioModel (string nome, string texto, DateTime horario)
        {
            this.Nome = nome;
            this.Texto = texto;
            this.Horaria = horario;
        }
    }
}