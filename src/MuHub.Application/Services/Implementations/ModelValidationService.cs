using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;

using MuHub.Application.Exceptions;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Application.Services.Implementations;

// TODO: Add logging
// TODO: Maybe add Result/Either variation extensions to convert errors
/// <summary>
/// Validates input models using FluentValidation.
/// </summary>
public sealed class ModelValidationService : IModelValidationService
{
    private const string ValidationPrefix = "General.ModelValidation";
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of <see cref="ModelValidationService"/>.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    /// <exception cref="ArgumentNullException">If some parameter contains null.</exception>
    public ModelValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public void EnsureValid<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ModelValidationException(validationResult.Errors);
        }
    }
    
    public async Task EnsureValidAsync<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = await ValidateAsync(model);
        
        if (!validationResult.IsValid)
        {
            throw new ModelValidationException(validationResult.Errors);
        }
    }
    
    public List<Error>? DetermineIfValid<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = Validate(model);
        
        return !validationResult.IsValid 
            ? validationResult.Errors.Select(x => Error.Validation($"{ValidationPrefix}.{x.ErrorCode}", x.ErrorMessage)).ToList()
            : null;
    }
    
    public async Task<List<Error>?> DetermineIfValidAsync<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = await ValidateAsync(model);
        
        return !validationResult.IsValid 
            ? validationResult.Errors.Select(x => Error.Validation(x.PropertyName, x.ErrorMessage)).ToList()
            : null;
    }

    public bool CheckIfValid<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = Validate(model);
        
        return validationResult.IsValid;
    }
    
    public async Task<bool> CheckIfValidAsync<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(model, paramName);

        var validationResult = await ValidateAsync(model);
        
        return validationResult.IsValid;
    }

    private ValidationResult Validate<T>(T model)
    {
        var validator = GetValidator<T>();

        return validator.Validate(model);
    }
    
    private async Task<ValidationResult> ValidateAsync<T>(T model)
    {
        var validator = GetValidator<T>();

        return await validator.ValidateAsync(model);
    }
    
    private IValidator<T> GetValidator<T>()
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        return validator ?? throw new InvalidOperationException($"Validator for type {typeof(T).FullName} was not found. Check if it's already registered.");
    }
}
