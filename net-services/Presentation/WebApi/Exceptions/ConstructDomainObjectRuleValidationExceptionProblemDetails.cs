using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Domain.Base.Rules;

namespace Reboard.Presentation.WebApi.Exceptions
{
    public class ConstructDomainObjectRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public ConstructDomainObjectRuleValidationExceptionProblemDetails(ConstructDomainObjectRuleValidationException exception)
        {
            this.Title = "Validation rule error on " + exception.DomainType;
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = exception.Reason;
            this.Type = "validation";
        }
    }
}