using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using MuHub.Api.Common.Constants;
using MuHub.Application.Exceptions;

namespace MuHub.Api.Controllers;


/// <summary>
/// Base controller for all API controllers, containing essential helper methods.
/// </summary>
public abstract class MuControllerBase : ControllerBase
{
    /// <summary>
    /// Returns an intuitive error message for the given <see cref="Exception"/>.
    /// </summary>
    /// <returns>An instance for <see cref="IActionResult"/>.</returns>
    protected IActionResult Problem(Exception? exception)
    {
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

        return Problem(statusCode: statusCode, detail: exception.Details);
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
            ClientFailureException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
