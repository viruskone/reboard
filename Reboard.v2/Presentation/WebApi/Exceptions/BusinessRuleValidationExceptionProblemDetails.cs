using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Base.Rules;
using System.Linq;

namespace Reboard.Presentation.WebApi.Exceptions
{
    public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status422UnprocessableEntity;
            this.Detail = exception.Details;
            this.Type = "business";
        }
    }
}