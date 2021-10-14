using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlumsailTest.Infrastructure.Exceptions;

namespace PlumsailTest.Logic.MvcFilters
{
    public class AppBadRequestExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not AppBadRequestException exception)
                return;

            context.ExceptionHandled = true;
            var validationProblemDetails = new ValidationProblemDetails();
            validationProblemDetails.Errors.Add(exception.Field, new[] {exception.ErrorMessage});
            context.Result = new BadRequestObjectResult(validationProblemDetails);
        }
    }
}