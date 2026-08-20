using MediatR;
using Microsoft.EntityFrameworkCore;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class DeleteVendorCommandHandler
        : IRequestHandler<DeleteVendor, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteVendor request,
            CancellationToken cancellationToken)
        {
            // 1. Get Vendor with its PosDevices
            var vendor = await unitOfWork
                .GetRepository<Vendor>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken,
                    query => query.Include(v => v.PosDevices));

            // 2. Vendor doesn't exist
            if (vendor == null)
            {
                return Result.Fail(
                    Error.NotFound(
                        "Vendor.NotFound",
                        "Vendor not found."));
            }

            // 3. Cannot delete Vendor with PosDevices
            if (vendor.PosDevices.Any())
            {
                return Result.Fail(
                    Error.Conflict(
                        "Vendor.CannotDelete",
                        "Cannot delete vendor because it has PosDevices."));
            }

            // 4. Delete
            unitOfWork
                .GetRepository<Vendor>()
                .Delete(vendor);

            // 5. Save
            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}