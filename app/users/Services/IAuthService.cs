using Reboard.Domain.Auth;
using System.Threading.Tasks;

namespace Reboard.App.Users.Services
{
    public interface IAuthService
    {
        Task Failed(string requestId, string user);
        Task Success(string requestId, string user, string token);
        Task<Auth> Get(string requestId);
    }
}