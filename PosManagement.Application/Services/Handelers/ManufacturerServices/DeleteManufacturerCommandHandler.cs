using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturer, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteManufacturer request, CancellationToken cancellationToken)
        {
            var Manufacturer = await unitOfWork.GetRepository<Manufacturer>().GetByIdAsync(request.Id);
            if (Manufacturer == null)
            {
                return Result.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            }
            unitOfWork.GetRepository<Manufacturer>().Delete(Manufacturer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();

        }
    }
}
