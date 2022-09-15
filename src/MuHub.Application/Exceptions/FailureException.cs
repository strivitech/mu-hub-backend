namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown when a failure occured while processing domain.
/// </summary>
[Serializable]
public abstract class FailureException : DomainException
{
    protected FailureException()
    {
    }

    protected FailureException(string? message) 
        : base(message)
    {
    }

    protected FailureException(string? message, Dictionary<string, string[]> additionalContext) 
        : base(message, additionalContext)
    {
    }

    protected FailureException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }

    protected FailureException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException) 
        : base(message, additionalContext, innerException)
    {
    }
}
