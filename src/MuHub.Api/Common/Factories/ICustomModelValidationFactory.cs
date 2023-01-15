using FluentValidation;

namespace MuHub.Api.Common.Factories;

/// <summary>
/// Factory for creating a new instance of a validator.
/// </summary>
public interface ICustomModelValidationFactory
{
    /// <summary>
    /// Gets the validator for the specified type.
    /// </summary>
    /// <param name="type">Particular type.</param>
    /// <returns>An instance of the validator if find or null.</returns>
    IValidator? GetValidator(Type type);
}
