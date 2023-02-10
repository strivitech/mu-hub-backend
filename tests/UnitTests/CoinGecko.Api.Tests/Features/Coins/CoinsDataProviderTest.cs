using System.Net;
using System.Text.Json;

using CoinGecko.Api.Common;
using CoinGecko.Api.Features.Coins;

using FluentAssertions;

using FluentResults;

using Microsoft.Extensions.Logging;

using Moq;

namespace CoinGecko.Api.Tests.Features.Coins;

public class CoinsDataProviderTest
{
    private readonly Mock<IRequestCoordinator> _requestCoordinator;
    private readonly Mock<ILogger<CoinsDataProvider>> _logger;
    private readonly CoinsDataProvider _coinsDataProvider;

    public CoinsDataProviderTest()
    {
        _requestCoordinator = new Mock<IRequestCoordinator>();
        _logger = new Mock<ILogger<CoinsDataProvider>>();
        _coinsDataProvider = new CoinsDataProvider(_requestCoordinator.Object, _logger.Object);
    }

    [Fact]
    public async Task GetCoinListAsync_WhenIncludePlatformIsFalse_ReturnsSuccessfulResultWithCoinList()
    {
        // Arrange
        var coinList = GetCoinListWithoutPlatform();
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(coinList))
            });

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(coinList);
    }

    [Fact]
    public async Task GetCoinListAsync_WhenIncludePlatformIsTrue_ReturnsSuccessfulResultWithCoinList()
    {
        // Arrange
        var coinList = new List<Coin>
        {
            new() { Id = "bitcoin", Symbol = "btc", Name = "Bitcoin", Platforms = new Dictionary<string, string>
            {
                ["platform"] = "platform-id"
            } },
        };
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(coinList))
            });

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(coinList);
    }
    
    [Fact]
    public async Task GetCoinListAsync_WhenRequestCoordinatorFailed_ReturnsFailedResultWithReason()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = reason,
            });

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(reason);
    }

    [Fact]
    public async Task GetCoinListAsync_WhenRequestCoordinatorReturnsFailedStatusCode_ReturnsFailedResultWithReason()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = reason,
            });

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(reason);
    }
    
    [Fact]
    public async Task GetCoinListAsync_WhenRequestCoordinatorThrowsException_ReturnsFailedResultWithExceptionalError()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ThrowsAsync(new Exception(reason));

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().As<ExceptionalError>().Message.Should().Be(reason);
    }
    
    private static List<Coin> GetCoinListWithoutPlatform()
    {
        return new List<Coin>
        {
            new() { Id = "bitcoin", Symbol = "btc", Name = "Bitcoin" },
            new() { Id = "ethereum", Symbol = "eth", Name = "Ethereum" },
            new() { Id = "binancecoin", Symbol = "bnb", Name = "Binance Coin" },
        };
    }
}
