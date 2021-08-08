using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.Tests.WebApi.IntegrationTests.Mocks
{
    public class MemoryUserRepository : IUserRepository
    {
        private static List<Company> _company = new List<Company>();
        private static List<User> _users = new List<User>();

        public Task<User> Get(Login login)
            => Task.FromResult(_users.FirstOrDefault(user => user.Login == login));

        public Task<Company> GetCompany(CompanyName name)
            => Task.FromResult(_company.FirstOrDefault(company => company.Name == name));

        public Task Save(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task Save(Company company)
        {
            _company.Add(company);
            return Task.CompletedTask;
        }

        public Task<bool> ValidatePassword(Login login, Password password)
            => Task.FromResult(_users.Any(user => user.Login == login && user.Password == password));
    }
}