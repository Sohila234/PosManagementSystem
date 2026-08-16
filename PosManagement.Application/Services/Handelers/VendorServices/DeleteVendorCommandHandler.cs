using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendor, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteVendor request, CancellationToken cancellationToken)
        {
            var vendor = await unitOfWork.GetRepository<Vendor>().GetByIdAsync(request.Id, cancellationToken);
            if (vendor == null)
            {
                return Result.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            }
            unitOfWork.GetRepository<Vendor>().Delete(vendor);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
