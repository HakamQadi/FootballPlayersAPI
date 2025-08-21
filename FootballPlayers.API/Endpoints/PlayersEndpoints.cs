using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.PlayerDtos;
using FootballPlayers.API.Entities;
using FootballPlayers.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class PlayersEndpoints
{
    public static RouteGroupBuilder MapPlayersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/players").WithParameterValidation();

        group.MapGet("/", async (FootballContext db) =>
            await db.Players.Include(player => player.TeamName)
            .Include(player => player.Position)
            .Select(player => player
            .ToDto())
            .AsNoTracking()
            .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, FootballContext db) =>
        {
            Player? player = await db.Players.FindAsync(id);

            if (player is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(player.ToPlayerDetailsDto());
        });

        group.MapPost("/create", async (CreatePlayerDto newPlayer, FootballContext db) =>
        {
            Player? player = await db.Players.FirstOrDefaultAsync(player => player.Name == newPlayer.Name);
            if (player is not null)
            {
                return Results.Problem("The Player with this name already exist");
            }

            var team = await db.Teams.FindAsync(newPlayer.TeamId);
            if (team is null)
            {
                return Results.NotFound("Team not found");
            }
            var position = await db.Positions.FindAsync(newPlayer.PositionId);
            if (position is null)
            {
                return Results.NotFound("Position not found");
            }


            player = newPlayer.ToEntity();
            db.Players.Add(player);
            await db.SaveChangesAsync();

            // Returns HTTP 201 Created with no Location header and no body
            return Results.Created();

            // Returns A Location header pointing to the newly created resource + A response body containing the created resource (DTO)
            // return Results.CreatedAtRoute("GetPlayers", new { id = player.Id }, player.ToDto());
        });

        group.MapPatch("/update", async (int id, UpdatePlayerDto updatedPlayer, FootballContext db) =>
        {
            Player? player = await db.Players.FindAsync(id);
            if (player is null)
            {
                return Results.NotFound("The player not found");
            }

            var team = await db.Teams.FindAsync(updatedPlayer.TeamId);
            if (team is null)
            {
                return Results.NotFound("Team not found");
            }

            var position = await db.Positions.FindAsync(updatedPlayer.PositionId);
            if (position is null)
            {
                return Results.NotFound("Position not found");
            }

            player.Name = updatedPlayer.Name;
            player.Age = updatedPlayer.Age;

            player.TeamName = team;
            player.Position = position;

            await db.SaveChangesAsync();

            return Results.Ok(player.ToDto());
        });

        group.MapDelete("/delete/{id}", async (int id, FootballContext db) =>
        {
            var deletedCount = await db.Players.Where(player => player.Id == id).ExecuteDeleteAsync();

            return deletedCount > 0
                    ? Results.NoContent()
                    : Results.NotFound("Player not found");
        });
        return group;
    }
}
