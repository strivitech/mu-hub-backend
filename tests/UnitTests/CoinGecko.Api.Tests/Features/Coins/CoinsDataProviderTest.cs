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
            new()
            {
                Id = "bitcoin",
                Symbol = "btc",
                Name = "Bitcoin",
                Platforms = new Dictionary<string, string> { ["platform"] = "platform-id" }
            },
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
                StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = reason,
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
                StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = reason,
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

    /// <summary>
    /// //
    /// </summary>
    [Fact]
    public async Task GetMarketCoinListAsync_WhenGetMarketCoinRequestIsValid_ReturnsSuccessfulResultWithMarketCoinList()
    {
        // Arrange
        var marketCoinList = GetMarketCoinList();
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(marketCoinList))
            });

        // Act
        var result = await _coinsDataProvider.GetMarketCoinListAsync(new GetMarketCoinRequest());

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(marketCoinList);
    }

    [Fact]
    public async Task GetMarketCoinListAsync_WhenRequestCoordinatorFailed_ReturnsFailedResultWithReason()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = reason,
            });

        // Act
        var result = await _coinsDataProvider.GetMarketCoinListAsync(new GetMarketCoinRequest());

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(reason);
    }

    [Fact]
    public async Task
        GetMarketCoinListAsync_WhenRequestCoordinatorReturnsFailedStatusCode_ReturnsFailedResultWithReason()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError, ReasonPhrase = reason,
            });

        // Act
        var result = await _coinsDataProvider.GetMarketCoinListAsync(new GetMarketCoinRequest());

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(reason);
    }

    [Fact]
    public async Task
        GetMarketCoinListAsync_WhenRequestCoordinatorThrowsException_ReturnsFailedResultWithExceptionalError()
    {
        // Arrange
        const string reason = "Something went wrong";
        _requestCoordinator.Setup(x => x.GetAsync(It.IsAny<Uri>()))
            .ThrowsAsync(new Exception(reason));

        // Act
        var result = await _coinsDataProvider.GetMarketCoinListAsync(new GetMarketCoinRequest());

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

    private static List<MarketCoin> GetMarketCoinList()
    {
        return new List<MarketCoin>
        {
            new()
            {
                Id = "id1",
                Symbol = "symbol1",
                Name = "name1",
                Image = "imageUrl1",
                CurrentPrice = 10,
                MarketCap = 100,
                MarketCapRank = 1,
                TotalVolume = 1000,
                High24H = 10000,
                Low24H = 1,
                Ath = 100000,
                AthChangePercentage = 10,
                AthDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Roi = new Roi { Currency = "usd", Percentage = 10, Times = 100 }
            },
            new()
            {
                Id = "id2",
                Symbol = "symbol2",
                Name = "name2",
                Image = "imageUrl2",
                CurrentPrice = 20,
                MarketCap = 200,
                MarketCapRank = 2,
                TotalVolume = 2000,
                High24H = 20000,
                Low24H = 2,
                Ath = 200000,
                AthChangePercentage = 20,
                AthDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Roi = new Roi { Currency = "usd", Percentage = 20, Times = 200 }
            },
            new()
            {
                Id = "id3",
                Symbol = "symbol3",
                Name = "name3",
                Image = "imageUrl3",
                CurrentPrice = 30,
                MarketCap = 300,
                MarketCapRank = 3,
                TotalVolume = 3000,
                High24H = 30000,
                Low24H = 3,
                Ath = 300000,
                AthChangePercentage = 30,
                AthDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Roi = new Roi { Currency = "usd", Percentage = 30, Times = 300 }
            },
        };
    }
}
