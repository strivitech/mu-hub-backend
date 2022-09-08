using System.ComponentModel.DataAnnotations;

namespace MuHub.Infrastructure.Contracts.Requests;

public class CreateUserRequest
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
    /// Phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    public string RoleName { get; set; } = null!;
}
