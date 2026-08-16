using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PosManagement.Application.Common;
using PosManagement.Application.Services.Queries.ModelQueries;
using PosManagement.Domain.Entities;
using PosManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Handelers.ModelServices
{
    public class GetModelByIdQueryHandler : IRequestHandler<GetModelById, Result<Model>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetModelByIdQueryHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Model>> Handle(GetModelById request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.GetRepository<Model>().GetByIdAsync(request.Id, cancellationToken , include: q => q.Include(m => m.Manufacturer));
            if (result == null)
            {
                return Result<Model>.Fail(Error.NotFound("Model.NotFound", "Model not found."));
            }
            

            return Result<Model>.Ok(result);
        }
    }
}
