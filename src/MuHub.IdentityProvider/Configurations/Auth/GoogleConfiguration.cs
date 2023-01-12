namespace MuHub.IdentityProvider.Configurations.Auth;

public class GoogleConfiguration
{
    public const string SectionName = "Authentication:Google";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
