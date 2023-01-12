using System.ComponentModel.DataAnnotations;

namespace MuHub.IdentityProvider.Pages.Account.Login;

/// <summary>
/// Input model for the login page.
/// </summary>
public class InputModel
{
    /// <summary>
    /// Username.
    /// </summary>
    [Required] 
    public string Username { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Determines whether remember login or not.
    /// </summary>
    public bool RememberLogin { get; set; }

    /// <summary>
    /// Url to return after the login flow.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Determines the type of button user clicked.
    /// </summary>
    public string Button { get; set; }
}
