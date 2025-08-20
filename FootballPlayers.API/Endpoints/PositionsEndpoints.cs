using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.PositionDto;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class PositionsEndpoints
{
    public static void MapPositionsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("api/positions");

        // Get all positions
        group.MapGet("/", async (FootballContext db) =>
            await db.Positions
                .AsNoTracking()
                .Select(p => new PositionDto(p.Id, p.Name))
                .ToListAsync()
        );

        // Get position by id
        group.MapGet("/{id:int}", async (FootballContext db, int id) =>
        {
            var position = await db.Positions
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PositionDto(p.Id, p.Name))
                .FirstOrDefaultAsync();

            return position is not null ? Results.Ok(position) : Results.NotFound();
        });

        // Create new position
        group.MapPost("/", async (FootballContext db, CreatePositionDto dto) =>
        {
            var position = new Position { Name = dto.Name };
            db.Positions.Add(position);
            await db.SaveChangesAsync();

            return Results.Created($"/positions/{position.Id}", new PositionDto(position.Id, position.Name));
        });
    }
}
