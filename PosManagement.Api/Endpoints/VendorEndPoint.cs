using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Application.Services.Queries.VendorQueries;

namespace PosManagement.Api.Endpoints
{
    public static class VendorEndPoint
    {
        public static void MapVendorEndPoint(
            this IEndpointRouteBuilder builder)
        {
            var group = builder
                .MapGroup("/api/Vendors")
                .WithTags("Vendors");

            // POST - Create
            group.MapPost("/", async (
                CreateVendor command,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(command, ct);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(result.Errors);
            });

            // GET - Get All
            group.MapGet("/", async (
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(
                    new GetAllVendor(),
                    ct);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(result.Errors);
            });

            // GET - Get By Id
            group.MapGet("/{id:int}", async (
                int id,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(
                    new GetVendorById(id),
                    ct);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.NotFound(result.Errors);
            });

            // PUT - Update
            group.MapPut("/{id:int}", async (
                int id,
                UpdateVendor command,
                IMediator mediator,
                CancellationToken ct) =>
            {
                if (id != command.Id)
                    return Results.BadRequest(
                        "Vendor Id mismatch.");

                var result = await mediator.Send(
                    command,
                    ct);

                if (result.IsSuccess)
                    return Results.NoContent();

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.NotFound))
                    return Results.NotFound(result.Errors);

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.Conflict))
                    return Results.Conflict(result.Errors);

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.Validation))
                    return Results.BadRequest(result.Errors);

                return Results.BadRequest(result.Errors);
            });

            // DELETE
            group.MapDelete("/{id:int}", async (
                int id,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(
                    new DeleteVendor(id),
                    ct);

                if (result.IsSuccess)
                    return Results.NoContent();

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.NotFound))
                    return Results.NotFound(result.Errors);

                if (result.Errors.Any(e =>
                    e.Type == ErrorType.Conflict))
                    return Results.Conflict(result.Errors);

                return Results.BadRequest(result.Errors);
            });
        }
    }
}