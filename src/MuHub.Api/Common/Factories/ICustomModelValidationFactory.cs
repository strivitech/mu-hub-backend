using FluentValidation;

namespace MuHub.Api.Common.Factories;

/// <summary>
/// 
/// </summary>
public interface ICustomModelValidationFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IValidator? GetValidator(Type type);
}
