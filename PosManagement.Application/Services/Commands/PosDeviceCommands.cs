using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands
{
    public record CreatePosDevices( string SerialNumber, int ModelId, int VendorId) : IRequest<Result<int>>;
    public record DeletePosDevice(int Id) : IRequest<Result<int>>;
    public record UpdatePosDevice(int Id , string SerialNumber , int ModelId , int VendorId) : IRequest<Result<int>>;

}
