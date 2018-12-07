using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Carfel.CheckPoint.Web.Interfaces;
using Carfel.CheckPoint.Web.Models;

namespace Carfel.CheckPoint.Web.Repositorios {
    public class ComentarioRepositorioSerializado : IComentario {
        public List<ComentarioModel> ComentariosSalvos { get; set; }

        /// <summary>
        /// cria ou pede para ler a lista de comentarios
        /// </summary>
        public ComentarioRepositorioSerializado () {
            if (File.Exists ("comentarios.dat")) {
                ComentariosSalvos = LerArquivoSerializado ();
            } else {
                ComentariosSalvos = new List<ComentarioModel> ();
            }
        }

        /// <summary>
        /// Le o arquivo serializado de comentarios
        /// </summary>
        /// <returns></returns>
        private List<ComentarioModel> LerArquivoSerializado () {
            byte[] bytesSerializados = File.ReadAllBytes ("comentarios.dat");

            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter ();

            return (List<ComentarioModel>) serializador.Deserialize (memoria);

        }

        /// <summary>
        /// adiciona um comentario na lista de comentario
        /// </summary>
        /// <param name="comentario"></param>
        public void Cadastrar (ComentarioModel comentario) {
            comentario.Id = ComentariosSalvos.Count + 1;
            ComentariosSalvos.Add (comentario);

            EscreverNoArquivo ();
        }

        /// <summary>
        /// escreve serializada a lista de comentarios no arquivo
        /// </summary>
        private void EscreverNoArquivo () {
            MemoryStream memoria = new MemoryStream ();

            BinaryFormatter serializador = new BinaryFormatter ();

            serializador.Serialize (memoria, ComentariosSalvos);

            byte[] bytes = memoria.ToArray ();

            File.WriteAllBytes ("comentarios.dat", bytes);
        }

        /// <summary>
        /// Altera o status do comentario
        /// </summary>
        /// <param name="novostatus"></param>
        /// <param name="id"></param>
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

        /// <summary>
        /// metodo que retorna a lista de usuarios para ser usada
        /// </summary>
        /// <returns></returns>
        public List<ComentarioModel> Listar () {
            return ComentariosSalvos;
        }

        /// <summary>
        /// Busca o comentario pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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