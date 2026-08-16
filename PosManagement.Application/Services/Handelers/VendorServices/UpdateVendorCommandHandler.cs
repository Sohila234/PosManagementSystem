using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendor, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateVendor request, CancellationToken cancellationToken)
        {
            var vendor = await unitOfWork.GetRepository<Vendor>().GetByIdAsync(request.Id, cancellationToken);
            if (vendor == null)
            {
                return Result.Fail(Error.NotFound("Vendor.NotFound", "Vendor not found."));
            }
            vendor.Name= request.Name;
            unitOfWork.GetRepository<Vendor>().Update(vendor);
            await unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
