using System.Threading.Tasks;

namespace Reboard.Core.Domain.Base
{
    public static class RuleValidator
    {
        public static async Task CheckRules<TCheck>(TCheck value, params IAsyncValidationRule<TCheck>[] rules)
        {
            var validationResult = await Validate(value, rules);
            if (validationResult.IsSuccess == false)
                throw validationResult.GetError();
        }

        public static void CheckRules<TCheck>(TCheck value, params IValidationRule<TCheck>[] rules)
        {
            var validationResult = Validate(value, rules);
            if (validationResult.IsSuccess == false)
                throw validationResult.GetError();
        }

        public static async Task<ValidationResult> Validate<T>(T value, IAsyncValidationRule<T>[] rules)
        {
            var validationResult = new ValidationResult();
            foreach (var rule in rules)
            {
                if (await rule.IsBroken(value))
                    validationResult.AddBrokenRule(rule);
            }

            return validationResult;
        }

        public static ValidationResult Validate<T>(T value, IValidationRule<T>[] rules)
        {
            var validationResult = new ValidationResult();
            foreach (var rule in rules)
            {
                if (rule.IsBroken(value))
                    validationResult.AddBrokenRule(rule);
            }

            return validationResult;
        }
    }
}