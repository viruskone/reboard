using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;
using Reboard.WebServer.Architecture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/reports")]
    [Authorize]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryGate;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IQueryDispatcher queryDispatcher, IUserAccessor userAccessor, ILogger<ReportsController> logger)
        {
            _queryGate = queryDispatcher;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> Get()
        {
            var user = _userAccessor.Get();
            if (user == null)
            {
                _logger.LogWarning("User is empty in controller {Controller}, method ", nameof(ReportsController), nameof(Get));
                return Problem(title: "User is empty");
            }
            var query = new ReportsQuery
            {
                ForUser = user.Login,
                ForCompany = user.Company
            };
            var result = await _queryGate.HandleAsync<ReportsQuery, IEnumerable<Report>>(query);
            return Ok(result);
        }
    }
}