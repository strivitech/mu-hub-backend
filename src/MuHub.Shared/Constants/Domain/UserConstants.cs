namespace MuHub.Shared.Constants.Domain;

/// <summary>
/// Contains constants for the user.
/// </summary>
public static class UserConstants
{
    /// <summary>
    /// Login length.
    /// </summary>
    public const int LoginLength = 50;
    
    /// <summary>
    /// Regex for the password.
    /// </summary>
    public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

    /// <summary>
    /// Regex for the phone. Contains 6-14 numbers from 0-9 range, doesn't include + sign.
    /// </summary>
    public const string PhoneNumberRegex = @"^[0-9]{6,14}$";
    
    /// <summary>
    /// Default user role.
    /// </summary>
    public const string DefaultRoleName = "user";
}
