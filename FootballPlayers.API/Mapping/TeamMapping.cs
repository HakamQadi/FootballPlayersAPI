using System;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;

namespace FootballPlayers.API.Mapping;

public static class TeamMapping
{
    public static Team ToEntity(this CreateTeamDto team)
    {
        return new Team()
        {
            Name = team.Name
        };
    }
    public static Team ToEntity(this UpdateTeamDto team, int id)
    {
        return new Team()
        {
            Id = id,
            Name = team.Name
        };
    }

    public static TeamDto ToDto(this Team team)
    {
        return new(
            team.Id,
            team.Name
        );
    }
    public static TeamDto ToTeamDetailsDto(this Team team)
    {
        return new(
            team.Id,
            team.Name
        );
    }

}
