namespace MuHub.Api.Services;

/// <summary>
/// Market coins interaction service.
/// </summary>
public interface IMarketCoinsInteractionService
{
    /// <summary>
    /// Updates coin information.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateCoinInformationAsync();
}
