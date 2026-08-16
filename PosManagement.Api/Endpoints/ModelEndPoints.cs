using MediatR;
using PosManagement.Application.Services.Commands.Model;
using PosManagement.Application.Services.Queries.ModelQueries;

namespace PosManagement.Api.Endpoints
{
    public static class ModelEndPoints
    {
        public static void MapModelEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/Models")
                           .WithTags("Models");

            group.MapPost("/", async (CreateModel command, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(command, ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });

            
            group.MapGet("/", async (IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetAllModels(), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });

           
            group.MapGet("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new GetModelById(id), ct);
                return result.IsSuccess ? Results.Ok(result.Data) : Results.NotFound(result.Errors);
            });

            
            group.MapPut("/{id:int}", async (int id, UpdateModel command, IMediator mediator, CancellationToken ct) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Model Not Found");

                var result = await mediator.Send(command, ct);
                return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
            });

            group.MapDelete("/{id:int}", async (int id, IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new DeleteModel(id), ct);
                return result.IsSuccess ? Results.NoContent() : Results.NotFound(result.Errors);
            });
        }
    }
    
}
