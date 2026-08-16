using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class GetPosDeviceByIdQueryHandler : IRequestHandler<GetPosDeviceById, Result<PosDevice>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetPosDeviceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<PosDevice>> Handle(GetPosDeviceById request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.GetRepository<PosDevice>().GetByIdAsync(request.Id,
                cancellationToken,
                query => query.Include(d => d.Model).Include(d => d.Vendor));
            if (result == null)
            {
                return Result<PosDevice>.Fail(Error.NotFound("PosDevice.NotFound", "PosDevice not found."));
            }
            return Result<PosDevice>.Ok(result);
        }
    }
}
