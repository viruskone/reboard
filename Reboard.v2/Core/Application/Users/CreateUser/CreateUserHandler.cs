using MediatR;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserUniqueLoginChecker _checker;
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserUniqueLoginChecker checker, IUserRepository userRepository, IHashService hashService)
        {
            _checker = checker;
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await User.CreateNew(
                request.Login,
                Password.Make(request.Password, _hashService),
                _checker);
            await _userRepository.Save(user);
            return Unit.Value;
        }
    }
}