using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.WebServer.Architecture;
using System;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/command")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IQueueCommandDispatcher _dispatcher;

        public CommandController(IQueueCommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var job = await _dispatcher.GetJob(id);
            switch (job.Status)
            {
                case CommandStatus.WaitingToRun:
                case CommandStatus.Running:
                    return this.AcceptedAtTask(id);

                case CommandStatus.Completed:
                    return Created(job.GetResourceUrl(), null);

                case CommandStatus.Faulted:
                    return Problem(title: "Command faulted", instance: id.ToString(), type: job.Command.GetType().FullName);

                default:
                    return Problem("Command status unknown", id.ToString(), (int?)job.Status);
            }
        }
    }
}