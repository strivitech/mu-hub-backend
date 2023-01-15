using FluentValidation;

namespace MuHub.Api.Common.Factories;

/// <summary>
/// Factory for creating instances of <see cref="IValidator{T}"/>.
/// </summary>
public class CustomModelValidationFactory : ICustomModelValidationFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomModelValidationFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public CustomModelValidationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// Gets the validator for the specified type.
    /// </summary>
    /// <param name="type">Particular type.</param>
    /// <returns>An instance of <see cref="IValidator{T}"/> if find or null.</returns>
    public IValidator? GetValidator(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        
        var genericValidatorType = typeof(IValidator<>);
        var specificValidatorType = genericValidatorType.MakeGenericType(type);
        
        using var scope = _serviceProvider.CreateScope();
        var validatorInstance = (IValidator?) scope.ServiceProvider.GetService(specificValidatorType);
        return validatorInstance;
    }
}
