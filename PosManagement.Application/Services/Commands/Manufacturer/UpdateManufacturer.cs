using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands.Manufacturer
{
    public record UpdateManufacturer : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
   
}
