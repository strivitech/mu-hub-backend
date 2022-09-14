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

    protected DomainException(string? message) : base(message)
    {
        Details = message;
    }
    
    protected DomainException(string? message, Dictionary<string, string[]> additionalContext) : base(message)
    {
        Details = message;
        AdditionalContext = additionalContext;
    }

    protected DomainException(string? message, Exception innerException) : base(message, innerException)
    {
        Details = message;
    }
    
    protected DomainException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException)
        : base(message, innerException)
    {
        Details = message;
        AdditionalContext = additionalContext;
    }

    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public string? Details { get; protected set; }
    
    public Dictionary<string, string[]>? AdditionalContext { get; protected set; }
    
    public abstract ExceptionType ExceptionType { get; }
}
