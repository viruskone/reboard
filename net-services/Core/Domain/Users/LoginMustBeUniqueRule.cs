using Reboard.Core.Domain.Base.Rules;
using Reboard.Core.Domain.Users.OutboundServices;

namespace Reboard.Core.Domain.Users
{
    public class LoginMustBeUniqueRule : IBusinessRule
    {
        private readonly Login _login;
        private IUserUniqueLoginChecker _checker;
        public string Message => $"{_login} already exist";

        public LoginMustBeUniqueRule(IUserUniqueLoginChecker checker, Login login)
        {
            _checker = checker;
            _login = login;
        }

        public bool IsBroken()
            => _checker.IsUnique(_login).Result == false;
    }
}