using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Shared.Rules;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Collections.Generic;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Users
{
    public class Password : ValueObject
    {
        public string EncryptedValue { get; }

        private Password(string encryptedValue)
        {
            EncryptedValue = encryptedValue;
        }

        public static implicit operator string(Password password) => password.EncryptedValue;

        public static Password MakeFromEncrypted(string encryptedvalue)
            => new Password(encryptedvalue);

        public static Password MakeNew(string password, IHashService hashService)
        {
            CheckRule(new MinLengthRule(password, 6));
            CheckRule(new AtLeastOneNumberAndOneLetterRule(password));
            CheckRule(new NonAlphaCharactersRule(password));
            var hashedPassword = hashService.Encrypt(password);
            return MakeFromEncrypted(hashedPassword);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EncryptedValue;
        }
    }
}