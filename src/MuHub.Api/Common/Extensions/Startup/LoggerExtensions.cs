using MuHub.Infrastructure.Common.Configurations;

using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MuHub.Api.Common.Extensions.Startup;

/// <summary>
/// Extensions for Logging setup
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Configures logging using Serilog
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var configuration = builder.Configuration ?? throw new ArgumentNullException(nameof(builder.Configuration));

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithEnvironmentName()
            .MinimumLevel.Debug()
            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration))
            .WriteTo.Console()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        builder.Host.UseSerilog(Log.Logger, true);

        return builder;
    }

    /// <summary>
    /// Configures ElasticSearch options sink
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
    {
        var options = configuration.GetSection(ElasticOptions.Name).Get<ElasticOptions>();
        
        return new ElasticsearchSinkOptions(new Uri(options.Uri))
        {
            AutoRegisterTemplate = options.AutoRegisterTemplate,
            NumberOfShards = options.NumberOfShards,
            NumberOfReplicas = options.NumberOfReplicas,
        };
        
    }
}
