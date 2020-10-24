using System;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace APIContagem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class ContadorController : ControllerBase
    {
        private static Contador _CONTADOR = new Contador();

        [HttpGet]
        public object Get([FromServices]IConfiguration configuration)
        {
            _CONTADOR.Incrementar();

            lock (_CONTADOR)
            {
                return new
                {
                    _CONTADOR.ValorAtual,
                    Environment.MachineName,
                    Local = "Teste",
                    Sistema = Environment.OSVersion.VersionString,
                    Saudacao = configuration["Saudacao"],
                    TargetFramework = Assembly
                        .GetEntryAssembly()?
                        .GetCustomAttribute<TargetFrameworkAttribute>()?
                        .FrameworkName
                };
            }
        }
    }
}