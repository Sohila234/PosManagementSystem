using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturer, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateManufacturer request, CancellationToken cancellationToken)
        {
            var manufacturer = await unitOfWork.GetRepository<Manufacturer>().GetByIdAsync(request.Id, cancellationToken);
            if (manufacturer == null)
                return Result.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            var manufacturers = await unitOfWork
            .GetRepository<Manufacturer>()
            .GetAllAsync(cancellationToken: cancellationToken);

            bool manufacturerExists = manufacturers.Any(m =>
                m.Id != request.Id &&
                m.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (manufacturerExists)
            {
                return Result.Fail(
                    Error.Conflict(
                        "Manufacturer.AlreadyExists",
                        "A manufacturer with this name already exists."));
            }
            try
            {
                manufacturer.ChangeName(request.Name);
            }
            catch (ArgumentException ex)
            {
                return Result.Fail(
                    Error.Validation(
                        "Manufacturer.EmptyName",
                        ex.Message));
            }
            unitOfWork.GetRepository<Manufacturer>().Update(manufacturer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
