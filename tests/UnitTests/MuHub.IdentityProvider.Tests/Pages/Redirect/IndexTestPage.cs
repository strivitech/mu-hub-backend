using FluentAssertions;

using Microsoft.AspNetCore.Mvc.RazorPages;

using MuHub.Tests.Common.Helpers;

using Index = MuHub.IdentityProvider.Pages.Redirect.IndexModel;

namespace MuHub.IdentityProvider.Tests.Pages.Redirect;

/// <summary>
/// Contains tests for the <see cref="Index"/> page.
/// </summary>
public class IndexModelPageTest
{
    private const string ValidRedirectUri = "validRedirectUri";
    private readonly Index _page;

    public IndexModelPageTest()
    {
        _page = new Index().WithDefaultValues();
    }

    [Fact]
    public void ReturnsPageWithSetRedirectUri()
    {
        // Arrange

        // Act
        var result = _page.OnGet(ValidRedirectUri);

        // Assert
        result.Should().BeOfType<PageResult>();
        _page.RedirectUri.Should().Be(ValidRedirectUri);
    }
}
