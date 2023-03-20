using MuHub.Application.Structures;
using MuHub.Domain.Entities;

namespace MuHub.Application.Contracts.Persistence;

/// <summary>
/// Coins storage interface.
/// </summary>
public interface ICoinsStorage
{
    /// <summary>
    /// Asynchronously gets all coins.
    /// </summary>
    /// <returns>A list of coins.</returns>
    Task<List<Coin>> GetAllAsync();

    /// <summary>
    /// Asynchronously gets all coins as dictionary.
    /// </summary>
    /// <returns>A dictionary of coins.</returns>
    Task<Dictionary<string, Coin>> GetAllDictionaryByExternalSymbolIdAsync();

    /// <summary>
    /// Asynchronously gets all coins by symbol.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns>A list of coins.</returns>
    Task<List<Coin>?> GetBySymbolAsync(string symbol);

    /// <summary>
    /// Asynchronously gets all coins as paged list.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>A paged list of coins.</returns>
    Task<PagedList<Coin>?> GetPagedAsync(int page, int pageSize);

    /// <summary>
    /// Asynchronously adds a coin.
    /// </summary>
    /// <param name="coin">Coin to add.</param>
    /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task AddAsync(Coin coin);

    /// <summary>
    /// Asynchronously adds a list of coins.
    /// </summary>
    /// <param name="coins">Coins to add.</param>
    /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task AddAsync(IEnumerable<Coin> coins);
    
    /// <summary>
    /// Asynchronously updates a coin.
    /// </summary>
    /// <param name="coin">Coin to update.</param>
    /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task UpdateAsync(Coin coin);
    
    /// <summary>
    /// Asynchronously updates a list of coins.
    /// </summary>
    /// <param name="coins">Coins to update.</param>
    /// <returns>An instance of <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task UpdateAsync(IEnumerable<Coin> coins);
}
