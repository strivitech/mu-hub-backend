using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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

    public bool CheckIfValid<T>(T model)
    {
        if (model is null)
        {
            return false;
        }

        var validationResult = Validate(model);
        
        return validationResult.IsValid;
    }
    
    public async Task<bool> CheckIfValidAsync<T>(T model)
    {
        if (model is null)
        {
            return false;
        }

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
