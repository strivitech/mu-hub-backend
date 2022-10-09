namespace MuHub.Api.ApiModels.Registration;

/// <summary>
/// Request model for user registration.
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Repeat User Password.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// User Password.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Repeat User Password.
    /// </summary>
    public string RepeatPassword { get; set; } = null!;
}
