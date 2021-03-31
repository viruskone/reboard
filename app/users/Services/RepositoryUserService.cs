using Reboard.Domain.Users;
using Reboard.Repository;
using Reboard.Repository.Exceptions;
using System.Threading.Tasks;

namespace Reboard.App.Users.Services
{
    public class RepositoryUserService : IUserService
    {
        private readonly IUserRepository _repository;

        public RepositoryUserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(User user)
        {
            try
            {
                await _repository.Create(user);
            }
            catch (DuplicateEntryException)
            {
                throw new UserException(UserException.ErrorType.UserAlreadyExist, user.Login);
            }
        }

        public async Task<User> Get(string login)
            => await _repository.Get(login);

        public async Task SetPassword(string login, string hashedPassword)
        {
            var user = await _repository.Get(login);
            if (user != null)
            {
                user.Password = hashedPassword;
                await _repository.Update(user);
            }
        }

        public async Task<bool> Validate(string login, string hashedPassword)
        {
            var user = await _repository.Get(login);
            return user != null && user.Password.Equals(hashedPassword);
        }
    }
}