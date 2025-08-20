using System;

namespace FootballPlayers.API.Entities;

public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }

    public int TeamId { get; set; }
    public Team? TeamName { get; set; }

    public int PositionId { get; set; }
    public Position? Position { get; set; }
}
