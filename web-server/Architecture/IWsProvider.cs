using System.Threading.Tasks;

namespace Reboard.WebServer.Architecture
{
    public interface IWsProvider
    {
        Task<WebSocketAcceptStatus> Accept();
    }

}