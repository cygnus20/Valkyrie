using Microsoft.EntityFrameworkCore;
using Valkyrie.Entities;
using Valkyrie.DTOs;
using System.Runtime.InteropServices;

namespace Valkyrie.Extensions;

public static class Endpoints
{
    public static void MapDevboardsEndpoints(this WebApplication app)
    {
        var devboards = app.MapGroup("/devboards");

        // GET devboards
        _ = devboards.MapGet("/", async (ValkDbContext ctx) => await ctx.DevBoards.Select(_ => _.AsDTO()).ToListAsync());

        // GET devboard/guid
        _ = devboards.MapGet("/{guid}", async (Guid guid, ValkDbContext ctx) =>
        {
            var devboard = await ctx.DevBoards.FirstOrDefaultAsync(_ => _.Guid == guid);

            return devboard is not null ? Results.Ok(devboard.AsDTO()) : Results.NotFound();
        });

        // PUT devboards/guid
        _ = devboards.MapPut("/{guid}", async (Guid guid, InputDevboardDTO devboard, ValkDbContext context) =>
        {
            var existingBoard = await context.DevBoards.AsNoTracking().FirstOrDefaultAsync(_ => _.Guid == guid);

            if (existingBoard is null)
                return Results.NotFound();

            var c = context.Entry(existingBoard).State;


            var updatedBoard = existingBoard with
            {
                Name = devboard.Name,
                Description = devboard.Description,
                Platform = devboard.Platform,
                Family = devboard.Family,
                MainMCU = devboard.MainMCU,
                Pins = devboard.Pins,
                Communications = devboard.Communications,
                Power = devboard.Power,
                Dimensions = devboard.Dimensions,
                JTAG = devboard.JTAG

            };
          
            context.Update(updatedBoard);

            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        // POST deveboards
        _ = devboards.MapPost("/", async (InputDevboardDTO devboard, ValkDbContext context) =>
        {
            var newBoard = new DevBoard
            {
                Name = devboard.Name,
                Description = devboard.Description,
                Platform = devboard.Platform,
                Family = devboard.Family,
                MainMCU = devboard.MainMCU,
                Pins = devboard.Pins,
                Communications = devboard.Communications,
                Power = devboard.Power,
                Dimensions = devboard.Dimensions,
                JTAG = devboard.JTAG

            };

            context.Add(newBoard);

            await context.SaveChangesAsync();

            return Results.Created($"/devboards/{newBoard.Guid}", newBoard);
        });

        // DELETE devboards/guid
        _ = devboards.MapDelete("/{guid}", async(Guid guid, ValkDbContext ctx) =>
        {
            if (await ctx.DevBoards.FirstOrDefaultAsync(_ => _.Guid == guid) is DevBoard dev )
            {
                ctx.Remove(dev);
                await ctx.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });


    }


}
