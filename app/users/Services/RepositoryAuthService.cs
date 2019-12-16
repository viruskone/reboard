using Reboard.Domain.Auth;
using Reboard.Repository;
using System;
using System.Threading.Tasks;

namespace Reboard.App.Users.Services
{
    public class RepositoryAuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public RepositoryAuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task Failed(string requestId)
            => await _repository.Create(new Auth
            {
                RequestId = requestId,
                Status = AuthStatus.Failed,
                Time = DateTime.Now
            });

        public async Task<Auth> Get(string requestId)
            => await _repository.Get(requestId);

        public async Task Success(string requestId, string token)
            => await _repository.Create(new Auth
            {
                RequestId = requestId,
                Status = AuthStatus.Success,
                Token = token,
                Time = DateTime.Now
            });
    }
}