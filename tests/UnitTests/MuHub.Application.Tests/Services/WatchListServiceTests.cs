using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Services.Implementations;
using MuHub.Domain.Entities;
using MuHub.Infrastructure.Persistence;

namespace MuHub.Application.Tests.Services
{
    public class WatchListServiceTests
    {
        private readonly WatchListService _watchListService;
        private readonly IApplicationDbContext _dbContext;

        public WatchListServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "application_database")
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Instance.Database.EnsureDeleted();
            _dbContext.Instance.Database.EnsureCreated();

            _watchListService = new WatchListService(_dbContext);
        }

        [Fact]
        public async Task AddToWatchListAsync_WithValidCoinIdAndUserId_ShouldReturnTrue()
        {
            // Arrange
            const int coinId = 1;
            const string userId = "ValidUserId";
            var coin = new Coin { Id = coinId, SymbolId = "BTC", Symbol = "BTC", Name = "Bitcoin", ExternalSymbolId = "BTC" };

            _dbContext.Coins.Add(coin);
            await _dbContext.Instance.SaveChangesAsync();

            // Act
            var result = await _watchListService.AddToWatchListAsync(coinId, userId);

            // Assert
            result.Should().BeTrue();
            _dbContext.WatchList.Should().Contain(wl => wl.CoinId == coinId && wl.UserId == userId);
        }
        
        [Fact]
        public async Task AddToWatchListAsync_WithNonExistentCoinId_ShouldReturnFalse()
        {
            // Arrange
            const int coinId = 2;
            const string userId = "ValidUserId";

            // Act
            var result = await _watchListService.AddToWatchListAsync(coinId, userId);

            // Assert
            result.Should().BeFalse();
            _dbContext.WatchList.Should().BeEmpty();
        }

        [Fact]
        public async Task AddToWatchListAsync_WithEmptyUserId_ShouldReturnFalse()
        {
            // Arrange
            const int coinId = 1;
            string userId = string.Empty;
            var coin = new Coin { Id = coinId, SymbolId = "BTC", Symbol = "BTC", Name = "Bitcoin", ExternalSymbolId = "BTC" };

            _dbContext.Coins.Add(coin);
            await _dbContext.Instance.SaveChangesAsync();

            // Act
            var result = await _watchListService.AddToWatchListAsync(coinId, userId);

            // Assert
            result.Should().BeFalse();
            _dbContext.WatchList.Should().BeEmpty();
        }

        [Fact]
        public async Task AddToWatchListAsync_WithZeroCoinId_ShouldReturnFalse()
        {
            // Arrange
            const int coinId = 0;
            const string userId = "ValidUserId";

            // Act
            var result = await _watchListService.AddToWatchListAsync(coinId, userId);

            // Assert
            result.Should().BeFalse();
            _dbContext.WatchList.Should().BeEmpty();
        }

        [Fact]
        public async Task GetWatchListsByUserAsync_WithNoWatchLists_ShouldReturnEmptyCollection()
        {
            // Arrange
            const string userId = "ValidUserId";

            // Act
            var result = await _watchListService.GetWatchListsByUserAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result!.CoinIds.Should().BeEmpty();
        }

        [Fact]
        public async Task GetWatchListsByUserAsync_WithExistingWatchLists_ShouldReturnCollection()
        {
            // Arrange
            const string userId = "ValidUserId";
            const int coinId1 = 1;
            const int coinId2 = 2;

            var coin1 = new Coin { Id = coinId1, SymbolId = "BTC", Symbol = "BTC", Name = "Bitcoin", ExternalSymbolId = "BTC" };
            var coin2 = new Coin { Id = coinId2, SymbolId = "ETH", Symbol = "ETH", Name = "Ethereum", ExternalSymbolId = "ETH" };

            _dbContext.Coins.AddRange(coin1, coin2);
            await _dbContext.Instance.SaveChangesAsync();

            var watchList1 = new WatchList { CoinId = coinId1, UserId = userId };
            var watchList2 = new WatchList { CoinId = coinId2, UserId = userId };

            _dbContext.WatchList.AddRange(watchList1, watchList2);
            await _dbContext.Instance.SaveChangesAsync();

            // Act
            var result = await _watchListService.GetWatchListsByUserAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result!.CoinIds.Should().Contain(new[] {coinId1, coinId2});
        }

        [Fact]
        public async Task GetWatchListsByUserAsync_WithEmptyUserId_ShouldReturnNull()
        {
            // Arrange
            string userId = string.Empty;

            // Act
            var result = await _watchListService.GetWatchListsByUserAsync(userId);

            // Assert
            result.Should().BeNull();
        }
    }
}