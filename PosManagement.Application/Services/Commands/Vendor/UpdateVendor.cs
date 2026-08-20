using MediatR;
using PosManagement.Application.Common;

namespace PosManagement.Application.Services.Commands.Vendor
{
    public record UpdateVendor(
        int Id,
        string Name) : IRequest<Result>;
}