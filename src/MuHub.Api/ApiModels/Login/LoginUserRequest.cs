namespace MuHub.Api.ApiModels.Login;

/// <summary>
/// 
/// </summary>
public class LoginUserRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// User Password.
    /// </summary>
    public string Password { get; set; } = null!;
}
