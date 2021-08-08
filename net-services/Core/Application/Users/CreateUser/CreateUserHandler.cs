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
        private readonly ICompanyUniqueNameChecker _companyChecker;
        private readonly IHashService _hashService;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserUniqueLoginChecker checker, IUserRepository userRepository, IHashService hashService, ICompanyUniqueNameChecker companyChecker)
        {
            _checker = checker;
            _userRepository = userRepository;
            _hashService = hashService;
            _companyChecker = companyChecker;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var company = await _userRepository.GetCompany((CompanyName)request.CompanyName);
            if (company == null)
            {
                company = Company.CreateNew((CompanyName)request.CompanyName, _companyChecker);
                await _userRepository.Save(company);
            }
            var user = User.CreateNew(
                (Login)request.Login,
                Password.MakeNew(request.Password, _hashService),
                company,
                _checker);
            await _userRepository.Save(user);
            return Unit.Value;
        }
    }
}