using System;

namespace FootballPlayers.API.Entities;

public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }

    public int TeamId { get; set; }
    public required Team TeamName { get; set; }

    public int PositionId { get; set; }
    public required Position Position { get; set; }
}
