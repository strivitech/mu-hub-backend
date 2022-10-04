using FluentValidation;

namespace MuHub.Api.Common.Factories;

/// <summary>
/// 
/// </summary>
public class CustomModelValidationFactory : ICustomModelValidationFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public CustomModelValidationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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
