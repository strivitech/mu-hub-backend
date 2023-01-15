namespace MuHub.IdentityProvider.Configurations.Auth;

/// <summary>
/// Google authentication configuration.
/// </summary>
public class GoogleConfiguration
{
    public const string SectionName = "Authentication:Google";
    
    /// <summary>
    /// Client ID.
    /// </summary>
    public string ClientId { get; set; }
    
    /// <summary>
    /// Client secret.
    /// </summary>
    public string ClientSecret { get; set; }
}
