using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Application.Services.Commands.Model;
using PosManagement.Application.Services.Queries.ManufacturerQueries;
using PosManagement.Application.Services.Queries.ModelQueries;

namespace PosManagement.Api.Endpoints
{
    public static class ManufacturerEndpoints 
    {
        public static void MapManufacturerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/Manufacturer")
                           .WithTags("Manufacturer");

            group.MapPost("/", async (
            CreateManufacturer command,
            IMediator mediator,
            CancellationToken ct) =>
            {
                var result = await mediator.Send(command, ct);

                if (result.IsSuccess)
                    return Results.Ok(result.Data);

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.Conflict))
                    return Results.Conflict(result.Errors);

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.Validation))
                    return Results.BadRequest(result.Errors);

                return Results.BadRequest(result.Errors);
            });


            group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetAllManufacturers(), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });

           
            group.MapGet("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetManufacturerById(id), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
            });


            group.MapPut("/{id:int}", async (
                     int id,
                    UpdateManufacturer command,
                    IMediator mediator,
                    CancellationToken ct) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Manufacturer Id mismatch.");

                var result = await mediator.Send(command, ct);

                if (result.IsSuccess)
                    return Results.NoContent();

                if (result.Errors.Any(e => e.Type == ErrorType.NotFound))
                    return Results.NotFound(result.Errors);

                if (result.Errors.Any(e => e.Type == ErrorType.Conflict))
                    return Results.Conflict(result.Errors);

                if (result.Errors.Any(e => e.Type == ErrorType.Validation))
                    return Results.BadRequest(result.Errors);

                return Results.BadRequest(result.Errors);
            });

            group.MapDelete("/{id:int}", async (
                 int id,
                IMediator mediator,
                 CancellationToken ct) =>
                {
                var result = await mediator.Send(
                    new DeleteManufacturer(id), ct);

                if (result.IsSuccess)
                    return Results.NoContent();

                if (result.Errors.Any(e => e.Type == ErrorType.NotFound))
                    return Results.NotFound(result.Errors);

                if (result.Errors.Any(e => e.Type == ErrorType.Conflict))
                    return Results.Conflict(result.Errors);

                return Results.BadRequest(result.Errors);
            });
        }
    }
}
