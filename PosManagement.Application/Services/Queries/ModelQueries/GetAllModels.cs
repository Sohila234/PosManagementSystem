using MediatR;
using PosManagement.Application.Common;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Application.Services.Queries.ModelQueries
{
    public record GetAllModels : IRequest<Result<IReadOnlyList<Model>>>;

}
