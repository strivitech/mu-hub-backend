using System.Diagnostics;
using System.Text.Json;

using Microsoft.AspNetCore.Identity;

using MuHub.Api;
using MuHub.Api.Common.Extensions.Startup;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configures all services for Application
builder.ConfigureServices();
builder.ConfigureLogging();

var app = builder.Build();

// Configures the pipeline for Application
app.Configure();

try
{
    Log.Information("Run app");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host failure");
    Debug.WriteLine(JsonSerializer.Serialize(ex));
}
finally
{
    Log.CloseAndFlush();
}
