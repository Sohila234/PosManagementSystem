using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturer, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateManufacturer request, CancellationToken cancellationToken)
        {
            var manufacturer = await unitOfWork.GetRepository<Manufacturer>().GetByIdAsync(request.Id, cancellationToken);
            if (manufacturer == null)
                return Result<bool>.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            manufacturer.Name = request.Name;
            unitOfWork.GetRepository<Manufacturer>().Update(manufacturer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Ok(true);
        }
    }
}
