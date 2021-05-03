using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Users
{
    public class Password : ValueObject
    {
        private Password(string value)
        {
            EncryptedValue = value;
        }

        public string EncryptedValue { get; }

        public static implicit operator string(Password password) => password.EncryptedValue;

        public static Password Make(string value, IHashService hashService)
        {
            RuleValidator.CheckRules(value,
                new MinimumLengthRule(Constraints.PasswordMinimumLength, () => ValidationErrors.PasswordMinimumLength(Constraints.PasswordMinimumLength))
            );
            return new Password(hashService.Encrypt(value));
        }

        public static Password MakeFromEncrypted(string encryptedvalue)
            => new Password(encryptedvalue);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EncryptedValue;
        }
    }
}