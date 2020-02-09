namespace Reboard.CQRS
{
    public enum CommandStatus
    {
        WaitingToRun = 1,
        Running = 2,
        Completed = 3,
        Faulted = 4
    }
}