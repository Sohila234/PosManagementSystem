using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class UpdateVendorCommandHandler
        : IRequestHandler<UpdateVendor, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateVendor request,
            CancellationToken cancellationToken)
        {
            // 1. Check vendor exists
            var vendor = await unitOfWork
                .GetRepository<Vendor>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (vendor == null)
            {
                return Result.Fail(
                    Error.NotFound(
                        "Vendor.NotFound",
                        "Vendor not found."));
            }

            // 2. Check name uniqueness
            var vendors = await unitOfWork
                .GetRepository<Vendor>()
                .GetAllAsync(
                    cancellationToken: cancellationToken);

            bool vendorExists = vendors.Any(v =>
                v.Id != request.Id &&
                v.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (vendorExists)
            {
                return Result.Fail(
                    Error.Conflict(
                        "Vendor.AlreadyExists",
                        "A vendor with this name already exists."));
            }

            // 3. Change Domain data
            try
            {
                vendor.ChangeName(request.Name);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(
                    Error.Validation(
                        "Vendor.InvalidName",
                        ex.Message));
            }

            // 4. Update repository
            unitOfWork
                .GetRepository<Vendor>()
                .Update(vendor);

            // 5. Save
            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}