using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Model;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ModelServices
{
    public class DeleteModelrCommandHandler : IRequestHandler<DeleteModel, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteModelrCommandHandler( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteModel request, CancellationToken cancellationToken)
        {
            var model = await unitOfWork.GetRepository<Model>().GetByIdAsync(request.Id);
            if (model == null)
            {
                return Result.Fail(Error.NotFound("Model.NotFound", "Model not found."));
            }
            unitOfWork.GetRepository<Model>().Delete(model);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
