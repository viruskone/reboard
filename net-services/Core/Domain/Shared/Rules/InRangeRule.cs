using Reboard.Core.Domain.Base.Rules;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class InRangeRule : IConstructDomainObjectRule
    {
        private readonly int _max;
        private readonly int _min;
        private readonly int _value;

        public string Reason => $"Value {_value} is out of range ({_min};{_max}";

        public InRangeRule(int min, int max, int value)
        {
            _min = min;
            _max = max;
            _value = value;
        }

        public bool IsInvalid()
            => _value < _min || _value > _max;
    }
}