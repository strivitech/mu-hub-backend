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
    // TODO: move this code
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!roleManager.Roles.Any())
    {
        roleManager.CreateAsync(new IdentityRole("user")).Wait();
    }
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
