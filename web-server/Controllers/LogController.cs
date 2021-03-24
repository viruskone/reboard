using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Reboard.WebServer.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {

        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public OkResult Handle([FromBody] string content)
        {
            _logger.LogError(content);
            return Ok();
        }
    }
}