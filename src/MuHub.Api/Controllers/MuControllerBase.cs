using ErrorOr;

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
    
    /// <summary>
    /// Returns an intuitive error message for the given <see cref="List{T}"/> of <see cref="Error"/>.
    /// </summary>
    /// <param name="errors">List of errors.</param>
    /// <returns></returns>
    protected IActionResult Problem(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        if (errors.Any(x => x.Type == ErrorType.Unexpected))
        {
            return Problem();
        }

        var firstError = errors.First();
        var statusCode = RetrieveStatusCode(firstError);
        
        return Problem(statusCode: statusCode, detail: firstError.Description);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    protected IActionResult Problem(Error error, int statusCode)
    {
        return Problem(statusCode: statusCode, detail: error.Description);
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
    
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
                
        foreach (var validationExceptionError in errors)
        {
            modelStateDictionary.AddModelError(validationExceptionError.Code, validationExceptionError.Description);
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
    
    private static int RetrieveStatusCode(Error error)
    {
        return error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
