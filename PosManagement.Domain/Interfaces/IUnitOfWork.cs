using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
