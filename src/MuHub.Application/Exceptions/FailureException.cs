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

    public FailureException(string? objectName)
    {
        ObjectName = objectName;
    }
    
    public FailureException(string? objectName, string? message) 
        : base(message)
    {
        ObjectName = objectName;
    }

    public FailureException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }
    
    public FailureException(string? objectName, string? message, Exception innerException) 
        : base(message, innerException)
    {
        ObjectName = objectName;
    }

    public string? ObjectName { get; } 
    
    public override ExceptionType ExceptionType => ExceptionType.Failure;
}
