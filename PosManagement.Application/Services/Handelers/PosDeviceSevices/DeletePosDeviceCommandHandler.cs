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
    public class DeletePosDeviceCommandHandler : IRequestHandler<DeletePosDevice, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeletePosDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeletePosDevice request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.GetRepository<PosDevice>().GetByIdAsync(request.Id);
            if (result == null)
            {
                return Result<int>.Fail(Error.NotFound("PosDevice.NotFound", "PosDevice not found."));
            }
            unitOfWork.GetRepository<PosDevice>().Delete(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<int>.Ok(request.Id);
        }

    }
}
