using FluentAssertions;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuHub.Tests.Common.Helpers;

/// <summary>
/// A helper class for testing Razor Pages. Contains methods for the asserting part.
/// </summary>
public static class AssertHelper
{
    /// <summary>
    /// Asserts that the <see cref="PageModel"/> model state has a single validation error for the specified property.
    /// </summary>
    /// <param name="page">Page.</param>
    /// <param name="expectedError">Expected error.</param>
    public static void PageModelStateContainsSingleError(this PageModel page, (string key, string errorMessage) expectedError)
    {
        var error = page.ModelState.Single();
        error.Key.Should().BeEquivalentTo(string.Empty);
        error.Value!.Errors.Single().ErrorMessage.Should().BeEquivalentTo(expectedError.errorMessage);
    }
}
