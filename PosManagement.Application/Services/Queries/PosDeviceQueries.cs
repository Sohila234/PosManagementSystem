using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Queries
{
    public record GetAllPosDevices : IRequest<Result<IReadOnlyList<PosDevice>>>;
    public record GetPosDeviceById(int Id) : IRequest<Result<PosDevice>>;


}
