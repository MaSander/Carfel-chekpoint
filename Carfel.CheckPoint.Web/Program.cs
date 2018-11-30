using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Carfel.CheckPoint.Web.Models;
using Carfel.CheckPoint.Web.Repositorios;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Carfel.CheckPoint.Web {
    public class Program {
        public static void Main (string[] args) {

            UsuarioRepositorioSerializacao repositorioSerializacao = new UsuarioRepositorioSerializacao();

            repositorioSerializacao.CadastraADM ();

            CreateWebHostBuilder (args).Build ().Run ();

        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ();

    }
}