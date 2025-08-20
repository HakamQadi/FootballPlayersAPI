using System;

namespace FootballPlayers.API.Dtos.PlayerDtos;

public record class PlayerDto(
    int Id,
    string Name,
    string Age,
    string Team,
    string Position
);
