namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception type for domain exceptions.
/// </summary>
[Serializable]
public enum ExceptionType : byte
{
    Failure = 0,
    Unexpected = 1,
    Validation = 2,
    Conflict = 3,
    NotFound = 4,
}
