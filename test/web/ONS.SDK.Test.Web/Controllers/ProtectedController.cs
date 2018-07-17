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
using Microsoft.AspNetCore.Authorization;

namespace ONS.SDK.Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ErrorHandlingFilter]
    [Authorize(Roles = "Servico")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        private readonly ILogger<ProtectedController> _logger;

        private readonly IDataQuery _query;

        public ProtectedController(ILogger<ProtectedController> logger, IDataQuery query) {
            this._logger = logger;
            this._query = query;
        }
        
        [HttpGet("All")]
        public IList<Conta> All()
        {
            Console.WriteLine("###### All");

            return _query.Set<Conta>().All();
        }

    }
}
