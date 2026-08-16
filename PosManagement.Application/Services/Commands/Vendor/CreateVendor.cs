using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands.Vendor
{
    public record CreateVendor(string Name) : IRequest<Result<int>>;
   
}
