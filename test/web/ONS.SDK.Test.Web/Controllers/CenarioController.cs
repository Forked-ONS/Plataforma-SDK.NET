using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONS.SDK.Test.Web.Models;
using ONS.SDK.Test.Web.Process;
using ONS.SDK.Test.Web.Entities;
using ONS.SDK.Worker;

namespace ONS.SDK.Test.Web.Controllers
{
    [Route("api/[controller]")]
    public class CenarioController : Controller
    {
        private readonly SDKWorker _sdk;

        public CenarioController(SDKWorker sdk) {
            _sdk = sdk;
        }

        [HttpGet("Inserir")]
        public string Inserir()
        {
            Console.WriteLine("###### Inserir");
            var conta = _createConta();

            _sdk.Run(conta, CenarioEvent.InserirCenario);

            return "Inserir";
        }

        [HttpGet("Alterar")]
        public string Alterar()
        {
            var conta = _createConta();
            conta.Id = Request.Query["id"];
            
            _sdk.Run(conta, CenarioEvent.AlterarCenario);
            
            return "Alterar";
        }

        [HttpGet("Deletar")]
        public string Deletar()
        {
            var conta = _createConta();
            
            //_sdk.Run(conta, CenarioEvent.DeletarCenario);

            return "Deletar";
        }

        private Conta _createConta() {
            return new Conta() {
                Name = Request.Query["name"],
                Balance = Convert.ToInt32(Request.Query["balance"])
            };
        }

    }
}
