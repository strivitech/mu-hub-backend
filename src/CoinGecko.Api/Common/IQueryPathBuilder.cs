namespace CoinGecko.Api.Common;

/// <summary>
/// Query path builder.
/// </summary>
internal interface IQueryPathBuilder : IQueryCompleteBuilder
{
    /// <summary>
    /// Adds path.
    /// </summary>
    /// <param name="path">Path.</param>
    /// <returns>An instance of <see cref="IQueryParametersBuilder"/>.</returns>
    IQueryParametersBuilder AddPath(string path);
}
