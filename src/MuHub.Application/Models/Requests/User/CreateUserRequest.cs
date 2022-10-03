using System.ComponentModel.DataAnnotations;

using MuHub.Application.Common.Constants;

namespace MuHub.Application.Models.Requests.User;

public class CreateUserRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    [StringLength(25, MinimumLength = 3)]
    [EmailAddress]
    public string Login { get; set; } = null!;

    /// <summary>
    /// User Password.
    /// </summary>
    [RegularExpression(
        RegisterConstants.PasswordRegex,
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Phone number.
    /// </summary>
    [Phone]
    public string PhoneNumber { get; set; } = null!;
    
    public string RoleName { get; set; } = null!;
}
