using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Users.OutboundServices;
using System.Collections.Generic;

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
            var hashedPassword = hashService.Encrypt(password);
            return MakeFromEncrypted(hashedPassword);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EncryptedValue;
        }
    }
}