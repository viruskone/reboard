using System;

namespace Reboard.CQRS
{
    public class Job
    {
        private string resourceUrl;

        public Guid Id { get; set; }
        public CommandStatus Status { get; set; }
        public ICommand Command { get; set; }

        public void RegisterResourceUrl(string resourceUrl) => this.resourceUrl = resourceUrl;

        public string GetResourceUrl() => resourceUrl;
    }
}