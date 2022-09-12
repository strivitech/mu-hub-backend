namespace MuHub.Application.Exceptions;

[Serializable]
public class UnexpectedException : DomainException
{
    public UnexpectedException()
    {
    }

    public UnexpectedException(Exception ex) : this("Unhandled exception", ex)
    {
    }

    public UnexpectedException(string? message) 
        : base(message)
    {
    }

    public UnexpectedException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }

    public override ExceptionType ExceptionType => ExceptionType.Unexpected;
}
