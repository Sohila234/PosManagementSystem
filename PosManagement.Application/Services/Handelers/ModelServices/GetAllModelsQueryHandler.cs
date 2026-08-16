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
    public class GetAllModelsQueryHandler : IRequestHandler<GetAllModels, Result<IReadOnlyList<Model>>>
    {
        private readonly IUnitOfWork unitOfWork;
       

        public GetAllModelsQueryHandler(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
            
        }
        public async Task<Result<IReadOnlyList<Model>>> Handle(GetAllModels request, CancellationToken cancellationToken)
        {

            var models = await unitOfWork.GetRepository<Model>()
            .GetAllAsync(include: q => q.Include(m => m.Manufacturer), cancellationToken);
           

            return Result<IReadOnlyList<Model>>.Ok(models);
        }
    }
}
