using MediatR;
using Microsoft.EntityFrameworkCore;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Queries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.PosDeviceSevices
{
    public class GetAllPosDevicesQueryHandler : IRequestHandler<GetAllPosDevices, Result<IReadOnlyList<PosDevice>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllPosDevicesQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IReadOnlyList<PosDevice>>> Handle(GetAllPosDevices request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.GetRepository<PosDevice>().GetAllAsync(include: query => query.Include(d => d.Model).Include(d => d.Vendor),cancellationToken);
            return Result<IReadOnlyList<PosDevice>>.Ok(result);
        }
    }
}
