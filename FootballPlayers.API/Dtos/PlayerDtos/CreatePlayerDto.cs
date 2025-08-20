using System;
using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.API.Dtos.PlayerDtos;

public record class CreatePlayerDto(
    [Required] string Name,
    int Age,

    [Required] int TeamId,
    [Required] int PositionId
);
