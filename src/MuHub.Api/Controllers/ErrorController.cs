using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using MuHub.Application.Exceptions;

namespace MuHub.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is not null)
        {
            if (exception is ValidationException validationException)
            {
                return ValidationProblem(validationException);
            }
            
            var statusCode = RetrieveStatusCode(exception);
            
            return Problem(statusCode: statusCode);
        }
        
        return Problem();
    }

    private IActionResult ValidationProblem(ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();
                
        foreach (var validationExceptionError in validationException.Errors)
        {
            modelStateDictionary.AddModelError(validationExceptionError.Key, validationExceptionError.Value.First());
        }
                
        return ValidationProblem(modelStateDictionary);
    }
    
    private static int RetrieveStatusCode(Exception exception)
    {
        return exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}

