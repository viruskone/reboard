using Reboard.Core.Domain.Base.Errors;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Base
{
    public class ValidationResult : ValueObject
    {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        public ValidationResult()
        {
        }

        public bool IsSuccess => _errors.Count == 0;

        public void AddBrokenRule(IValidationErrorMaker rule)
            => _errors.Add(rule.GetError());

        public ValidationErrorException GetError()
                    => new ValidationErrorException(_errors);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IsSuccess;
            foreach (var error in _errors)
            {
                yield return error.Code;
            }
        }
    }
}