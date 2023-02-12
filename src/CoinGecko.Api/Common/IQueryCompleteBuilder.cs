namespace CoinGecko.Api.Common;

/// <summary>
/// Query complete builder.
/// </summary>
internal interface IQueryCompleteBuilder
{
    /// <summary>
    /// Builds the query.
    /// </summary>
    /// <returns>An instance of <see cref="Uri"/>.</returns>
    Uri Build();
}
