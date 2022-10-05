using AutoMapper.Internal;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using MuHub.Api.Common.Extensions;
using MuHub.Api.Common.Factories;

namespace MuHub.Api.Common.Filters;

/// <summary>
/// A filter that validates the model state, received from the request.
/// </summary>
public class ApiModelValidationFilter : IAsyncActionFilter
{
    private readonly ICustomModelValidationFactory _validatorFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiModelValidationFilter"/> class.
    /// </summary>
    /// <param name="validatorFactory">Factory of validator.</param>
    public ApiModelValidationFilter(ICustomModelValidationFactory validatorFactory)
    {
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }
    
    /// <summary>
    /// Called asynchronously before the action, after model binding is complete.
    /// </summary>
    /// <param name="context">The <see cref="ActionExecutingContext"/>.</param>
    /// <param name="next">
    /// The <see cref="ActionExecutionDelegate"/>. Invoked to execute the next action filter or the action itself.
    /// </param>
    /// <returns>A <see cref="Task"/> that on completion indicates the filter has executed.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.Any())
        {
            await next();
            return;
        }

        var validationFailures = new List<ValidationFailure>();

        foreach (var actionArgument in context.ActionArguments)
        {
            var validationErrors = await GetValidationErrorsAsync(actionArgument.Value);
            validationFailures.AddRange(validationErrors);
        }

        if (!validationFailures.Any())
        {
            await next();
            return;
        }

        context.Result = new BadRequestObjectResult(validationFailures.ToProblemDetails());
    }
    
    private async Task<IEnumerable<ValidationFailure>> GetValidationErrorsAsync(object? value)
    {
        if (value is not null)
        {
            var validatorInstance = _validatorFactory.GetValidator(value.GetType());

            if (validatorInstance is not null)
            {
                var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(value));
                return validationResult.Errors ?? Enumerable.Empty<ValidationFailure>();
            }
        }
        
        return Enumerable.Empty<ValidationFailure>();
    }
}
