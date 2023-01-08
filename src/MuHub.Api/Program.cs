using System.Diagnostics;
using System.Text.Json;

using Microsoft.AspNetCore.Identity;

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
    using var scope = app.Services.CreateScope();
    // Add logic before app runs 
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
