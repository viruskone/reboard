using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using static Reboard.Core.Domain.Base.Rules.BusinessRuleValidator;

namespace Reboard.Core.Domain.Users
{
    public class User : Entity
    {
        public Login Login { get; }

        public Password Password { get; }

        private User(Login login, Password password)
        {
            Login = login;
            Password = password;

            Id = Guid.NewGuid();
        }

        public static User CreateNew(Login login, Password password, IUserUniqueLoginChecker checker)
        {
            CheckRule(new LoginMustBeUniqueRule(checker, login));
            return new User(login, password);
        }

        public static User Make(Login login, Password password)
            => new User(login, password);
    }
}