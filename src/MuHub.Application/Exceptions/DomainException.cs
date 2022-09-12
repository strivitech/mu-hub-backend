using System.Runtime.Serialization;

namespace MuHub.Application.Exceptions;

/// <summary>
/// Base exception type for domain exceptions.
/// </summary>
[Serializable]
public abstract class DomainException : Exception
{
    protected DomainException()
    {
    }

    protected DomainException(Exception ex) : this("Unhandled exception", ex)
    {
    }

    protected DomainException(string? message) : base(message)
    {
    }

    protected DomainException(string? message, Exception innerException) : base(message, innerException)
    {
    }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public abstract ExceptionType ExceptionType { get; }
}
