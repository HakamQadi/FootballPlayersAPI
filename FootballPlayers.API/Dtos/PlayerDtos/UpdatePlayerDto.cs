using System;

namespace FootballPlayers.API.Dtos.PlayerDtos;

public record class UpdatePlayerDto(
    string Name,
    int Age,
    int TeamId,
    int PositionId
);
