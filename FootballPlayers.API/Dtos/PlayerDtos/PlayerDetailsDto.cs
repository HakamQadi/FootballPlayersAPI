using System;

namespace FootballPlayers.API.Dtos.PlayerDtos;

public record class PlayerDetailsDto(
    int Id,
    string Name,
    int Age,
    int TeamId,
    int PositionId
);
