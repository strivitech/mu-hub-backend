namespace MuHub.Application.Models.Requests.User;

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
