using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using MuHub.Api.Common.Constants;
using MuHub.Application.Exceptions;

namespace MuHub.Api.Controllers;

/// <summary>
/// Error controller that is used to handle all unhandled exceptions.
/// </summary>
public class ErrorController : ControllerBase
{
    /// <summary>
    /// Handles all unhandled exceptions.
    /// </summary>
    /// <returns></returns>
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is DomainException domainException)
        {
            if (exception is ModelValidationException validationException)
            {
                return ValidationProblem(validationException);
            }
            
            return Problem(domainException);
        }

        return Problem();
    }
    
    private IActionResult Problem(DomainException exception)
    {
        if (exception.AdditionalContext is not null)
        {
            HttpContext.Items[ProblemDetailsConstants.AdditionalContext] = exception.AdditionalContext;
        }
        
        var statusCode = RetrieveStatusCode(exception);
            
        return Problem(statusCode: statusCode, detail: exception.Message);
    }
    
    private IActionResult ValidationProblem(ModelValidationException modelValidationException)
    {
        var modelStateDictionary = new ModelStateDictionary();
                
        foreach (var validationExceptionError in modelValidationException.Errors)
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

