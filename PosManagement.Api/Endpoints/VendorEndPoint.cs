using MediatR;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Application.Services.Queries.ManufacturerQueries;
using PosManagement.Application.Services.Queries.VendorQueries;
using System.Xml.Serialization;

namespace PosManagement.Api.Endpoints
{
    public static class VendorEndPoint
    {
        public static void MapVendorEndPoint (this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/api/Vendors")
                           .WithTags("Vendors");
            group.MapPost("/", async (CreateVendor Command, IMediator mediator, CancellationToken ct) =>
            {
                var result =await mediator.Send(Command, ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });
            group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetAllVendor(), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });
            group.MapGet("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetVendorById(id), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
            });
            group.MapPut("/{id:int}", async (int id, UpdateVendor command, IMediator mediator, CancellationToken ct) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Vendor Not Found");

                var result = await mediator.Send(command, ct);
                return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
            });
            group.MapDelete("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new DeleteVendor(id), ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Errors);
            });

        }
    }
}
