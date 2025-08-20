using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.PlayerDtos;
using FootballPlayers.API.Entities;
using FootballPlayers.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class PlayersEndpoints
{
    public static void MapPlayersEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/players");

        group.MapGet("/", async (FootballContext db) =>
            await db.Players.Include(p => p.TeamName).Include(p => p.Position)
                // .Select(p => p.ToPlayerDetailsDto())
                .Select(p => p.ToDto())
                .AsNoTracking()
                .ToListAsync());

        group.MapPost("/", async (FootballContext db, CreatePlayerDto dto) =>
        {
            var player = new Player
            {
                Name = dto.Name,
                Age = dto.Age,
                TeamId = dto.TeamId,
                PositionId = dto.PositionId
            };

            db.Players.Add(player);
            await db.SaveChangesAsync();

            // Reload with navigations
            await db.Entry(player).Reference(p => p.TeamName).LoadAsync();
            await db.Entry(player).Reference(p => p.Position).LoadAsync();

            return Results.Created($"/players/{player.Id}", player.ToDto());
        });
    }
}
