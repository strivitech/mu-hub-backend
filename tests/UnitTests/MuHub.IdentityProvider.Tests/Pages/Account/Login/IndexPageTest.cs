using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Moq;

using MuHub.IdentityProvider.Models;
using MuHub.IdentityProvider.Pages.Account.Login;
using MuHub.Tests.Common.Helpers;
using MuHub.Tests.Common.Identity;

using Index = MuHub.IdentityProvider.Pages.Account.Login.Index;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MuHub.IdentityProvider.Tests.Pages.Account.Login;

/// <summary>
/// Contains tests for the <see cref="Index"/> page.
/// </summary>
public class IndexPageTest
{
    private const string ValidReturnUri = "https://localhost:3000/main";
    private const string ErrorRedirectUrl = "~/Home/Error/Index";
    private const string LoginButton = "login";
    private const string NotLoginButton = "notLogin";
    private readonly Mock<IIdentityServerInteractionService> _interactionServiceMock;
    private readonly Mock<IAuthenticationSchemeProvider> _authenticationSchemeProviderMock;
    private readonly Mock<IIdentityProviderStore> _identityProviderStoreMock;
    private readonly Mock<IEventService> _eventServiceMock;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<ILogger<Index>> _loggerMock;
    private readonly Index _page;

    public IndexPageTest()
    {
        _interactionServiceMock = new Mock<IIdentityServerInteractionService>();
        _authenticationSchemeProviderMock = new Mock<IAuthenticationSchemeProvider>();
        _identityProviderStoreMock = new Mock<IIdentityProviderStore>();
        _eventServiceMock = new Mock<IEventService>();
        _userManagerMock = UserManagerCreator.CreateMockedUserManager<ApplicationUser>();
        _signInManagerMock = SignInManagerCreator.CreateMockedSignInManager<ApplicationUser>();
        _loggerMock = new Mock<ILogger<Index>>();

        _page = new Index(
            _interactionServiceMock.Object,
            _authenticationSchemeProviderMock.Object,
            _identityProviderStoreMock.Object,
            _eventServiceMock.Object,
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _loggerMock.Object
        )
        {
            Input = new InputModel(),
            View = new ViewModel()
        }.WithDefaultValues();
    }

    [Fact]
    public async Task OnGet_WhenIsNotExternalLoginOnly_ReturnsPageResult()
    {
        // Arrange
        AuthorizationRequest context = new();
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);
        _authenticationSchemeProviderMock.Setup(x => x.GetSchemeAsync(context.IdP))
            .ReturnsAsync((AuthenticationScheme?)null);
        _authenticationSchemeProviderMock.Setup(x => x.GetAllSchemesAsync())
            .ReturnsAsync(CreateAuthenticationSchemes());
        _identityProviderStoreMock.Setup(x => x.GetAllSchemeNamesAsync())
            .ReturnsAsync(CreateIdentityProviderNames());

        // Act
        var result = await _page.OnGet(ValidReturnUri);

        // Assert
        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public async Task OnGet_WhenIsExternalLoginOnly_ReturnsRedirectToPageResult()
    {
        // Arrange
        const string redirectToPageUrl = "/ExternalLogin/Challenge";
        AuthorizationRequest context = new() { IdP = "singleExternalProvider" };
        AuthenticationScheme scheme = new("singleExternalProviderScheme1", "singleExternalProviderScheme1",
            typeof(IAuthenticationHandler));
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);
        _authenticationSchemeProviderMock.Setup(x => x.GetSchemeAsync(context.IdP))
            .ReturnsAsync(scheme);

        // Act
        var result = await _page.OnGet(ValidReturnUri);

        // Assert
        result.Should().BeOfType<RedirectToPageResult>().Which.PageName.Should().Be(redirectToPageUrl);
    }

    [Fact]
    public async Task
        OnPost_WhenButtonIsNotLoginAndAuthorizationRequestIsNotNull_DeniesAuthorizationAndReturnsRedirectResultToReturnUrl()
    {
        // Arrange
        _page.Input.Button = NotLoginButton;
        _page.Input.ReturnUrl = ValidReturnUri;
        AuthorizationRequest context = new();
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);
        _interactionServiceMock.Setup(x => x.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied, null))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _page.OnPost();

        // Assert
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(_page.Input.ReturnUrl);
    }

    [Fact]
    public async Task
        OnPost_WhenButtonIsNotLoginAndAuthorizationRequestIsNull_ReturnsRedirectResultToErrorPage()
    {
        // Arrange
        _page.Input.Button = NotLoginButton;
        _page.Input.ReturnUrl = ValidReturnUri;
        AuthorizationRequest? context = null;
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);

        // Act
        var result = await _page.OnPost();

        // Assert
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(ErrorRedirectUrl);
    }

    [Fact]
    public async Task
        OnPost_WhenModelStateIsNotValid_ReturnsPageResultWithErrors()
    {
        // Arrange
        _page.Input.Button = LoginButton;
        _page.Input.ReturnUrl = ValidReturnUri;
        (string, string) modelError = (string.Empty, "error");
        _page.ModelState.AddModelError(modelError.Item1, modelError.Item2);
        AuthorizationRequest context = new();
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);
        _authenticationSchemeProviderMock.Setup(x => x.GetSchemeAsync(context.IdP))
            .ReturnsAsync((AuthenticationScheme?)null);
        _authenticationSchemeProviderMock.Setup(x => x.GetAllSchemesAsync())
            .ReturnsAsync(CreateAuthenticationSchemes());
        _identityProviderStoreMock.Setup(x => x.GetAllSchemeNamesAsync())
            .ReturnsAsync(CreateIdentityProviderNames());

        // Act
        var result = await _page.OnPost();

        // Assert
        result.Should().BeOfType<PageResult>();
        _page.PageModelStateContainsSingleError(modelError);
    }

    [Fact] 
    public async Task
        OnPost_WhenModelStateIsValidAndPasswordSignInResultIsSuccess_ReturnsRedirectToPageResult()
    {
        // Arrange
        _page.Input.Button = LoginButton;
        _page.Input.ReturnUrl = ValidReturnUri;
        AuthorizationRequest context = new() { Client = new Client{ ClientId = "client" } };
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);

        _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new ApplicationUser());
        _eventServiceMock.Setup(x => x.RaiseAsync(It.IsAny<Event>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _page.OnPost();

        // Assert
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(ValidReturnUri);
    }
    
    [Fact]
    public async Task
        OnPost_WhenModelStateIsValidAndPasswordSignInResultIsFailed_ReturnsPageResultWithErrors()
    {
        // Arrange
        _page.Input.Button = LoginButton;
        _page.Input.ReturnUrl = ValidReturnUri;
        AuthorizationRequest context = new() { Client = new Client{ ClientId = "client" } };
        _interactionServiceMock.Setup(x => x.GetAuthorizationContextAsync(ValidReturnUri))
            .ReturnsAsync(context);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);
        _eventServiceMock.Setup(x => x.RaiseAsync(It.IsAny<Event>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _page.OnPost();

        // Assert
        result.Should().BeOfType<PageResult>();
        _page.PageModelStateContainsSingleError((string.Empty, LoginOptions.InvalidCredentialsErrorMessage));
    }

    private static List<AuthenticationScheme> CreateAuthenticationSchemes()
    {
        return new List<AuthenticationScheme>
        {
            new("scheme1", "scheme1", typeof(IAuthenticationHandler)),
            new("scheme2", "scheme2", typeof(IAuthenticationHandler)),
            new("scheme3", "scheme3", typeof(IAuthenticationHandler))
        };
    }

    private static List<IdentityProviderName> CreateIdentityProviderNames()
    {
        return new List<IdentityProviderName>
        {
            new(){Scheme = "identityProviderScheme1", DisplayName = "identityProviderScheme1", Enabled = true},
            new(){Scheme = "identityProviderScheme2", DisplayName = "identityProviderScheme2", Enabled = true},
            new(){Scheme = "identityProviderScheme3", DisplayName = "identityProviderScheme3", Enabled = true},
        };
    }
}
