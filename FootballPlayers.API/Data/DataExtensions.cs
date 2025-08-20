// using System;
// using Microsoft.EntityFrameworkCore;

// namespace FootballPlayers.API.Data;

// public static class DataExtensions
// {
//     public static async Task MigrateDbAsync(this WebApplication app)
//     {
//         using var scope = app.Services.CreateScope();
//         var dbContext = scope.ServiceProvider.GetRequiredService<FootballContext>();
//         await dbContext.Database.MigrateAsync();
//     }
// }

// using FootballPlayers.API.Data;
// using Microsoft.EntityFrameworkCore;

// namespace FootballPlayers.API;

// public static class DataExtensions
// {
//     public static void AddDatabase(this IServiceCollection services, IConfiguration config)
//     {
//         services.AddDbContext<FootballContext>(options =>
//             options.UseSqlite(config.GetConnectionString("DefaultConnection")));
//     }
// }

using FootballPlayers.API.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API;

public static class DataExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<FootballContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }
}
