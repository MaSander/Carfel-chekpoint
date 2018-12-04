using System.Collections.Generic;
using Carfel.CheckPoint.Web.Models;

namespace Carfel.CheckPoint.Web.Interfaces
{
    public interface IComentario
    {
        void Cadastrar(ComentarioModel comentario);
    }
}