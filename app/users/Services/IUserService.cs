using Reboard.Domain.Users;
using System.Threading.Tasks;

namespace Reboard.App.Users.Services
{
    public interface IUserService
    {
        Task Create(string email);

        Task SetPassword(string email, string hashedPassword);

        Task<User> Get(string email);

        Task<bool> Validate(string login, string password);
    }
}