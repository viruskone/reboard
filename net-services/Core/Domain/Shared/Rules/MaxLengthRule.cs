using Reboard.Core.Domain.Base.Rules;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class MaxLengthRule : IConstructDomainObjectRule
    {
        private readonly int _maxLength;
        private readonly string _value;
        public string Reason => $"Value {_value} extend maximum length ({_maxLength})";

        public MaxLengthRule(string value, int maxLength)
        {
            _value = value;
            _maxLength = maxLength;
        }

        public bool IsInvalid()
            => _value.Length > _maxLength;
    }
}