namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown when a logic failure in the code caused incorrect behaviour.
/// </summary>
[Serializable]
public sealed class LogicFailureException : FailureException
{
    public LogicFailureException()
    {
    }

    public LogicFailureException(string? message) 
        : base(message)
    {
    }

    public LogicFailureException(string? message, Dictionary<string, string[]> additionalContext) 
        : base(message, additionalContext)
    {
    }

    public LogicFailureException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }

    public LogicFailureException(string? message, Dictionary<string, string[]> additionalContext, Exception innerException) 
        : base(message, additionalContext, innerException)
    {
    }

    public override ExceptionType ExceptionType => ExceptionType.LogicFailure;
}
