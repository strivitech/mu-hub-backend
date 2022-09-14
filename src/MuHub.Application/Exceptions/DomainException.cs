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
    
    protected DomainException(string? message, Dictionary<string, string[]> additionalContext) : base(message)
    {
        AdditionalContext = additionalContext;
    }

    protected DomainException(string? message, Exception innerException) : base(message, innerException)
    {
    }
    
    protected DomainException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException)
        : base(message, innerException)
    {
        AdditionalContext = additionalContext;
    }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public Dictionary<string, string[]>? AdditionalContext { get; protected set; }
    
    public abstract ExceptionType ExceptionType { get; }
}
