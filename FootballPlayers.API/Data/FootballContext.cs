using System;

namespace FootballPlayers.API.Data;

using FootballPlayers.API.Entities;
using Microsoft.EntityFrameworkCore;
public class FootballContext(DbContextOptions<FootballContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();

    public DbSet<Team> Teams => Set<Team>();

    public DbSet<Position> Positions => Set<Position>();

    // This will be executed when the migration executed
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new { Id = 1, Name = "FC Barcelona" },
            new { Id = 2, Name = "Real Madrid" },
            new { Id = 3, Name = "Atlético de Madrid" }

        );
    }
}
