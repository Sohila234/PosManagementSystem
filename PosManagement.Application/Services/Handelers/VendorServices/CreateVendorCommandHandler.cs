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
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendor, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateVendorCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateVendor request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<int>.Fail(Error.Validation("Vendor.EmptyName", "Vendor Name Is Required"));
            var vendor = new Vendor
            {
                Name= request.Name
            };
            unitOfWork.GetRepository<Vendor>().Add(vendor);
            await unitOfWork.SaveChangesAsync();
            return vendor.Id ;


        }
    }
}
