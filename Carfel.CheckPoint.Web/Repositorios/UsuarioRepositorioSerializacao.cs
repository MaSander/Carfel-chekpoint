using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Carfel.CheckPoint.Web.Interfaces;
using Carfel.CheckPoint.Web.Models;

namespace Carfel.CheckPoint.Web.Repositorios
{
    public class UsuarioRepositorioSerializacao : IUsuario
    {

        private List<UsuarioModel> UsuariosSalvos { get; set; }

        /// <summary>
        /// Cria uma lista de usuario
        /// </summary>
        public UsuarioRepositorioSerializacao()
        {
            if(File.Exists("usuario.dat"))
            {
                UsuariosSalvos = LerArquivoSerializado();
            }else
            {
                UsuariosSalvos = new List<UsuarioModel>();
            }
        }

        /// <summary>
        /// Le o arquivo serealizado e transforma em lista
        /// </summary>
        /// <returns>Retorna Lista de usuario</returns>
        private List<UsuarioModel> LerArquivoSerializado()
        {
            byte[] bytesSerializados = File.ReadAllBytes("usuario.dat");

            MemoryStream memoria = new MemoryStream(bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter();

            return (List<UsuarioModel>) serializador.Deserialize(memoria);
        }

        

        /// <summary>
        /// adiciona um usuario a lista de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>retorna um usuario</returns>
        public UsuarioModel Cadastrar(UsuarioModel usuario)
        {
            usuario.Id = UsuariosSalvos.Count + 1;
            UsuariosSalvos.Add(usuario);

            EscreverNoArquivo();

            return usuario;
        }

        /// <summary>
        /// Serializa a lista de usuario e escreve no arquivo
        /// </summary>
        private void EscreverNoArquivo()
        {
            MemoryStream memoria = new MemoryStream();

            BinaryFormatter serializadora = new BinaryFormatter();

            serializadora.Serialize(memoria, UsuariosSalvos);

            byte[] bytes = memoria.ToArray();

            File.WriteAllBytes("usuario.dat", bytes);
        }

        /// <summary>
        /// Cadastra o primeiro adiministrador
        /// </summary>
        public void CadastraADM () {
            if (!File.Exists ("usuario.dat")) {
                UsuarioModel usuarioModel = new UsuarioModel ();

                UsuarioRepositorioSerializacao usuarioRepositorio = new UsuarioRepositorioSerializacao ();

                usuarioModel.Id = 00001;
                usuarioModel.Nome = "ADM";
                usuarioModel.Email = "adm@email.com";
                usuarioModel.Senha = "senha123";

                usuarioRepositorio.Cadastrar (usuarioModel);
            }
        }

        public UsuarioModel BuscarEmailSenha(string email, string senha)
        {
            foreach (UsuarioModel item in UsuariosSalvos)
            {
                if(email == item.Email && senha == item.Senha)
                return item;
            }
            return null;
        }
    }
}