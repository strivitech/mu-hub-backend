namespace MuHub.Application.Models.Responses;

/// <summary>
/// 
/// </summary>
public class GetIdentityProviderUserResponse
{
    /// <summary>
    /// 
    /// </summary>
    public string? IdentityProviderId { get; set; }  
        
    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// </summary>
    /// <value>True if the email address has been confirmed, otherwise false.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a telephone number for the user.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their telephone address.
    /// </summary>
    /// <value>True if the telephone number has been confirmed, otherwise false.</value>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}
