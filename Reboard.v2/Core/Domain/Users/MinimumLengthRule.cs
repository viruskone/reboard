using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Base.Errors;
using System;

namespace Reboard.Core.Domain.Users
{
    public class MinimumLengthRule : IValidationRule<string>
    {
        private readonly int _minimumLength;
        private readonly Func<ValidationError> _errorCallback;

        public MinimumLengthRule(int minimumLength, Func<ValidationError> errorCallback)
        {
            _minimumLength = minimumLength;
            _errorCallback = errorCallback;
        }

        public ValidationError GetError() => _errorCallback();

        public bool IsBroken(string value) => value == null || value.Length < _minimumLength;
    }
}