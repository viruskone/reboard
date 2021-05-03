using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Domain.Base;
using System.Linq;

namespace Reboard.Presentation.WebApi.Exceptions
{
    public class ValidationErrorProblemDetails : ProblemDetails
    {
        public ValidationErrorProblemDetails(ValidationErrorException exception)
        {
            Title = exception.Source;
            Status = StatusCodes.Status422UnprocessableEntity;
            Detail = string.Join(";", exception.Errors.Select(error => error.Message));
            Extensions.Add("codes", exception.Errors.Select(e => new ErrorDto { Code = e.Code, Description = e.Message }).ToArray());
        }

        private class ErrorDto
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }
    }
}