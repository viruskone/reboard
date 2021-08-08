using Reboard.Core.Domain.Base.Rules;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class MinLengthRule : IConstructDomainObjectRule
    {
        private readonly int _minLength;
        private readonly string _value;

        public string Reason => $"Value {_value} is shorter than {_minLength} chars";

        public MinLengthRule(string value, int minLength)
        {
            _value = value;
            _minLength = minLength;
        }

        public bool IsInvalid()
            => _value.Length < _minLength;
    }
}