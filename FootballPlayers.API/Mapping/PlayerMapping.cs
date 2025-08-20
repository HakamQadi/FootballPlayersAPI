using System;
using FootballPlayers.API.Dtos.PlayerDtos;
using FootballPlayers.API.Entities;

namespace FootballPlayers.API.Mapping;

public static class PlayerMapping
{
    public static Player ToEntity(this CreatePlayerDto player)
    {
        return new Player()
        {
            Name = player.Name,
            Age = player.Age,
            TeamId = player.TeamId,
            PositionId = player.PositionId
        };
    }

    public static Player ToEntity(this UpdatePlayerDto player, int id)
    {
        return new Player()
        {
            Id = id,
            Name = player.Name,
            Age = player.Age,
            TeamId = player.TeamId,
            PositionId = player.PositionId
        };
    }

    public static PlayerDto ToDto(this Player player)
    {
        return new(
            player.Id,
            player.Name,
            player.Age,
            player.TeamName!.Name,
            player.Position!.Name
        );
    }

    public static PlayerDetailsDto ToPlayerDetailsDto(this Player player)
    {
        return new(

            player.Id,
            player.Name,
            player.Age,
            player.TeamId,
            player.PositionId
        );
    }

}
