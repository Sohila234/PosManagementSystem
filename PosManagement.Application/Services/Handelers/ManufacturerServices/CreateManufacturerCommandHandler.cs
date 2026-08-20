using MediatR;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;

namespace PosManagement.Application.Services.Handelers.ManufacturerServices
{
    public class CreateManufacturerCommandHandler
        : IRequestHandler<CreateManufacturer, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            CreateManufacturer request,
            CancellationToken cancellationToken)
        {
            var manufacturers = await unitOfWork
                .GetRepository<Manufacturer>()
                .GetAllAsync(
                    cancellationToken: cancellationToken);

            bool manufacturerExists = manufacturers.Any(m =>
                m.Name.Equals(
                    request.Name,
                    StringComparison.OrdinalIgnoreCase));

            if (manufacturerExists)
            {
                return Result<int>.Fail(
                    Error.Conflict(
                        "Manufacturer.AlreadyExists",
                        "A manufacturer with this name already exists."));
            }

            try
            {
                var manufacturer = new Manufacturer(request.Name);

                unitOfWork
                    .GetRepository<Manufacturer>()
                    .Add(manufacturer);

                await unitOfWork
                    .SaveChangesAsync(cancellationToken);

                return manufacturer.Id;
            }
            catch (ArgumentException ex)
            {
                return Result<int>.Fail(
                    Error.Validation(
                        "Manufacturer.InvalidData",
                        ex.Message));
            }
        }
    }
}