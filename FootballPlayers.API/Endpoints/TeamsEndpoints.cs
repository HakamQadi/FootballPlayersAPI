using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;
using FootballPlayers.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class TeamsEndpoints
{
    public static RouteGroupBuilder MapTeamsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/teams").WithParameterValidation();

        group.MapGet("/", async (FootballContext db) =>
            await db.Teams.Select(team => team.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapGet("/{id}", async (FootballContext db, int id) =>
        {
            Team? team = await db.Teams.FindAsync(id);
            if (team is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(team.ToTeamDetailsDto());
        });

        group.MapPost("/create", async (FootballContext db, CreateTeamDto newTeam) =>
        {
            Team? team = await db.Teams.FirstOrDefaultAsync(team => team.Name == newTeam.Name);
            if (team is not null)
            {
                return Results.Conflict("The Team with this name is already exist");
            }

            team = newTeam.ToEntity();
            db.Teams.Add(team);
            await db.SaveChangesAsync();

            return Results.Created();
        });

        group.MapPatch("/update/{id}", async (int id, UpdateTeamDto updatedTeam, FootballContext db) =>
        {
            Team? team = await db.Teams.FirstOrDefaultAsync(team => team.Name == updatedTeam.Name);
            if (team is not null)
            {
                return Results.Conflict("Team with same name is already exist");
            }

            team = await db.Teams.FindAsync(id);
            if (team is null)
            {
                return Results.NotFound();
            }


            team.Name = updatedTeam.Name;

            await db.SaveChangesAsync();

            return Results.Ok(team.ToDto());
        });

        group.MapDelete("/delete/{id}", async (int id, FootballContext db) =>
        {
            Team? team = await db.Teams.FindAsync(id);
            if (team is null)
            {
                return Results.NotFound();
            }

            await db.Teams.Where(team => team.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return group;
    }
}
