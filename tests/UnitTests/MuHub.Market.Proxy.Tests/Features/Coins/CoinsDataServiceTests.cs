using System.Net;

using CoinGecko.Api.Features.Coins;

using FluentAssertions;

using FluentResults;

using Moq;

using MuHub.Market.Proxy.Mapping;

namespace MuHub.Market.Proxy.Features.Coins;

public class CoinsDataServiceTest
{
    private readonly Mock<ICoinsDataProvider> _coinsDataProviderMock;
    private readonly CoinsDataService _coinsDataProvider;

    public CoinsDataServiceTest()
    {
        _coinsDataProviderMock = new Mock<ICoinsDataProvider>();
        _coinsDataProvider = new CoinsDataService(_coinsDataProviderMock.Object);
    }

    [Fact]
    public async Task GetCoinListAsync_WhenResultIsSuccessful_ReturnsCoinList()
    {
        // Arrange
        var providerCoinsList = GetProviderCoinListWithoutPlatform();
        var expectedCoinList = providerCoinsList.ToCoins();
        _coinsDataProviderMock.Setup(x => x.GetCoinListAsync(It.IsAny<bool>()))
            .ReturnsAsync(Result.Ok(providerCoinsList));

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedCoinList);
    }
    
    [Fact]
    public async Task GetCoinListAsync_WhenResultIsFailed_ReturnsCoinList()
    {
        // Arrange
        const string providerError = "Provider error";
        _coinsDataProviderMock.Setup(x => x.GetCoinListAsync(It.IsAny<bool>()))
            .ReturnsAsync(Result.Fail(providerError));

        // Act
        var result = await _coinsDataProvider.GetCoinListAsync();

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(providerError);
    }
    
    private static List<CoinGecko.Api.Features.Coins.Coin> GetProviderCoinListWithoutPlatform()
    {
        return new List<CoinGecko.Api.Features.Coins.Coin>
        {
            new() { Id = "bitcoin", Symbol = "btc", Name = "Bitcoin" },
            new() { Id = "ethereum", Symbol = "eth", Name = "Ethereum" },
            new() { Id = "binancecoin", Symbol = "bnb", Name = "Binance Coin" },
        };
    }
}
