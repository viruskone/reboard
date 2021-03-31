using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Queries;
using System.Threading.Tasks;

namespace Reboard.App.Users.QueryHandlers
{
    public class UserQueryHandler : IQueryHandler<UserQuery, User>
    {
        private readonly IUserService _service;

        public UserQueryHandler(IUserService service)
        {
            _service = service;
        }

        public async Task<User> HandleAsync(UserQuery query)
        {
            var user = await _service.Get(query.Login);
            user.Password = string.Empty;
            return user;
        }
    }
}