using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;
using Reboard.Domain.Users;
using Reboard.WebServer.Architecture;
using Reboard.WebServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/reports/{id?}")]
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
        public async Task<ActionResult<IEnumerable<ReportModel>>> Get()
        {
            if (GetUser(out var user) == false) return Problem(title: "User is empty");

            var query = new ReportsQuery
            {
                User = user
            };
            var result = await _queryGate.HandleAsync<ReportsQuery, IEnumerable<Report>>(query);
            return Ok(result.Select(r => r.FromDomain()));
        }

        [HttpGet]
        public async Task<ActionResult<ReportModel>> Get(string id)
        {
            if (GetUser(out var user) == false) return Problem(title: "User is empty");
            var result = await _queryGate.HandleAsync<ReportQuery, Report>(new ReportQuery { Id = id, User = user });
            return Ok(result.FromDomain());
        }

        private bool GetUser(out User user)
        {
            user = _userAccessor.Get();
            if (user == null)
            {
                _logger.LogWarning("User is empty in controller {Controller}", nameof(ReportsController));
                return false;
            }
            return true;
        }

    }
}