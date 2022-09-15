namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown whenever an unexpected domain situation.
/// </summary>
[Serializable]
public sealed class UnexpectedException : DomainException
{
    public UnexpectedException(string? message) 
        : base(message)
    {
    }
    
    public UnexpectedException(string? message, Dictionary<string, string[]> additionalContext) 
        : base(message, additionalContext)
    {
    }

    public UnexpectedException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }
    
    public UnexpectedException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException) 
        : base(message, additionalContext, innerException)
    {
    }

    public override ExceptionType ExceptionType => ExceptionType.Unexpected;
}
