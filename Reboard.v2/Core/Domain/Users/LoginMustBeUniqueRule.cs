using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Base.Errors;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users
{
    internal class LoginMustBeUniqueRule : IAsyncValidationRule<string>
    {
        private IUserUniqueLoginChecker _checker;

        public LoginMustBeUniqueRule(IUserUniqueLoginChecker checker)
        {
            _checker = checker;
        }

        public ValidationError GetError() => ValidationErrors.LoginMustBeUnique();

        public async Task<bool> IsBroken(string value)
            => (await _checker.IsUnique(value)) == false;
    }
}