using Reboard.Core.Domain.Base.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reboard.Core.Domain.Base
{
    public class ValidationErrorException : Exception
    {
        public const string ExceptionMessage = "Validation error";
        public const string ExceptionSource = "DomainValidation";

        public override string Source { get => ExceptionSource; set { } }
        public override string Message => ExceptionMessage;

        public IEnumerable<ValidationError> Errors { get; }

        public ValidationErrorException(List<ValidationError> errors)
        {
            Errors = errors.AsReadOnly();
        }

        public override string ToString()
        {
            var sb = new StringBuilder(ExceptionMessage);
            sb.Append(".");
            foreach (var error in Errors)
            {
                sb.AppendFormat("{0}: {1}.", error.Code, error.Message);
            }
            return sb.ToString();
        }
    }
}