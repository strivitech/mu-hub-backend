using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

using Microsoft.AspNetCore.Http;

using MuHub.Api.Controllers.V1;
using MuHub.Application.Contracts.Infrastructure;
using MuHub.Application.Models.Data.WatchList;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Api.Tests.Controllers;

public class WatchListControllerTests
{
    private readonly WatchListController _watchListController;
    private readonly Mock<IWatchListService> _watchListServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public WatchListControllerTests()
    {
        _watchListServiceMock = new Mock<IWatchListService>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _watchListController = new WatchListController(_watchListServiceMock.Object, _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task AddToWatchList_ReturnsOk_WhenCoinAddedToWatchList()
    {
        // Arrange
        var request = new AddToWatchListRequest { CoinId = 1 };
        const string userId = "ValidUserId";

        _currentUserServiceMock.Setup(x => x.UserSessionData.UserId).Returns(userId);
        _watchListServiceMock.Setup(x => x.AddToWatchListAsync(request.CoinId, userId)).ReturnsAsync(true);

        // Act
        var result = await _watchListController.AddToWatchList(request);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().Be("Added to watch list");
    }

    [Fact]
    public async Task AddToWatchList_ReturnsBadRequest_WhenFailedToAddToWatchList()
    {
        // Arrange
        var request = new AddToWatchListRequest { CoinId = 1 };
        const string userId = "ValidUserId";

        _currentUserServiceMock.Setup(x => x.UserSessionData.UserId).Returns(userId);
        _watchListServiceMock.Setup(x => x.AddToWatchListAsync(request.CoinId, userId)).ReturnsAsync(false);

        // Act
        var result = await _watchListController.AddToWatchList(request);

        // Assert
        var badRequestResult = result as ObjectResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        badRequestResult?.Value.As<ProblemDetails>().Detail.Should().Be("Failed to add to watch list");
    }
}

