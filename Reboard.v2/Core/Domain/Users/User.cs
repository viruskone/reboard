using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users
{
    public class User : Entity
    {
        private User(Login login, Password password)
        {
            Login = login;
            Password = password;

            Id = Guid.NewGuid();
        }

        public Login Login { get; }
        public Password Password { get; }

        public static async Task<User> CreateNew(Login login, Password password, IUserUniqueLoginChecker checker)
        {
            await RuleValidator.CheckRules(login.Value, new LoginMustBeUniqueRule(checker));
            return new User(login, password);
        }

        public static User Make(Login login, Password password)
            => new User(login, password);
    }
}