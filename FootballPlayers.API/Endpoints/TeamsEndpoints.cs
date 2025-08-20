using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class TeamsEndpoints
{
    public static void MapTeamsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/teams");

        // Get all teams
        group.MapGet("/", async (FootballContext db) =>
            await db.Teams
                .AsNoTracking()
                .Select(t => new TeamDto(t.Id, t.Name))
                .ToListAsync()
        );

        // Get team by id
        group.MapGet("/{id:int}", async (FootballContext db, int id) =>
        {
            var team = await db.Teams
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TeamDto(t.Id, t.Name))
                .FirstOrDefaultAsync();

            return team is not null ? Results.Ok(team) : Results.NotFound();
        });

        // Create new team
        group.MapPost("/", async (FootballContext db, CreateTeamDto dto) =>
        {
            var team = new Team { Name = dto.Name };
            db.Teams.Add(team);
            await db.SaveChangesAsync();

            return Results.Created($"/teams/{team.Id}", new TeamDto(team.Id, team.Name));
        });
    }
}
