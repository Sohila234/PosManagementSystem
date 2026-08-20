using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Application.Services.Commands.Model;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ModelServices
{
    public class CreateModelrCommandHandler : IRequestHandler<CreateModel, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModelrCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateModel request, CancellationToken cancellationToken)
        {

            var manufacturer =
        await unitOfWork
            .GetRepository<Manufacturer>()
            .GetByIdAsync(
                request.ManufacturerId,
                cancellationToken);

            if (manufacturer == null)
            {
                return Result<int>.Fail(
                    Error.NotFound(
                        "Manufacturer.NotFound",
                        "Manufacturer not found."));
            }

            var model = manufacturer.AddModel(request.Name);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return model.Id;
        }
    }
}
