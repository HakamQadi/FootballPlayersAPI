using System;
using FootballPlayers.API.Dtos.PositionDto;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;

namespace FootballPlayers.API.Mapping;

public static class PositionMapping
{
    public static Position ToEntity(this CreatePositionDto position)
    {
        return new Position()
        {
            Name = position.Name
        };
    }
    public static Position ToEntity(this UpdatepositionDto position, int id)
    {
        return new Position()
        {
            Id = id,
            Name = position.Name
        };
    }

    public static PositionDto ToDto(this Position position)
    {
        return new(
            position.Id,
            position.Name
        );
    }
    public static PositionDto ToPositionDetailsDto(this Position position)
    {
        return new(
            position.Id,
            position.Name
        );
    }

}
