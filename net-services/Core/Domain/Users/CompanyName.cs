using Reboard.Core.Domain.Base;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Users
{
    public class CompanyName : ValueObject
    {
        public string Value { get; }

        private CompanyName(string value)
        {
            Value = value;
        }

        public static explicit operator CompanyName(string value) => Make(value);

        public static implicit operator string(CompanyName companyName) => companyName.Value;

        public static CompanyName Make(string value)
        {
            return new CompanyName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}