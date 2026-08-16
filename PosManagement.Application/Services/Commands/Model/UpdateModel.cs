using MediatR;
using PosManagement.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Commands.Model
{
    public record UpdateModel(int Id, string Name, int ManufacturerId) : IRequest<Result>;
}