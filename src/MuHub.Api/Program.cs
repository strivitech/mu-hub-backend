using System.Diagnostics;
using System.Text.Json;

using MuHub.Api;

var builder = WebApplication.CreateBuilder(args);

// Configures all services for Application
builder.ConfigureServices();

var app = builder.Build();

// Configures the pipeline for Application
app.Configure();

try
{
    // Add logs
#if DEBUG
    MuHub.Api.Seeders.CoinsFakeSeeder.AddFakeCoins(app);
#endif
    
    app.Run();
}
catch (Exception ex)
{
    // Add logs
    Debug.WriteLine(JsonSerializer.Serialize(ex));
}
finally
{
    // Add logs and flush
}
