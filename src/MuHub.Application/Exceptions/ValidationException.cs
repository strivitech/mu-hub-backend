using FluentValidation.Results;

namespace MuHub.Application.Exceptions;

/// <summary>
/// An exception if a model isn't valid.
/// </summary>
[Serializable]
public class ValidationException : DomainException
{
    /// <summary>
    /// Initializes a new instance of <see cref="ValidationException"/>.
    /// </summary>
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="failures">Validation failures.</param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    /// <summary>
    /// Contains validation errors.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    public override ExceptionType ExceptionType => ExceptionType.Validation;
}
