using Reboard.CQRS;
using System;

namespace Reboard.WebServer.Architecture
{
    public class JobStatus
    {
        public Guid JobId { get; set; }
        public CommandStatus Status { get; set; }
    }

}