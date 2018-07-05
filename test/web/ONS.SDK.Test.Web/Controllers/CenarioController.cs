using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ONS.SDK.Test.Web.Models;
using ONS.SDK.Test.Web.Process;
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
            var payload = new InseriCenarioPayload();

            _sdk.Run(payload, CenarioEvent.InserirCenario);

            return "Inserir";
        }

        [HttpGet("Alterar")]
        public string Alterar()
        {
            var payload = new AlteraCenarioPayload();

            _sdk.Run(payload, CenarioEvent.AlterarCenario);

            return "Alterar";
        }

    }
}
