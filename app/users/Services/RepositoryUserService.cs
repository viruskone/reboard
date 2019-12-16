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

        public async Task Create(string email)
        {
            try
            {
                await _repository.Create(new User
                {
                    Email = email
                });
            }
            catch (DuplicateEntryException)
            {
                throw new UserException(UserException.ErrorType.UserAlreadyExist, email);
            }
        }

        public async Task<User> Get(string email)
            => await _repository.Get(email);

        public async Task SetPassword(string email, string hashedPassword)
        {
            var user = await _repository.Get(email);
            if (user != null)
            {
                user.Password = hashedPassword;
                await _repository.Update(user);
            }
        }

        public async Task<bool> Validate(string email, string hashedPassword)
        {
            var user = await _repository.Get(email);
            return user.Password.Equals(hashedPassword);
        }
    }
}