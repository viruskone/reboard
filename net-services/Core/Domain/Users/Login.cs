using Reboard.Core.Domain.Base;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Users
{
    public class Login : OneValueObject<string>
    {
        private Login(string value) : base(value)
        {
        }

        public static explicit operator Login(string value) => Make(value);

        public static implicit operator string(Login login) => login.Value;

        public static Login Make(string value)
        {
            return new Login(value);
        }
    }
}