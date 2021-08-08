using Reboard.Core.Domain.Base.Rules;
using System.Text.RegularExpressions;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class NonAlphaCharactersRule : IConstructDomainObjectRule
    {
        private readonly string _value;

        public string Reason => $"At least one non alpha character is required";

        public NonAlphaCharactersRule(string value)
        {
            _value = value;
        }

        public bool IsInvalid()
            => Regex.IsMatch(_value, @"[^0-9a-zA-Z\._]") == false;
    }
}