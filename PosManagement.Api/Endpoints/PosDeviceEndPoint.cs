using MediatR;
using PosManagement.Application.Services.Commands;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Application.Services.Queries;

namespace PosManagement.Api.Endpoints
{
    public static class PosDeviceEndPoint
    {
        public static void MapPosDeviceEndPoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/PosDevice")
                           .WithTags("PosDevice");

            group.MapPost("/", async (CreatePosDevices command, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(command, ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });


            group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetAllPosDevices(), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });


            group.MapGet("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetPosDeviceById(id), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
            });


            group.MapPut("/{id:int}", async (int id, UpdatePosDevice command, IMediator mediator, CancellationToken ct) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("PosDevice Not Found");

                var result = await mediator.Send(command, ct);
                return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
            });

            group.MapDelete("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new DeletePosDevice(id), ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Errors);
            });
        }
    }
}
