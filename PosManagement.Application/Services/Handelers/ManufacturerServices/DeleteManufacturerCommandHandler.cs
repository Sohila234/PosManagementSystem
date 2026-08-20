using MediatR;
using Microsoft.EntityFrameworkCore;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturer, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteManufacturer request, CancellationToken cancellationToken)
        {
            var manufacturer = await unitOfWork
                .GetRepository<Manufacturer>()
                .GetByIdAsync(
                    request.Id,
                    cancellationToken,
                    query => query.Include(m => m.Models));
            if (manufacturer == null)
            {
                return Result.Fail(Error.NotFound("Manufacturer.NotFound", "Manufacturer not found."));
            }
            try
            {
                manufacturer.Delete();
            }
            catch (InvalidOperationException ex)
            {
                return Result.Fail(
                    Error.Conflict(
                        "Manufacturer.CannotDelete",
                        ex.Message));
            }
            unitOfWork.GetRepository<Manufacturer>().Delete(manufacturer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Ok();

        }
    }
}
