using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Queries.VendorQueries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class GetAllVendorQueryHandler
        : IRequestHandler<GetAllVendor, Result<IReadOnlyList<Vendor>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllVendorQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<Vendor>>> Handle(
            GetAllVendor request,
            CancellationToken cancellationToken)
        {
            var vendors = await unitOfWork
                .GetRepository<Vendor>()
                .GetAllAsync(
                    cancellationToken: cancellationToken);

            return Result<IReadOnlyList<Vendor>>.Ok(vendors);
        }
    }
}