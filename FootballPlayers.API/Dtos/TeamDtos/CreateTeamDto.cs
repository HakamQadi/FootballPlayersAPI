using System;
using System.ComponentModel.DataAnnotations;

namespace FootballPlayers.API.Dtos.TeamDtos;

public record class CreateTeamDto(
    [Required] string Name
);
