using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using PosManagement.Application.Services.Queries.ManufacturerQueries;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class GetAllManufacturersQueryHandler : IRequestHandler<GetAllManufacturers, Result<IReadOnlyList<Manufacturer>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllManufacturersQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IReadOnlyList<Manufacturer>>> Handle(GetAllManufacturers request, CancellationToken cancellationToken)
        {
            var manufacturers = await unitOfWork.GetRepository<Manufacturer>().GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<Manufacturer>>.Ok(manufacturers);
        }
    }
}
