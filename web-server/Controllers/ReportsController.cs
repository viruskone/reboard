using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;

namespace Reboard.WebServer.Controllers
{
    [Route("api/reports")]
    [Authorize]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryGate;

        public ReportsController(IQueryDispatcher queryDispatcher)
        {
            _queryGate = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> Get()
            => Ok(await _queryGate.HandleAsync<ReportsQuery, IEnumerable<Report>>(new ReportsQuery()));

    }
}