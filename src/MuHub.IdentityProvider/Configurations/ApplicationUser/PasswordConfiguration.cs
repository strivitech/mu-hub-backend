namespace MuHub.IdentityProvider.Configurations.ApplicationUser;

public static class PasswordConfiguration
{
    public const bool RequireDigit = true;
    public const bool RequireLowercase = true;
    public const bool RequireNonAlphanumeric = true;
    public const bool RequireUppercase = true;
    public const int RequiredLength = 8;
}
