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
using ONS.SDK.Impl.Data.Query;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ONS.SDK.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ErrorHandlingFilter]
    public class CenarioController : Controller
    {
        private readonly ILogger<CenarioController> _logger;
        
        private readonly ISDKWorker _sdk;

        private readonly IDataQuery _query;

        public CenarioController(ILogger<CenarioController> logger, ISDKWorker sdk, IDataQuery query) {
            this._logger = logger;
            this._sdk = sdk;
            this._query = query;
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

        [HttpGet("Excluir/{id}")]
        public string Excluir(string id)
        {
            Console.WriteLine("###### Id: " + id);

            var conta = new Conta() { Id = id };
            
            _sdk.Run(conta, CenarioEvent.ExcluirCenario);

            return "Excluir";
        }

        [HttpGet("ById/{id}")]
        public Conta FindById(string id)
        {
            Console.WriteLine("###### Id: " + id);

            return _query.Set<Conta>().ById(id);
        }

        [HttpGet("ByName")]
        public IPagedResult<Conta> FindByName()
        {
            Console.WriteLine("###### ByName ");


            var filter = new ContaFilter() { 
                Name="byName", 
                Parameters=new Conta() { Name=Request.Query["name"] }, 
                Page = Convert.ToInt32(Request.Query["page"]), 
                PageSize = Convert.ToInt32(Request.Query["page_size"])
            };

            return _query.Set<Conta>().FindPaged(filter);
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
