using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users.OutboundServices
{
    public interface IUserRepository
    {
        Task<User> Get(Login login);

        Task Save(User user);
    }
}