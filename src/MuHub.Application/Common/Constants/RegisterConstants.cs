namespace MuHub.Application.Common.Constants;

/// <summary>
/// Contains constants for the user registration.
/// </summary>
public static class RegisterConstants
{
    /// <summary>
    /// Regex for the password.
    /// </summary>
    public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
}
