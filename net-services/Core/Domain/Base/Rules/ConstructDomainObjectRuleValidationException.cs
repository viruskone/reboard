using System;

namespace Reboard.Core.Domain.Base.Rules
{
    public class ConstructDomainObjectRuleValidationException : Exception
    {
        public string DomainType { get; }
        public IConstructDomainObjectRule InvalidRule { get; }
        public string Reason { get; }

        public ConstructDomainObjectRuleValidationException(Type type, IConstructDomainObjectRule invalidRule)
            : base($"Construct {type.Name} failed. {invalidRule.Reason}")
        {
            DomainType = type.Name;
            InvalidRule = invalidRule;
            Reason = invalidRule.Reason;
        }

        public override string ToString()
        {
            return $"{DomainType} -> {InvalidRule.GetType().FullName}: {InvalidRule.Reason}";
        }
    }
}