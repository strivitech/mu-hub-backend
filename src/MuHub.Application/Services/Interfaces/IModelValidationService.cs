using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using ErrorOr;

using MuHub.Application.Exceptions;

namespace MuHub.Application.Services.Interfaces;

/// <summary>
/// Validates input models.
/// </summary>
public interface IModelValidationService
{
    /// <summary>
    /// Validates the given model and throws an appropriate exception of type <see cref="ModelValidationException"/> if invalid.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    void EnsureValid<T>([NotNull]T model, [CallerArgumentExpression("model")]string? paramName = null);
    
    /// <summary>
    /// Validates the given model and throws an appropriate exception of type <see cref="ModelValidationException"/> if invalid.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// </returns>
    Task EnsureValidAsync<T>([NotNull]T model, [CallerArgumentExpression("model")]string? paramName = null);

    /// <summary>
    /// Determines whether the model is valid or not.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>The result contains a <see cref="List{T}"/> of <see cref="Error"/> if model is incorrect; otherwise, null.</returns>
    List<Error>? DetermineIfValid<T>(
        [NotNull] T model,
        [CallerArgumentExpression("model")] string? paramName = null);

    
    /// <summary>
    /// Asynchronously determines whether the model is valid or not.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains a <see cref="List{T}"/> of <see cref="Error"/> if model is incorrect; otherwise, null.
    /// </returns>
    Task<List<Error>?> DetermineIfValidAsync<T>(
        [NotNull] T model,
        [CallerArgumentExpression("model")] string? paramName = null);
    
    /// <summary>
    /// Checks whether the model is valid or not.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>The result contains true if valid; otherwise, false.</returns>
    
    bool CheckIfValid<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null);
    
    /// <summary>
    /// Asynchronously checks whether the model is valid or not.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains true if valid; otherwise, false.
    /// </returns>
    Task<bool> CheckIfValidAsync<T>([NotNull]T model, [CallerArgumentExpression("model")] string? paramName = null);
}
