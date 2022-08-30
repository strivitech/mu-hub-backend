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
    app.Run();
}
catch (Exception ex)
{
    // Add logs
}
finally
{
    // Add logs and flush
}
