namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown when a client failure caused incorrect behaviour.
/// </summary>
[Serializable]
public sealed class ClientFailureException : FailureException
{
    public ClientFailureException()
    {
    }

    public ClientFailureException(string? message) 
        : base(message)
    {
    }

    public ClientFailureException(string? message, Dictionary<string, string[]> additionalContext) 
        : base(message, additionalContext)
    {
    }

    public ClientFailureException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }

    public ClientFailureException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException) 
        : base(message, additionalContext, innerException)
    {
    }

    public override ExceptionType ExceptionType => ExceptionType.ClientFailure;
}
