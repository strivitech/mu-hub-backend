namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception thrown when an object is not found.
/// </summary>
[Serializable]
public sealed class NotFoundException : DomainException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string? objectName)
    {
        ObjectName = objectName;
    }
    
    public NotFoundException(string? objectName, string? message) 
        : base(message)
    {
        ObjectName = objectName;
    }

    public NotFoundException(string? message, Exception innerException) 
        : base(message, innerException)
    {
    }
    
    public NotFoundException(string? objectName, string? message, Exception innerException) 
        : base(message, innerException)
    {
        ObjectName = objectName;
    }

    public string? ObjectName { get; } 
    
    public override ExceptionType ExceptionType => ExceptionType.NotFound;
}
