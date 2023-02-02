using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MuHub.Api.Controllers;

/// <summary>
/// Error controller that is used to handle all unhandled exceptions.
/// </summary>
public class ErrorController : MuControllerBase
{
    /// <summary>
    /// Handles all unhandled exceptions.
    /// </summary>
    /// <returns>An instance for <see cref="IActionResult"/>.</returns>
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(exception);
    }
}

