using Reboard.Core.Domain.Base.Rules;
using Reboard.Core.Domain.Users.OutboundServices;

namespace Reboard.Core.Domain.Users
{
    public class CompanyNameMustBeUniqueRule : IBusinessRule
    {
        private readonly CompanyName _companyName;
        private ICompanyUniqueNameChecker _checker;
        public string Message => $"{_companyName} already exist";

        public CompanyNameMustBeUniqueRule(ICompanyUniqueNameChecker checker, CompanyName name)
        {
            _checker = checker;
            _companyName = name;
        }

        public bool IsBroken()
            => _checker.IsUnique(_companyName).Result == false;
    }
}