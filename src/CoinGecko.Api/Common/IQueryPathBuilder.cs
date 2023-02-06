namespace CoinGecko.Api.Common;

internal interface IQueryPathBuilder : IQueryCompleteBuilder
{
    IQueryParametersBuilder AddPath(string path);
}
