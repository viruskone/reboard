using System;

namespace Reboard.CQRS
{
    public class Job
    {
        private CommandStatus status;

        public Guid Id { get; set; }
        public CommandStatus Status
        {
            get { return status; }
            set
            {
                if (value == status) return;
                status = value;
                StatusChanged?.Invoke(this, new JobStatusChangedEventArgs { NewStatus = status });
            }
        }
        public ICommand Command { get; set; }

        public event EventHandler<JobStatusChangedEventArgs> StatusChanged;

    }

}