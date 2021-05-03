using Reboard.Core.Domain.Users.OutboundServices;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Users.DomainServices
{
    public class UserUniqueLoginChecker : IUserUniqueLoginChecker
    {
        private readonly IUserRepository _repository;

        public UserUniqueLoginChecker(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUnique(string login)
        {
            var userWithThatLogin = await _repository.Get(login);
            return userWithThatLogin == null;
        }
    }
}