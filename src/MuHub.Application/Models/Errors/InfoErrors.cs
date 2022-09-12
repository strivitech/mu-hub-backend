using ErrorOr;

namespace MuHub.Application.Models.Errors;

/// <summary>
/// Here we define the error codes that are used in the application.
/// </summary>
public static partial class Errors
{
    /// <summary>
    /// Informational errors.
    /// </summary>
    public static class Info
    {
        /// <summary>
        /// The operation was not successful because of invalid input.
        /// </summary>
        public static Error InvalidInput => 
            Error.Validation("Info.InvalidInput", "Invalid input");
    }
}
