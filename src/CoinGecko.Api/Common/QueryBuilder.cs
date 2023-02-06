using System.Text;

namespace CoinGecko.Api.Common;

internal static class QueryBuilder
{
    private class QueryImplementation : IQueryPathBuilder, IQueryParametersBuilder
    {
        private readonly StringBuilder _uriValue = new();
    
        internal QueryImplementation(string host)
        {
            _uriValue.Append(host);
        }
    
        internal QueryImplementation(Uri hostUri)
        {
            _uriValue.Append(hostUri.AbsoluteUri);
        }
        
        public IQueryParametersBuilder AddPath(string path)
        {
            _uriValue.Append(path);
            return this;
        }

        public IQueryCompleteBuilder AddQueryParameters(Dictionary<string, string> parameters)
        {
            var queryBuilder = new Microsoft.AspNetCore.Http.Extensions.QueryBuilder();
            foreach ((string key, string value) in parameters)
            {
                queryBuilder.Add(key, value);
            }
            _uriValue.Append(queryBuilder.ToQueryString().ToString());
            return this;
        }

        public Uri Build()
        {
            return new Uri(_uriValue.ToString());
        }
    }
    
    internal static IQueryPathBuilder Create(string host)
    {
        return new QueryImplementation(host);
    }
    
    internal static IQueryPathBuilder Create(Uri host)
    {
        return new QueryImplementation(host);
    }
}
