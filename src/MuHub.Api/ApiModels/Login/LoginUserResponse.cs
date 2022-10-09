namespace MuHub.Api.ApiModels.Login;

/// <summary>
/// 
/// </summary>
public class LoginUserResponse
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
    /// Gets or sets the role name for this user.
    /// </summary>
    public string RoleName { get; set; } = null!;
}
