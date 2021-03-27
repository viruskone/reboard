using System;

namespace Reboard.CQRS
{
    public class JobStatusChangedEventArgs : EventArgs
    {
        public CommandStatus NewStatus { get; set; }
    }

}