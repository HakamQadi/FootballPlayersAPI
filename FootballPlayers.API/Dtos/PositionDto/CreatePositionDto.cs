using System;
using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.API.Dtos.TeamDtos;

public record class CreatePositionDto(
    [Required] string Name
);
