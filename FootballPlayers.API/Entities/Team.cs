using System;

namespace FootballPlayers.API.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // It tells EF Core: “A team has many players”. When EF builds the database, it uses this to set up foreign keys.
    public List<Player> Players { get; set; } = new();
}
