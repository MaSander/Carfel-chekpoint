using System;

namespace Carfel.CheckPoint.Web.Models
{
    [Serializable]
    public class ComentarioModel
    {
        public int Id { get; set; }
        public string Nome {get; set;}
        public string Texto { get; set; }
        public DateTime Horario { get; set; }
        public string Status { get; set; } // espera; aceito; rejeitado
        public ComentarioModel (string nome, string texto, DateTime horario, string status)
        {
            this.Nome = nome;
            this.Texto = texto;
            this.Horario = horario;
            this.Status = status;
        }
    }
}