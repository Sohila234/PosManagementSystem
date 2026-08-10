using MediatR;
using PosManagement.Application.Common;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Queries.ManufacturerQueries
{
    public record GetManufacturerById(int Id) : IRequest<Result<Manufacturer>>;
    
}
