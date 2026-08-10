using MediatR;
using PosManagement.Application.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using PosManagement.Application.Services.Queries.ManufacturerQueries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerById, Result<Manufacturer>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetManufacturerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Manufacturer>> Handle(GetManufacturerById request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.GetRepository<Manufacturer>().GetByIdAsync(request.Id,cancellationToken);
            if (result == null)
            {
                return Result<Manufacturer>.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            }
            return Result<Manufacturer>.Ok(result);
        }
    }
}
