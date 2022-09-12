﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using MuHub.Application.Exceptions;

namespace MuHub.Application.Services.Interfaces;

/// <summary>
/// Validates input models.
/// </summary>
public interface IModelValidationService
{
    /// <summary>
    /// Validates the given model and throws an appropriate exception of type <see cref="ValidationException"/> if invalid.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <param name="paramName">Parameter name.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    void EnsureValid<T>([NotNull]T model, [CallerArgumentExpression("model")]string? paramName = null);
    
    /// <summary>
    /// Validates the given model and throws an appropriate exception of type <see cref="ValidationException"/> if invalid.
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
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>The result contains true if valid; otherwise, false.</returns>
    bool CheckIfValid<T>(T model);
    
    /// <summary>
    /// Asynchronously determines whether the model is valid or not.
    /// </summary>
    /// <param name="model">Model to validate.</param>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains true if valid; otherwise, false.
    /// </returns>
    Task<bool> CheckIfValidAsync<T>(T model);
}
