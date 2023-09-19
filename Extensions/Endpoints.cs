﻿using Microsoft.EntityFrameworkCore;
using Valkyrie.Entities;
using Valkyrie.DTOs;
using System.Runtime.InteropServices;
using System.Collections.Immutable;
using Valkyrie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Valkyrie.Extensions;

public static class Endpoints
{
    public static void MapDevboardsEndpoints(this WebApplication app)
    {
        var devboards = app.MapGroup("devboards").RequireAuthorization();

        // GET /devboards
        _ = devboards.MapGet("/", async (QueryModel query, ValkDbContext ctx) =>
        {
            var devs = ctx.DevBoards.AsQueryable();
            List<DevboardDTO>? pageDevboards = null;

            if (query.FilterByPlat is not null)
            {
                pageDevboards = await Task.FromResult(devs.Where(d => d.Platform.ToLower() == query.FilterByPlat.ToLower()).Select(d => d.AsDTO()).ToList());
            }

            switch (query.SortBy)
            {
                case "name":
                    if (query.SortDirection == "desc")
                        pageDevboards = await Task.FromResult(pageDevboards?.OrderByDescending(d => d.Name).ToList() ??
                                                                                                  devs.OrderByDescending(d => d.Name).Select(d => d.AsDTO()).ToList());
                    else if (query.SortDirection == "asc" || query.SortDirection == "" || query.SortDirection!.Any())
                        pageDevboards = await Task.FromResult(pageDevboards?.OrderBy(d => d.Name).ToList() ?? devs.OrderBy(d => d.Name).Select(d => d.AsDTO()).ToList());
                    break;
                case "platform":
                    if (query.SortDirection == "desc")
                        pageDevboards = await Task.FromResult(pageDevboards?.OrderByDescending(d => d.Platform).ToList() ?? 
                                                        devs.OrderByDescending(d => d.Platform).Select(d => d.AsDTO()).ToList());
                    else if (query.SortDirection == "asc" || query.SortDirection == "" || query.SortDirection!.Any())
                        pageDevboards = await Task.FromResult(pageDevboards?.OrderBy(d => d.Platform).ToList() ??
                                                devs.OrderBy(d => d.Platform).Select(d => d.AsDTO()).ToList());
                   break;
                default:
                    if (query.FilterByPlat is null)
                        pageDevboards = await Task.FromResult(devs.Select(d => d.AsDTO()).ToList());
                    break;
            }
            return pageDevboards ?? Enumerable.Empty<DevboardDTO>();
    })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get devboards",
                Description = "Get lists of development boards. Returns ten boards by default.",
                Parameters = new List<OpenApiParameter>()
                {
                    new OpenApiParameter
                    {
                        Name = "filter",
                        Description = "Platform of devboards to display",
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "string" }
                    },
                    new OpenApiParameter
                    {
                        Name = "sortBy",
                        Description = "Property which the boards are sort by",
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "string" }
                    },
                    new OpenApiParameter
                    {
                        Name = "sortDir",
                        Description = "Direction of sort, either asc for ascending and desc for descending. The default is asc.",
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "string" }
                    },
                    new OpenApiParameter
                    {
                        Name = "page",
                        Description = "Page number of boards",
                        In = ParameterLocation.Query,
                        Required = false,
                        Schema = new OpenApiSchema { Type = "int" }
                    }
                }
            })
            .Produces<DevboardDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status200OK)
            .AllowAnonymous();

        // GET devboard/guid
        _ = devboards.MapGet("/{guid}", async (Guid guid, ValkDbContext ctx) =>
        {
            var devboard = await ctx.DevBoards.FirstOrDefaultAsync(_ => _.Guid == guid);

            return devboard is not null ? Results.Ok(devboard.AsDTO()) : Results.NotFound();
        })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a single develoment board by guid",
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "guid",
                        Description = "Universally Unique Identifier of the development board",
                        In = ParameterLocation.Path,
                        Schema = new OpenApiSchema { Type = "uuid" }
                    }
                }
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .AllowAnonymous();

        // PUT devboards/guid
        _ = devboards.MapPut("/{guid}", async (Guid guid, InputDevboardDTO devboard, ValkDbContext context) =>
        {
            var existingBoard = await context.DevBoards.AsNoTracking().FirstOrDefaultAsync(_ => _.Guid == guid);

            if (existingBoard is null)
                return Results.NotFound();


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
        })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Modify a development board of the specified guid",
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "guid",
                        Description = "Universally Unique Identifier of the development board",
                        In = ParameterLocation.Path,
                        Schema = new OpenApiSchema { Type = "uuid" }
                    }
                }
            })
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

        // POST deveboards
        _ = devboards.MapPost("/", async (InputDevboardDTO devboard, ValkDbContext context) =>
        {
            var newBoard = new DevBoard
            {
                Guid = Guid.NewGuid(),
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
        })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create new development board"
            })
            .Produces(StatusCodes.Status201Created);

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
        })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete the development board of the specified guid",
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "guid",
                        Description = "Universally Unique Identifier of the development board",
                        In = ParameterLocation.Path,
                        Schema = new OpenApiSchema { Type = "uuid" }
                    }
                }
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);


    }

    public static void MapSBCsEndpoints(this WebApplication app)
    {
        var sbcs = app.MapGroup("sbcs").RequireAuthorization();

        // GET sbcs
        _ = sbcs.MapGet("/", async (ValkDbContext ctx) => await ctx.SBC.Select(_ => _.AsDTO()).ToListAsync())
                .WithName("GetSBCs")
                .WithOpenApi()
                .AllowAnonymous();

        // GET sbcs/guid
        _ = sbcs.MapGet("/{guid}", async (Guid guid, ValkDbContext ctx) =>
        {
            var sbc = await ctx.SBC.FirstOrDefaultAsync(_ => _.Guid == guid);
            return sbc is not null ? Results.Ok(sbc.AsDTO()) : Results.NotFound();
        });

        // PUT sbcs/guid
        _ = sbcs.MapPut("/{guid}", async (Guid guid, InputSBCDTO sbc, ValkDbContext ctx) =>
        {
            var existingSBC = await ctx.SBC.AsNoTracking().FirstOrDefaultAsync(_ => _.Guid == guid);

            if (existingSBC is null)
                return Results.NotFound();

            var updatedSBC = existingSBC with
            {
                Name = sbc.Name,
                Description = sbc.Description,
                Platform = sbc.Platform,
                OperatingSystems = sbc.OperatingSystems,
                Sensors = sbc.Sensors,
                ExtraInterfaces = sbc.ExtraInterfaces,
                Power = sbc.Power,
                Pins = sbc.Pins,
                Communications = sbc.Communications,
                IO = sbc.IO,
                NetworkingAndComm = sbc.NetworkingAndComm,
                SpecialFeatures = sbc.SpecialFeatures

            };

            ctx.Update(updatedSBC);
            await ctx.SaveChangesAsync();

            return Results.NoContent();
        });

        // POST sbcs/guid
        _ = sbcs.MapPost("/", async (InputSBCDTO sbc, ValkDbContext ctx) =>
        {
            var newSBC = new SBC
            {
                Guid = Guid.NewGuid(),
                Name = sbc.Name,
                Description = sbc.Description,
                Platform = sbc.Platform,
                OperatingSystems = sbc.OperatingSystems,
                Sensors = sbc.Sensors,
                ExtraInterfaces = sbc.ExtraInterfaces,
                Power = sbc.Power,
                Pins = sbc.Pins,
                Communications = sbc.Communications,
                IO = sbc.IO,
                NetworkingAndComm = sbc.NetworkingAndComm,
                SpecialFeatures = sbc.SpecialFeatures
            };

            ctx.Add(newSBC);
            await ctx.SaveChangesAsync();

            return Results.Created($"sbcs/{newSBC.Guid}", newSBC);
        });

        // DELETE sbcs/guid
        _ = sbcs.MapDelete("/{guid}", async (Guid guid, ValkDbContext ctx) =>
        {

            if (await ctx.SBC.FirstOrDefaultAsync(_ => _.Guid == guid) is SBC sbc)
            {
                ctx.Remove(sbc);
                await ctx.SaveChangesAsync();

                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }


}
