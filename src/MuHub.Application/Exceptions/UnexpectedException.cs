namespace MuHub.Application.Exceptions;

[Serializable]
public class UnexpectedException : DomainException
{
    public UnexpectedException(Exception ex) : this("Unhandled exception", ex)
    {
    }

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
