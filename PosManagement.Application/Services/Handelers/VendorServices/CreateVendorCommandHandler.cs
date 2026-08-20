using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Vendor;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class CreateVendorCommandHandler
        : IRequestHandler<CreateVendor, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateVendor request,
            CancellationToken cancellationToken)
        {
            var vendors = await unitOfWork
                .GetRepository<Vendor>()
                .GetAllAsync(
                    cancellationToken: cancellationToken);

            bool vendorExists = vendors.Any(v =>
                v.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (vendorExists)
            {
                return Result<int>.Fail(
                    Error.Conflict(
                        "Vendor.AlreadyExists",
                        "A vendor with this name already exists."));
            }

            Vendor vendor;

            try
            {
                vendor = new Vendor(request.Name);
            }
            catch (ArgumentException ex)
            {
                return Result<int>.Fail(
                    Error.Validation(
                        "Vendor.InvalidName",
                        ex.Message));
            }

            unitOfWork
                .GetRepository<Vendor>()
                .Add(vendor);

            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return Result<int>.Ok(vendor.Id);
        }
    }
}