using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuHub.IdentityProvider.Pages.Redirect;

/// <summary>
/// Redirect Index page.
/// </summary>
[AllowAnonymous]
public class IndexModel : PageModel
{
    public string RedirectUri { get; set; }

    /// <summary>
    /// Gets the page with the redirect uri.
    /// </summary>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public IActionResult OnGet(string redirectUri)
    {
        RedirectUri = redirectUri;
        return Page();
    }
}
