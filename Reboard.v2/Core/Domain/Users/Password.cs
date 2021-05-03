using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Users
{
    public class Password : ValueObject
    {
        public string Value { get; }

        private Password(string value)
        {
            Value = value;
        }

        public static Password Make(string value)
        {
            RuleValidator.CheckRules(value,
                new MinimumLengthRule(Constraints.PasswordMinimumLength, () => ValidationErrors.PasswordMinimumLength(Constraints.PasswordMinimumLength))
            );
            return new Password(value);
        }

        public static implicit operator Password(string value) => Make(value);

        public static implicit operator string(Password password) => password.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}