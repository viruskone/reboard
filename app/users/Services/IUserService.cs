using Reboard.Domain.Users;
using System.Threading.Tasks;

namespace Reboard.App.Users.Services
{
    public interface IUserService
    {
        Task Create(User user);

        Task SetPassword(string login, string hashedPassword);

        Task<User> Get(string login);

        Task<bool> Validate(string login, string password);
    }
}