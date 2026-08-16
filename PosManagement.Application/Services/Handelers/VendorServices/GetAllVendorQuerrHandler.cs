using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Queries.VendorQueries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.VendorServices
{
    public class GetAllVendorQuerrHandler : IRequestHandler<GetAllVendor, Result<IReadOnlyList<Vendor>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllVendorQuerrHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IReadOnlyList<Vendor>>> Handle(GetAllVendor request, CancellationToken cancellationToken)
        {
            var Vendors= await unitOfWork.GetRepository<Vendor>().GetAllAsync();
            return Result<IReadOnlyList<Vendor>>.Ok(Vendors);
        }
    }
}
