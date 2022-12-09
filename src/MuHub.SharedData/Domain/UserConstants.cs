namespace MuHub.SharedData.Domain;

/// <summary>
/// Contains constants for the user.
/// </summary>
public class UserConstants
{
    /// <summary>
    /// 
    /// </summary>
    public const string AdminRole = "Admin";
    
    /// <summary>
    /// 
    /// </summary>
    public const string UserRole = "User";

    /// <summary>
    /// Regex for the phone. Contains 6-14 numbers from 0-9 range, doesn't include + sign.
    /// </summary>
    public const string PhoneNumberRegex = @"^[0-9]{6,14}$";

    /// <summary>
    /// Default user role.
    /// </summary>
    public const string DefaultRoleName = UserRole;
}
