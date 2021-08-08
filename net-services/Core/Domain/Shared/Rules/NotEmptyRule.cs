using Reboard.Core.Domain.Base.Rules;

namespace Reboard.Core.Domain.Shared.Rules
{
    public class NotEmptyRule : IConstructDomainObjectRule
    {
        private readonly string _value;

        public string Reason => "Value is empty";

        public NotEmptyRule(string value)
        {
            _value = value;
        }

        public bool IsInvalid()
            => string.IsNullOrEmpty(_value);
    }
}