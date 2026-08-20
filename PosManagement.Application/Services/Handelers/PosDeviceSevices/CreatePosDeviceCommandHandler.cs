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
    public class CreatePosDeviceCommandHandler : IRequestHandler<CreatePosDevice, Result <int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreatePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreatePosDevice request,
            CancellationToken cancellationToken)
        {
            
            var devices = await unitOfWork
                .GetRepository<PosDevice>()
                .GetAllAsync(cancellationToken: cancellationToken);

            bool serialExists = devices.Any(d =>
                d.SerialNumber.Equals(
                    request.SerialNumber,
                    StringComparison.OrdinalIgnoreCase));

            if (serialExists)
            {
                return Result<int>.Fail(
                    Error.Conflict(
                        "PosDevice.SerialExists",
                        "A device with this serial number already exists."));
            }
            var model = await unitOfWork
                .GetRepository<Model>()
                .GetByIdAsync(
                    request.ModelId,
                    cancellationToken);

            if (model == null)
            {
                return Result<int>.Fail(Error.NotFound(
                        "Model.NotFound",
                        "Model not found."));
            }
            
            var vendor = await unitOfWork
                .GetRepository<Vendor>()
                .GetByIdAsync(
                    request.VendorId,
                    cancellationToken);

            if (vendor == null)
            {
                return Result<int>.Fail(
                    Error.NotFound(
                        "Vendor.NotFound",
                        "Vendor not found."));
            }
            PosDevice device;
            try
            {
                device = new PosDevice(
                    request.SerialNumber,
                    request.ModelId,
                    request.VendorId);
            }
            catch (ArgumentException ex)
            {
                return Result<int>.Fail(
                    Error.Validation(
                        "PosDevice.InvalidData",
                        ex.Message));
            }
            unitOfWork
                .GetRepository<PosDevice>()
                .Add(device); await unitOfWork.SaveChangesAsync(cancellationToken);
            return device.Id;
        }
    }
}

