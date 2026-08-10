using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands.Manufacturer
{
    public record CreateManufacturer(string Name) : IRequest<Result<int>>;
    
}
