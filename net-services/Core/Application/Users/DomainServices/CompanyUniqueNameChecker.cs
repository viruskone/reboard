using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Users.DomainServices
{
    public class CompanyUniqueNameChecker : ICompanyUniqueNameChecker
    {
        private readonly IUserRepository _repository;

        public CompanyUniqueNameChecker(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUnique(string value)
        {
            var companyWithThatName = await _repository.GetCompany((CompanyName)value);
            return companyWithThatName == null;
        }
    }
}