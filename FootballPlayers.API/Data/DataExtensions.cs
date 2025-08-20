using System;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FootballContext>();
        await dbContext.Database.MigrateAsync();
    }
}
