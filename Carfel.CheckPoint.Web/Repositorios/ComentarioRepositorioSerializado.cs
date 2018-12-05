using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Carfel.CheckPoint.Web.Interfaces;
using Carfel.CheckPoint.Web.Models;

namespace Carfel.CheckPoint.Web.Repositorios {
    public class ComentarioRepositorioSerializado : IComentario {
        public List<ComentarioModel> ComentariosSalvos { get; set; }

        public ComentarioRepositorioSerializado () {
            if (File.Exists ("comentarios.dat")) {
                ComentariosSalvos = LerArquivoSerializado ();
            } else {
                ComentariosSalvos = new List<ComentarioModel> ();
            }
        }

        private List<ComentarioModel> LerArquivoSerializado () {
            byte[] bytesSerializados = File.ReadAllBytes ("comentarios.dat");

            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter ();

            return (List<ComentarioModel>) serializador.Deserialize (memoria);

        }

        public void Cadastrar (ComentarioModel comentario) {
            comentario.Id = ComentariosSalvos.Count + 1;
            ComentariosSalvos.Add (comentario);

            EscreverNoArquivo ();
        }

        private void EscreverNoArquivo () {
            MemoryStream memoria = new MemoryStream ();

            BinaryFormatter serializador = new BinaryFormatter ();

            serializador.Serialize (memoria, ComentariosSalvos);

            byte[] bytes = memoria.ToArray ();

            File.WriteAllBytes ("comentarios.dat", bytes);
        }

        public ComentarioModel Avaliacao (ComentarioModel comentario) {
            throw new System.NotImplementedException ();
        }

        public void Editar (string novostatus, int id) {

            ComentarioModel comenatrioBuscado = BuscarPorId(id);

            for(int i=0; i<ComentariosSalvos.Count; i++)
            {
                if(id == ComentariosSalvos[i].Id)
                {
                    ComentariosSalvos[i].Status = novostatus;
                    break;
                }
            }

            EscreverNoArquivo();
        }

        public List<ComentarioModel> Listar () {
            return ComentariosSalvos;
        }

        public ComentarioModel BuscarPorId (int id) {
            foreach (ComentarioModel item in ComentariosSalvos) {
                if (id == item.Id) {
                    return item;
                }
            }
            return null;
        }
    }
}