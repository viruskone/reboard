using System;
using System.Threading.Tasks;
using Reboard.Domain.Users;

namespace Reboard.App.Users.Services
{
    public interface IUserService
    {
        Task Create(string email);
        Task SetPassword(string email, string hashedPassword);
        Task<User> Get(string email);
    }
}
