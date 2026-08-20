using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Queries.VendorQueries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class GetByIdVendorQueryHandler
        : IRequestHandler<GetVendorById, Result<Vendor>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetByIdVendorQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Vendor>> Handle(
            GetVendorById request,
            CancellationToken cancellationToken)
        {
            var vendor = await unitOfWork
                .GetRepository<Vendor>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (vendor == null)
            {
                return Result<Vendor>.Fail(
                    Error.NotFound(
                        "Vendor.NotFound",
                        "Vendor not found."));
            }

            return Result<Vendor>.Ok(vendor);
        }
    }
}