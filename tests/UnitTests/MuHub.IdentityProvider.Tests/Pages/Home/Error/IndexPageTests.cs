using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using MuHub.IdentityProvider.Models;
using MuHub.Tests.Common.Helpers;
using MuHub.Tests.Common.Identity;

using Index = MuHub.IdentityProvider.Pages.Home.Error.Index;

namespace MuHub.IdentityProvider.Tests.Pages.Home.Error;

/// <summary>
/// Contains tests for the <see cref="Index"/> page.
/// </summary>
public class IndexPageTest
{
    private const string ValidErrorId = "validErrorId";
    private readonly Mock<IIdentityServerInteractionService> _interactionServiceMock;
    private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
    private readonly Index _page;

    public IndexPageTest()
    {
        _interactionServiceMock = new Mock<IIdentityServerInteractionService>();
        _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        
        _page = new Index(
            _interactionServiceMock.Object,
            _webHostEnvironmentMock.Object
        ).WithDefaultValues();
    }

    [Fact]
    public async Task OnGet_WhenErrorContextExists_SetsViewError()
    {
        // Arrange
        var errorMessage = new ErrorMessage();
        _interactionServiceMock.Setup(x => x.GetErrorContextAsync(ValidErrorId))
            .ReturnsAsync(errorMessage);

        // Act
        await _page.OnGet(ValidErrorId);
        
        // Assert
        _page.View.Error.Should().BeSameAs(errorMessage);
    }
    
    [Fact]
    public async Task OnGet_WhenEnvironmentIsDevelopment_SetsViewErrorWithDescription()
    {
        // Arrange
        const string errorDescription = "errorDescription";
        var errorMessage = new ErrorMessage{ ErrorDescription = errorDescription };
        _interactionServiceMock.Setup(x => x.GetErrorContextAsync(ValidErrorId))
            .ReturnsAsync(errorMessage);
        _webHostEnvironmentMock.Setup(x => x.EnvironmentName).Returns(Environments.Development);

        // Act
        await _page.OnGet(ValidErrorId);
        
        // Assert
        _page.View.Error.Should().BeSameAs(errorMessage);
        _page.View.Error.ErrorDescription.Should().Be(errorDescription);
    }
    
    [Fact]
    public async Task OnGet_WhenEnvironmentIsNotDevelopment_SetsViewErrorWithoutDescription()
    {
        // Arrange
        const string errorDescription = "errorDescription";
        var errorMessage = new ErrorMessage{ ErrorDescription = errorDescription };
        _interactionServiceMock.Setup(x => x.GetErrorContextAsync(ValidErrorId))
            .ReturnsAsync(errorMessage);
        _webHostEnvironmentMock.Setup(x => x.EnvironmentName).Returns(Environments.Production);

        // Act
        await _page.OnGet(ValidErrorId);
        
        // Assert
        _page.View.Error.Should().BeSameAs(errorMessage);
        _page.View.Error.ErrorDescription.Should().BeNull();
    }
    
    [Fact]
    public async Task OnGet_WhenErrorNotFound_Returns()
    {
        // Arrange
        _interactionServiceMock.Setup(x => x.GetErrorContextAsync(It.IsAny<string>()))
            .ReturnsAsync((ErrorMessage)null!);

        // Act
        await _page.OnGet(It.IsAny<string>());
        
        // Assert
        _page.View.Error.Should().BeNull();
    }
}
