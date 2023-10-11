using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VebTech.Application.Requests.ExceptionsHandling;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            int? statusCode = 200;

            if (validationException.Errors.Any(failure => failure.ErrorCode == "404"))
                statusCode = 404;
            else if (validationException.Errors.Any(failure => failure.ErrorCode == "400"))
                statusCode = 400;
                
                
            context.Result = new ObjectResult(validationException)
            {
                StatusCode = statusCode,
                Value = validationException.Errors,
            };

            context.ExceptionHandled = true;
        }
        else if (context.Exception is HttpResponseException httpResponseException)
        {
            context.Result = new ObjectResult(httpResponseException.Value)
            {
                StatusCode = httpResponseException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }

    
}