using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.PosDeviceSevices
{
    public class DeletePosDeviceCommandHandler
        : IRequestHandler<DeletePosDevice, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeletePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeletePosDevice request,
            CancellationToken cancellationToken)
        {
            var device = await unitOfWork
                .GetRepository<PosDevice>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (device == null)
            {
                return Result.Fail(
                    Error.NotFound(
                        "PosDevice.NotFound",
                        "PosDevice not found."));
            }

            unitOfWork
                .GetRepository<PosDevice>()
                .Delete(device);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}