using Reboard.Core.Domain.Base.Errors;

namespace Reboard.Core.Domain.Shared
{
    public static class ValidationErrors
    {
        public static ValidationError PasswordMinimumLength(int minimumLength)
            => new ValidationError("PWD1", $"password must be at least {minimumLength} characters long");

        public static ValidationError LoginMustBeUnique()
            => new ValidationError("LOG1", "Login must be unique");
    }
}