using Reboard.CQRS;

namespace Reboard.WebServer.Architecture
{
    public interface INotification
    {
        void RegisterJob(Job job);

    }

}