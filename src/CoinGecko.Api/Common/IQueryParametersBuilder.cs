namespace CoinGecko.Api.Common;

internal interface IQueryParametersBuilder : IQueryCompleteBuilder
{
    IQueryCompleteBuilder AddQueryParameters(Dictionary<string, string> parameters);
}
