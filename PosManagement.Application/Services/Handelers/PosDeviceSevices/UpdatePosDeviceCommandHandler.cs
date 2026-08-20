using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.PosDeviceSevices
{
    public class UpdatePosDeviceCommandHandler
        : IRequestHandler<UpdatePosDevice, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdatePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            UpdatePosDevice request,
            CancellationToken cancellationToken)
        {
            var device = await unitOfWork
                .GetRepository<PosDevice>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (device == null)
            {
                return Result<int>.Fail(
                    Error.NotFound(
                        "PosDevice.NotFound",
                        "PosDevice not found."));
            }

            var devices = await unitOfWork
                .GetRepository<PosDevice>()
                .GetAllAsync(
                    cancellationToken: cancellationToken);

            bool serialExists = devices.Any(d =>
                d.Id != request.Id &&
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
                return Result<int>.Fail(
                    Error.NotFound(
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

            try
            {
                device.ChangeSerialNumber(request.SerialNumber);

                device.ChangeModelAndVendor(
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
                .Update(device);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Ok(device.Id);
        }
    }
}