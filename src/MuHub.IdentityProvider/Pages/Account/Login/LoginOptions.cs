namespace MuHub.IdentityProvider.Pages.Account.Login;

public static class LoginOptions
{
    public const bool AllowLocalLogin = true;
    public const bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration { get; } = TimeSpan.FromDays(30);
    public const string InvalidCredentialsErrorMessage = "Invalid username or password";
}
