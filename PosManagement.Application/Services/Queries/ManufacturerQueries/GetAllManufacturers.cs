using MediatR;
using PosManagement.Application.Common;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Queries.ManufacturerQueries
{
    public record GetAllManufacturers : IRequest<Result<IReadOnlyList<Manufacturer>>>;
    
}
