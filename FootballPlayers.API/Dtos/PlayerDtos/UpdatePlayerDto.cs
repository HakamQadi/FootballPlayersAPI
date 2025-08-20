using System;

namespace FootballPlayers.API.Dtos.PlayerDtos;

public record class UpdatePlayerDto(
    string Name,
    string Age,
    int TeamId,
    int PositionId
);
