namespace CoinGecko.Api.Common;

/// <summary>
/// Query parameters builder.
/// </summary>
internal interface IQueryParametersBuilder : IQueryCompleteBuilder
{
    /// <summary>
    /// Adds query parameters.
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    /// <returns>An instance of <see cref="IQueryCompleteBuilder"/>.</returns>
    IQueryCompleteBuilder AddQueryParameters(Dictionary<string, string> parameters);
}
