using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MuHub.Application.Contracts.Persistence;
using MuHub.Domain.Entities;
using MuHub.Infrastructure.Persistence;

namespace MuHub.Tests.Infrastructure.Persistence;

public class UpdateMarketCoinTimeStampStorageTests
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly UpdateMarketCoinTimeStampStorage _updateMarketCoinTimeStampStorage;

    public UpdateMarketCoinTimeStampStorageTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "application_database")
            .Options;
        _applicationDbContext = new ApplicationDbContext(options);
        _applicationDbContext.Instance.Database.EnsureDeleted();
        _applicationDbContext.Instance.Database.EnsureCreated();
        _updateMarketCoinTimeStampStorage = new UpdateMarketCoinTimeStampStorage(_applicationDbContext);
    }

    [Fact]
    public async Task GetLastUpdateTimeAsync_WhenNoRecords_ReturnsNull()
    {
        // Arrange

        // Act
        var result = await _updateMarketCoinTimeStampStorage.GetLastUpdateTimeAsync();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetLastUpdateTimeAsync_WhenRecordsExist_ReturnsNull()
    {
        // Arrange
        var marketCoinsUpdateTimestampList = GetMarketCoinsUpdateTimestampList();
        _applicationDbContext.MarketCoinsUpdateTimestamps.AddRange(marketCoinsUpdateTimestampList);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var result = await _updateMarketCoinTimeStampStorage.GetLastUpdateTimeAsync();

        // Assert
        result.Should().BeEquivalentTo(marketCoinsUpdateTimestampList.OrderBy(x => x.LastUpdated).Last());
    }

    private static List<MarketCoinsUpdateTimestamp> GetMarketCoinsUpdateTimestampList()
    {
        return new List<MarketCoinsUpdateTimestamp>()
        {
            new() { Id = 1, LastUpdated = DateTimeOffset.Now.AddMinutes(-2) },
            new() { Id = 2, LastUpdated = DateTimeOffset.Now.AddMinutes(-1) },
            new() { Id = 3, LastUpdated = DateTimeOffset.Now },
        };
    }
}
