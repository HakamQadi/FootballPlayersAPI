using FootballPlayers.API.Data;
using FootballPlayers.API.Dtos.PositionDto;
using FootballPlayers.API.Dtos.TeamDtos;
using FootballPlayers.API.Entities;
using FootballPlayers.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.API.Endpoints;

public static class PositionsEndpoints
{
    public static RouteGroupBuilder MapPositionsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/positions").WithParameterValidation();

        group.MapGet("/", async (FootballContext db) =>
            await db.Positions
                .Select(p => p.ToDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapGet("/{id:int}", async (FootballContext db, int id) =>
        {
            Position? position = await db.Positions.FindAsync(id);
            if (position is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(position.ToPositionDetailsDto());
        });

        group.MapPost("/create", async (FootballContext db, CreatePositionDto newPosition) =>
        {
            Position? position = await db.Positions.FirstOrDefaultAsync(position => position.Name == newPosition.Name);
            if (position is not null)
            {
                return Results.Conflict("The Position with this name is already exist");
            }

            position = newPosition.ToEntity();
            db.Positions.Add(position);
            await db.SaveChangesAsync();

            return Results.Created();
        });

        group.MapPatch("/update/{id}", async (int id, UpdatepositionDto updatedPosition, FootballContext db) =>
        {
            Position? position = await db.Positions.FirstOrDefaultAsync(position => position.Name == updatedPosition.Name);
            if (position is not null)
            {
                return Results.Conflict("Position with same name is already exist");
            }

            position = await db.Positions.FindAsync(id);
            if (position is null)
            {
                return Results.NotFound();
            }


            position.Name = updatedPosition.Name;

            await db.SaveChangesAsync();

            return Results.Ok(position.ToDto());
        });

        group.MapDelete("/delete/{id}", async (int id, FootballContext db) =>
        {
            Position? position = await db.Positions.FindAsync(id);
            if (position is null)
            {
                return Results.NotFound();
            }

            await db.Positions.Where(position => position.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
        return group;
    }
}
