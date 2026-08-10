using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Domain.Interfaces
{
    public interface IGenericRepository<T> where T :class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
        Task<T?> GetByIdAsync(int Id, CancellationToken ct = default);
    }
}
