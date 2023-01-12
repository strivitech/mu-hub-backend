using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuHub.IdentityProvider.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Challenge : PageModel
{
    private readonly IIdentityServerInteractionService _interactionService;

    public Challenge(IIdentityServerInteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    /// <summary>
    /// Provides a challenge step for the external authentication.
    /// </summary>
    /// <param name="scheme">Authentication scheme.</param>
    /// <param name="returnUrl">Url to return after authentication.</param>
    public IActionResult OnGet(string scheme, string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = "~/";
        }

        // validate returnUrl - either it is a valid OIDC URL
        if (!_interactionService.IsValidReturnUrl(returnUrl))
        {
            // user might have clicked on a malicious link - should be logged
            throw new InvalidOperationException("invalid return URL");
        }

        // start challenge and roundtrip the return URL and scheme 
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Page("/externallogin/callback"),
            Items = { { "returnUrl", returnUrl }, { "scheme", scheme }, }
        };

        return Challenge(props, scheme);
    }
}
