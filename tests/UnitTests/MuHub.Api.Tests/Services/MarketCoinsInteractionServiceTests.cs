using FluentAssertions;

using FluentResults;

using Microsoft.AspNetCore.SignalR;

using Moq;

using MuHub.Api.Hubs;
using MuHub.Api.Hubs.Clients;
using MuHub.Api.Responses;
using MuHub.Api.Services;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Exceptions;
using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

using Throw;

namespace MuHub.Api.Tests.Services;

public class MarketCoinsInteractionServiceTests
{
    private readonly Mock<ICoinsDataService> _coinsDataServiceMock;
    private readonly Mock<IMarketCoinsStorage> _marketCoinsStorageMock;
    private readonly Mock<IUpdateMarketCoinTimeStampStorage> _updateMarketCoinTimeStampStorageMock;
    private readonly Mock<ICoinsStorage> _coinsStorageMock;
    private readonly Mock<IHubContext<CoinsHub, ICoinsClient>> _hubMock;
    private readonly MarketCoinsInteractionService _marketCoinsInteractionService;

    public MarketCoinsInteractionServiceTests()
    {
        _coinsDataServiceMock = new Mock<ICoinsDataService>();
        _marketCoinsStorageMock = new Mock<IMarketCoinsStorage>();
        _updateMarketCoinTimeStampStorageMock = new Mock<IUpdateMarketCoinTimeStampStorage>();
        _coinsStorageMock = new Mock<ICoinsStorage>();
        _hubMock = new Mock<IHubContext<CoinsHub, ICoinsClient>>();
        _marketCoinsInteractionService = new MarketCoinsInteractionService(_coinsDataServiceMock.Object,
            _marketCoinsStorageMock.Object, _updateMarketCoinTimeStampStorageMock.Object, _coinsStorageMock.Object,
            _hubMock.Object);
    }

    [Fact]
    public async Task UpdateCoinInformation_WhenNoCoinsFound_ShouldThrowUnexpectedException()
    {
        // Arrange
        var coinsDictionary = new Dictionary<string, Coin>();
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync()).ReturnsAsync(coinsDictionary);

        // Act & Assert
        await FluentActions.Invoking(() =>
            _marketCoinsInteractionService.UpdateCoinInformation()).Should().ThrowAsync<UnexpectedException>();
        _updateMarketCoinTimeStampStorageMock.Verify(x => x.GetLastUpdateTimeAsync(), Times.Never);
        _coinsDataServiceMock.Verify(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()), Times.Never);
        _marketCoinsStorageMock.Verify(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()), Times.Never);
        _hubMock.Verify(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCoinInformation_SuccessfulUpdating()
    {
        // Arrange
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync())
            .ReturnsAsync(GetCoinList().ToDictionary(x => x.ExternalSymbolId));
        _updateMarketCoinTimeStampStorageMock.Setup(x => x.GetLastUpdateTimeAsync())
            .ReturnsAsync(new MarketCoinsUpdateTimestamp());
        _coinsDataServiceMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Ok(new List<MarketCoinDto>()));
        _marketCoinsStorageMock.Setup(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()))
            .Returns(Task.CompletedTask);
        _hubMock.Setup(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()));

        // Act
        await _marketCoinsInteractionService.UpdateCoinInformation();
        
        // Assert
        _coinsStorageMock.Verify(x => x.GetAllDictionaryByExternalSymbolIdAsync(), Times.Once);
        _updateMarketCoinTimeStampStorageMock.Verify(x => x.GetLastUpdateTimeAsync(), Times.Once);
        _coinsDataServiceMock.Verify(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()), Times.Once);
        _marketCoinsStorageMock.Verify(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()), Times.Once);
        _hubMock.Verify(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCoinInformation_WhenLastUpdatedWithinValidDataPeriodSeconds_ShouldReturnWithoutUpdating()
    {
        // Arrange
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync())
            .ReturnsAsync(GetCoinList().ToDictionary(x => x.ExternalSymbolId));
        _updateMarketCoinTimeStampStorageMock.Setup(x => x.GetLastUpdateTimeAsync())
            .ReturnsAsync(new MarketCoinsUpdateTimestamp()
            {
                LastUpdated = DateTimeOffset.UtcNow
            });

        // Act
        await _marketCoinsInteractionService.UpdateCoinInformation();
        
        // Assert
        _coinsStorageMock.Verify(x => x.GetAllDictionaryByExternalSymbolIdAsync(), Times.Once);
        _updateMarketCoinTimeStampStorageMock.Verify(x => x.GetLastUpdateTimeAsync(), Times.Once);
        _coinsDataServiceMock.Verify(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()), Times.Never);
        _marketCoinsStorageMock.Verify(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()), Times.Never);
        _hubMock.Verify(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCoinInformation_WhenCoinsDataServiceGetMarketCoinListAsyncFailed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync())
            .ReturnsAsync(GetCoinList().ToDictionary(x => x.ExternalSymbolId));
        _updateMarketCoinTimeStampStorageMock.Setup(x => x.GetLastUpdateTimeAsync())
            .ReturnsAsync(new MarketCoinsUpdateTimestamp());
        _coinsDataServiceMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Fail("error"));

        // Act & Assert
        await FluentActions.Invoking(() =>
            _marketCoinsInteractionService.UpdateCoinInformation()).Should().ThrowAsync<InvalidOperationException>();
        _marketCoinsStorageMock.Verify(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()), Times.Never);
        _hubMock.Verify(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()), Times.Never);
    }
    
    [Fact]
    public void UpdateCoinInformation_WhenMarketCoinsStorageReplaceAllMarketCoinsAsyncFailedAndThrows_ShouldNotUpdateCoinsInformationToClients()
    {
        // Arrange
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync())
            .ReturnsAsync(GetCoinList().ToDictionary(x => x.ExternalSymbolId));
        _updateMarketCoinTimeStampStorageMock.Setup(x => x.GetLastUpdateTimeAsync())
            .ReturnsAsync(new MarketCoinsUpdateTimestamp());
        _coinsDataServiceMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Ok(new List<MarketCoinDto>()));
        _marketCoinsStorageMock.Setup(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()))
            .Throws(new Exception());


        // Act & Assert
        FluentActions.Invoking(() => _marketCoinsInteractionService.UpdateCoinInformation()).Should().Throw();
        _hubMock.Verify(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()), Times.Never);
    }
    
    [Fact]
    public void UpdateCoinInformation_WhenClientsAllUpdateCoinsInformationFailedAndThrows_ShouldThrow()
    {
        // Arrange
        _coinsStorageMock.Setup(x => x.GetAllDictionaryByExternalSymbolIdAsync())
            .ReturnsAsync(GetCoinList().ToDictionary(x => x.ExternalSymbolId));
        _updateMarketCoinTimeStampStorageMock.Setup(x => x.GetLastUpdateTimeAsync())
            .ReturnsAsync(new MarketCoinsUpdateTimestamp());
        _coinsDataServiceMock.Setup(x => x.GetMarketCoinListAsync(It.IsAny<GetMarketCoinRequest>()))
            .ReturnsAsync(Result.Ok(new List<MarketCoinDto>()));
        _marketCoinsStorageMock.Setup(x => x.ReplaceAllMarketCoinsAsync(It.IsAny<IList<MarketCoin>>()))
            .Returns(Task.CompletedTask);
        _hubMock.Setup(x => x.Clients.All.UpdateCoinsInformation(It.IsAny<IEnumerable<MarketCoinResponse>>()))
            .Throws(new Exception());


        // Act & Assert
        FluentActions.Invoking(() => _marketCoinsInteractionService.UpdateCoinInformation()).Should().Throw();
    }
    
    private static List<Coin> GetCoinList()
    {
        return new List<Coin>
        {
            new()
            {
                ExternalSymbolId = "id1", SymbolId = "id1", Symbol = "symbol1", Name = "name1",
            },
            new()
            {
                ExternalSymbolId = "id2", SymbolId = "id2", Symbol = "symbol2", Name = "name2",
            },
            new()
            {
                ExternalSymbolId = "id3", SymbolId = "id3", Symbol = "symbol3", Name = "name3",
            },
            new()
            {
                ExternalSymbolId = "id4", SymbolId = "id4", Symbol = "symbol4", Name = "name4",
            },
            new()
            {
                ExternalSymbolId = "id5", SymbolId = "id5", Symbol = "symbol5", Name = "name5",
            },
            new()
            {
                ExternalSymbolId = "id6", SymbolId = "id6", Symbol = "symbol6", Name = "name6",
            },
            new()
            {
                ExternalSymbolId = "id7", SymbolId = "id7", Symbol = "symbol7", Name = "name7",
            },
            new()
            {
                ExternalSymbolId = "id8", SymbolId = "id8", Symbol = "symbol8", Name = "name8",
            },
            new()
            {
                ExternalSymbolId = "id9", SymbolId = "id9", Symbol = "symbol9", Name = "name9",
            },
        };
    }
}
