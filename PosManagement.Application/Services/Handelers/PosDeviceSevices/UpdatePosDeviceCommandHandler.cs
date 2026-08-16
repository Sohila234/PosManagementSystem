using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.PosDeviceSevices
{
    public class UpdatePosDeviceCommandHandler : IRequestHandler<UpdatePosDevice, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdatePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdatePosDevice request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.GetRepository<PosDevice>().GetByIdAsync(request.Id, cancellationToken);
            if (value == null)
            {
                return Result<int>.Fail(Error.NotFound("PosDevice.NotFound", "PosDevice not found."));

            }
            value.SerialNumber = request.SerialNumber;
            value.ModelId = request.ModelId;
            value.VendorId = request.VendorId;
            unitOfWork.GetRepository<PosDevice>().Update(value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Ok(value.Id);
        }
    }
}
