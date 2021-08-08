using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Users
{
    public class User : Entity<UserId>
    {
        public Company Company { get; }
        public Login Login { get; }

        public Password Password { get; }

        private User(Login login, Password password, Company company)
        {
            Login = login;
            Password = password;
            Company = company;

            Id = UserId.Make(Guid.NewGuid());
        }

        private User(Guid id, Login login, Password password, Company company)
        {
            Id = UserId.Make(id);
            Login = login;
            Password = password;
            Company = company;
        }

        public static User CreateNew(Login login, Password password, Company company, IUserUniqueLoginChecker checker)
        {
            CheckRule(new LoginMustBeUniqueRule(checker, login));
            return new User(login, password, company);
        }

        public static User Make(Guid userId, Login login, Password password, Company company)
            => new User(userId, login, password, company);
    }
}