using System.ComponentModel.DataAnnotations;

namespace MuHub.Api.Contracts.Requests;

/// <summary>
/// Request model for user registration.
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    public string Login { get; set; } = null!;
    
    /// <summary>
    /// User Password.
    /// </summary>
    public string Password { get; set; } = null!;
    
    /// <summary>
    /// Repeat User Password.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// Repeat User Password.
    /// </summary>
    [Compare(nameof(Password))]
    public string RepeatPassword { get; set; } = null!;
}
