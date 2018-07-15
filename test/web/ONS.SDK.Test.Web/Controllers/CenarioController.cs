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
using ONS.SDK.Data.Query;

namespace ONS.SDK.Test.Web.Controllers
{
    [Route("api/[controller]")]
    public class CenarioController : Controller
    {
        private readonly ISDKWorker _sdk;

        private readonly IDataQuery _query;

        public CenarioController(ISDKWorker sdk, IDataQuery query) {
            _sdk = sdk;
            _query = query;
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
            
            _sdk.Run(conta, CenarioEvent.ExcluirCenario);

            return "Deletar";
        }

        [HttpGet("ById/{id}")]
        public Conta FindById(string id)
        {
            Console.WriteLine("###### Id: " + id);

            return _query.Set<Conta>().ById(id);
        }

        [HttpGet("All")]
        public IList<Conta> All()
        {
            Console.WriteLine("###### All");

            return _query.Set<Conta>().All();
        }

        private Conta _createConta() {
            return new Conta() {
                Name = Request.Query["name"],
                Balance = Convert.ToInt32(Request.Query["balance"])
            };
        }

    }
}
