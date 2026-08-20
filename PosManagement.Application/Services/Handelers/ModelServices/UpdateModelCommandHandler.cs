using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Model;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PosManagement.Application.Services.Handelers.ModelServices
{
    public class UpdateModelCommandHandler
        : IRequestHandler<UpdateModel, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateModelCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<Result> Handle(
            UpdateModel request,
            CancellationToken cancellationToken)
        {
            var model = await unitOfWork
                .GetRepository<Model>()
                .GetByIdAsync(
                    request.ModelId,
                    cancellationToken,
                    query => query.Include(m => m.Manufacturer));

            if (model == null)
                return Result.Fail(
                    Error.NotFound(
                        "model.NotFound",
                        "Model not found."));

            model.Manufacturer!.UpdateModel(
                model.Id,
                request.NewName);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}