using System.Net;

using CoinGecko.Api.Features.Coins;

using FluentAssertions;

using FluentResults;

using Moq;

using MuHub.Market.Proxy.Features.Coins.Mapping;

namespace MuHub.Market.Proxy.Features.Coins;

public class CoinsDataServiceTest
{
    private readonly Mock<ICoinsDataProvider> _coinsDataProviderMock;
    private readonly CoinsDataService _coinsDataService;

    public CoinsDataServiceTest()
    {
        _coinsDataProviderMock = new Mock<ICoinsDataProvider>();
        _coinsDataService = new CoinsDataService(_coinsDataProviderMock.Object);
    }

    [Fact]
    public async Task GetMarketCoinListAsync_WhenResultIsSuccessful_ReturnsMarketCoinList()
    {
        // Arrange
        var providerMarketCoinsList = GetMarketCoinList();
        var expectedCoinList = providerMarketCoinsList.ToMarketCoinDtoList();
        _coinsDataProviderMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<CoinGecko.Api.Features.Coins.GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Ok(providerMarketCoinsList));

        // Act
        var result = await _coinsDataService.GetMarketCoinListAsync(new GetMarketCoinRequest());

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedCoinList);
    }
    
    [Fact]
    public async Task GetMarketCoinListAsync_WhenResultIsFailed_ReturnsMarketCoinList()
    {
        // Arrange
        const string providerError = "Provider error";
        _coinsDataProviderMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<CoinGecko.Api.Features.Coins.GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Fail(providerError));

        // Act
        var result = await _coinsDataService.GetMarketCoinListAsync(new GetMarketCoinRequest());

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be(providerError);
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
