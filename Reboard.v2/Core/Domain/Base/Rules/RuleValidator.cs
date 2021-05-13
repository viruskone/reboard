using System.Diagnostics;

namespace Reboard.Core.Domain.Base.Rules
{
    public static class RuleValidator
    {
        public static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public static void CheckRule(IConstructDomainObjectRule rule)
        {
            if (rule.IsInvalid())
            {
                var stackTrace = new StackTrace();
                throw new ConstructDomainObjectRuleValidationException(stackTrace.GetFrame(1).GetMethod().DeclaringType, rule);
            }
        }
    }
}