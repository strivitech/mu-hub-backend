using System.Text;

namespace CoinGecko.Api.Common;

/// <summary>
/// Query builder.
/// </summary>
internal static class QueryBuilder
{
    /// <summary>
    /// Inside implementation of query builder.
    /// </summary>
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
    
    /// <summary>
    /// Creates a new instance of <see cref="IQueryPathBuilder"/>.
    /// </summary>
    /// <param name="host">Host.</param>
    /// <returns>An instance of <see cref="IQueryPathBuilder"/>.</returns>
    internal static IQueryPathBuilder Create(string host)
    {
        return new QueryImplementation(host);
    }
    
    /// <summary>
    /// Creates a new instance of <see cref="IQueryPathBuilder"/>.
    /// </summary>
    /// <param name="host">Host.</param>
    /// <returns>An instance of <see cref="IQueryPathBuilder"/>.</returns>
    internal static IQueryPathBuilder Create(Uri host)
    {
        return new QueryImplementation(host);
    }
}
