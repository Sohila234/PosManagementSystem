using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturer, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateManufacturerCommandHandler( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateManufacturer request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return  Error.Validation("Manufacturer.EmptynName", "Manufacturer Name Is Requierd" );

            var Manufacturer = new Manufacturer
            {
                Name = request.Name,
            };
            unitOfWork.GetRepository<Manufacturer>().Add(Manufacturer);
            await unitOfWork.SaveChangesAsync();
            return Manufacturer.Id;


        }
    }
}
