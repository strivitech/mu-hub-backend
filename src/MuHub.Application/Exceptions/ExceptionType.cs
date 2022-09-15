namespace MuHub.Application.Exceptions;

/// <summary>
/// Exception type for domain exceptions.
/// </summary>
[Serializable]
public enum ExceptionType : byte
{
    ClientFailure = 0,
    LogicFailure = 1,
    Unexpected = 2,
    Validation = 3,
    Conflict = 4,
    NotFound = 5,
}
