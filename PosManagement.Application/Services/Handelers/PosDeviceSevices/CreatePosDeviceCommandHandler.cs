using AutoMapper;
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
    public class CreatePosDeviceCommandHandler : IRequestHandler<CreatePosDevices, Result <int>>
    {
        private readonly IUnitOfWork unitOfWork;
      
        public CreatePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
           
        }
        public async Task<Result<int>> Handle(CreatePosDevices request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SerialNumber))
                return Result<int>.Fail(Error.Validation("PosDevice.EmptyName", "PosDevice Name Is Required."));
            var device = new PosDevice
            {
                SerialNumber = request.SerialNumber,
                ModelId = request.ModelId,
                VendorId = request.VendorId
            };
            unitOfWork.GetRepository<PosDevice>().Add(device);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<int>.Ok(device.Id);
        }
    }
}
