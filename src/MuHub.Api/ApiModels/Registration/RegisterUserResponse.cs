namespace MuHub.Api.ApiModels.Registration;

/// <summary>
/// Response model for user registration.
/// </summary>
public class RegisterUserResponse
{
    /// <summary>
    /// Gets or sets the primary key for this user.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets a telephone number for the user.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the role name for this user.
    /// </summary>
    public string RoleName { get; set; } = null!;

    /// <summary>
    /// Gets ot sets time when the user was created.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; }
}
