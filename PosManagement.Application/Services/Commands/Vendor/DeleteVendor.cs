using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands.Vendor
{
    public record DeleteVendor(int Id) : IRequest<Result>;
    
}
