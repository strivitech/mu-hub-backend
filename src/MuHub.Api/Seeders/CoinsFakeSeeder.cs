#if DEBUG

using System.Text.Json;

using MuHub.Application.Contracts.Persistence;
using MuHub.Domain.Entities;

namespace MuHub.Api.Seeders;

/// <summary>
/// Coin fake seeder.
/// </summary>
public static class CoinsFakeSeeder
{
    /// <summary>
    /// Add fake coins.
    /// </summary>
    /// <param name="webApplication">Web application.</param>
    public static void AddFakeCoins(this WebApplication webApplication)
    {
        if (!webApplication.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = webApplication.Services.CreateScope();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        
        if (!applicationDbContext.Coins.Any())
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeders", "coin_names.json");;
            var jsonString = File.ReadAllText(path);
            var coins = JsonSerializer.Deserialize<List<Coin>>(jsonString);
            for (int i = 0; i < coins!.Count; i++)
            {
                coins[i].Id = i + 1;
            }
            
            applicationDbContext.Coins.AddRange(coins);
            applicationDbContext.Instance.SaveChanges();
        }
    }
}

#endif
