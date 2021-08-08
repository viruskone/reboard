using Reboard.Core.Domain.Base.Rules;
using System.Text.RegularExpressions;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class AtLeastOneNumberAndOneLetterRule : IConstructDomainObjectRule
    {
        private readonly string _value;

        public string Reason => $"Number and letter is required";

        public AtLeastOneNumberAndOneLetterRule(string value)
        {
            _value = value;
        }

        public bool IsInvalid()
            => Regex.IsMatch(_value, @"(?:\d+[a-z]|[a-z]+\d)[a-z\d]*") == false;
    }
}