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
    public class UpdateModelrCommandHandler : IRequestHandler<UpdateModel, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateModelrCommandHandler( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateModel request, CancellationToken cancellationToken)
        {
            var model = await unitOfWork.GetRepository<Model>().GetByIdAsync(request.Id, cancellationToken);
            if (model == null)
                return Result.Fail(Error.NotFound("model.NotFound", "model not found."));
            model.Name = request.Name;
            model.ManufacturerId = request.ManufacturerId;
            unitOfWork.GetRepository<Model>().Update(model);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
