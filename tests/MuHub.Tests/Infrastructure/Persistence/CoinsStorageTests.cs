using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Moq;

using MuHub.Application.Contracts.Persistence;
using MuHub.Application.Structures;
using MuHub.Domain.Entities;
using MuHub.Infrastructure.Persistence;

namespace MuHub.Tests.Infrastructure.Persistence;

public class CoinsStorageTests
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly CoinsStorage _coinsStorage;

    public CoinsStorageTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "application_database")
            .Options;
        _applicationDbContext = new ApplicationDbContext(options);
        _applicationDbContext.Instance.Database.EnsureDeleted();
        _applicationDbContext.Instance.Database.EnsureCreated();
        _coinsStorage = new CoinsStorage(_applicationDbContext);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCoinList()
    {
        // Arrange
        var coinList = GetCoinList();
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var result = await _coinsStorage.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(coinList);
    }

    [Fact]
    public async Task GetAllDictionaryByExternalSymbolIdAsync_ReturnsCoinDictionaryByExternalSymbolId()
    {
        // Arrange
        var coinList = GetCoinList();
        var expected = coinList.ToDictionary(x => x.ExternalSymbolId);
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var result = await _coinsStorage.GetAllDictionaryByExternalSymbolIdAsync();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetBySymbolAsync_ReturnsCoinList()
    {
        // Arrange
        var coinList = GetCoinList();
        const string symbol = "symbol1";
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();

        // Act
        var result = await _coinsStorage.GetBySymbolAsync(symbol);

        // Assert
        result.Should().BeEquivalentTo(coinList.Where(x => x.Symbol == symbol).ToList());
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsPagedCoinList()
    {
        // Arrange
        var coinList = GetCoinList();
        const int page = 1;
        const int pageSize = 3;
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();
        var expected = PagedList<Coin>.FromItems(coinList.Skip((page - 1) * pageSize).Take(pageSize), coinList.Count,
            page, pageSize);

        // Act
        var result = await _coinsStorage.GetPagedAsync(page, pageSize);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task AddAsync()
    {
        // Arrange
        var coinList = GetCoinList();
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();
        var newCoin = new Coin
        {
            ExternalSymbolId = "id111", SymbolId = "id111", Symbol = "symbol111", Name = "name111",
        };
        coinList.Add(newCoin);

        // Act
        await _coinsStorage.AddAsync(newCoin);

        // Assert
        var coins = await _applicationDbContext.Coins.ToListAsync();
        coins.Should().BeEquivalentTo(coinList);
    }

    [Fact]
    public async Task AddEnumerableAsync()
    {
        // Arrange
        var coinList = GetCoinList();
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();
        var newCoins = new Coin[]
        {
            new(){ExternalSymbolId = "id111", SymbolId = "id111", Symbol = "symbol111", Name = "name111"},
            new(){ExternalSymbolId = "id222", SymbolId = "id222", Symbol = "symbol222", Name = "name222"},
            new(){ExternalSymbolId = "id333", SymbolId = "id333", Symbol = "symbol333", Name = "name333"},
        };
        coinList.AddRange(newCoins);

        // Act
        await _coinsStorage.AddAsync(newCoins);

        // Assert
        var coins = await _applicationDbContext.Coins.ToListAsync();
        coins.Should().BeEquivalentTo(coinList);
    }
    
    [Fact]
    public async Task UpdateAsync()
    {
        // Arrange
        var coinList = GetCoinList();
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();
        var updateCoin = coinList.First();
        updateCoin.Symbol = "newSymbol";

        // Act
        await _coinsStorage.UpdateAsync(updateCoin);

        // Assert
        var coins = await _applicationDbContext.Coins.ToListAsync();
        coins.Should().BeEquivalentTo(coinList);
    }
    
    [Fact]
    public async Task UpdateEnumerableAsync()
    {
        // Arrange
        var coinList = GetCoinList();
        _applicationDbContext.Coins.AddRange(coinList);
        await _applicationDbContext.SaveChangesAsync();
        var updateCoins = coinList.Where(x => x.Id % 2 == 0).ToList();
        updateCoins.ForEach(x => x.Symbol = "newSymbol");

        // Act
        await _coinsStorage.UpdateAsync(updateCoins);

        // Assert
        var coins = await _applicationDbContext.Coins.ToListAsync();
        coins.Should().BeEquivalentTo(coinList);
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
