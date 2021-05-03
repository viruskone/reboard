using Reboard.Core.Domain.Base;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Users
{
    public class Login : ValueObject
    {
        public string Value { get; }

        private Login(string value)
        {
            Value = value;
        }

        public static Login Make(string value)
        {
            return new Login(value);
        }

        public static implicit operator Login(string value) => Make(value);

        public static implicit operator string(Login login) => login.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}