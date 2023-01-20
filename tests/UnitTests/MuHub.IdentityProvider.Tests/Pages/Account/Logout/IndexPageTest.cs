using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;

using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Moq;

using MuHub.IdentityProvider.Models;
using MuHub.Tests.Common.Helpers;
using MuHub.Tests.Common.Identity;

using Index = MuHub.IdentityProvider.Pages.Account.Logout.Index;

namespace MuHub.IdentityProvider.Tests.Pages.Account.Logout;

public class IndexPageTest
{
    private const string ValidReturnUri = "https://localhost:3000/main";
    private const string ValidLogoutId = "logoutId";
    private const string LoggedOutRedirectUrl = "/Account/Logout/LoggedOut";
    private const string ErrorRedirectUrl = "~/Home/Error/Index";
    private const string LoginButton = "login";
    private const string NotLoginButton = "notLogin";
    private readonly Mock<IIdentityServerInteractionService> _interactionServiceMock;
    private readonly Mock<IEventService> _eventServiceMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<ClaimsIdentity> _identityMock;
    private readonly Mock<ClaimsPrincipal> _claimsPrincipalMock;
    private readonly Index _page;

    public IndexPageTest()
    {
        _interactionServiceMock = new Mock<IIdentityServerInteractionService>();
        _signInManagerMock = SignInManagerCreator.CreateMockedSignInManager<ApplicationUser>();
        _eventServiceMock = new Mock<IEventService>();

        _page = new Index(
            _signInManagerMock.Object,
            _interactionServiceMock.Object,
            _eventServiceMock.Object
        ).WithDefaultValues();
        
        _identityMock = new Mock<ClaimsIdentity>();
        _identityMock.Setup(x => x.Name).Returns("IdentityName");
        _claimsPrincipalMock = new Mock<ClaimsPrincipal>();
        _claimsPrincipalMock.Setup(x => x.Identity).Returns(_identityMock.Object);
        _page.HttpContext.User = _claimsPrincipalMock.Object;
    }
    
    [Fact]
    public async Task OnGet_WhenUserIsNotAuthenticated_ReturnsRedirectToPageResult()
    {
        // Arrange
        _identityMock.SetupGet(x => x.IsAuthenticated).Returns(false);

        // Act
        var result = await _page.OnGet(ValidLogoutId);

        // Assert
        result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be(LoggedOutRedirectUrl);
    }
    
    [Fact]
    public async Task OnGet_WhenUserIsAuthenticatedAndShowSignOutPromptIsTrue_ReturnsPageResult()
    {
        // Arrange
        _identityMock.SetupGet(x => x.IsAuthenticated).Returns(true);
        _interactionServiceMock.Setup(x => x.GetLogoutContextAsync(ValidLogoutId))
            .ReturnsAsync(new LogoutRequest(null, new LogoutMessage()) { ClientId = null });

        // Act
        var result = await _page.OnGet(ValidLogoutId);

        // Assert
        result.Should().BeOfType<PageResult>();
    }
    
    [Fact]
    public async Task OnGet_WhenUserIsLocalAuthenticatedAndShowSignOutPromptIsFalse_ReturnsRedirectToPageResult()
    {
        // Arrange
        var localLoginClaim = new Claim("local", "local");
        var subClaim = new Claim("sub", "sub");
        const string clientId = "clientId";
        _identityMock.SetupGet(x => x.IsAuthenticated).Returns(true);
        _interactionServiceMock.Setup(x => x.GetLogoutContextAsync(ValidLogoutId))
            .ReturnsAsync(new LogoutRequest(null, new LogoutMessage()) { ClientId = clientId });
        _interactionServiceMock.Setup(x => x.CreateLogoutContextAsync())
            .ReturnsAsync(ValidLogoutId);
        _signInManagerMock.Setup(x => x.SignOutAsync())
            .Returns(Task.CompletedTask);
        _eventServiceMock.Setup(x => x.RaiseAsync(It.IsAny<Event>()))
            .Returns(Task.CompletedTask);
        // _claimsPrincipalMock.Setup(x => x.FindFirst(JwtClaimTypes.IdentityProvider))
        //     .Returns(localLoginClaim);
        _identityMock.Setup(x => x.FindFirst(JwtClaimTypes.Subject))
            .Returns(subClaim);
        // _claimsPrincipalMock.Setup(x => x.FindFirst(JwtClaimTypes.Subject))
        //     .Returns(subClaim);   
        
        // Act
        var result = await _page.OnGet(ValidLogoutId);

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
    }
}
