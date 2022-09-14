namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown when a failure occured while processing domain.
/// </summary>
[Serializable]
public class FailureException : DomainException
{
    public FailureException()
    {
    }

    public FailureException(Exception ex) : this("Unhandled exception", ex)
    {
    }

    public FailureException(string? message) 
        : base(message)
    {
    }
    
    public FailureException(string? message, Dictionary<string, string[]> additionalContext) 
        : base(message, additionalContext)
    {
    }

    public FailureException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }
    
    public FailureException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException) 
        : base(message, additionalContext, innerException)
    {
    }

    public override ExceptionType ExceptionType => ExceptionType.Failure;
}
