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
        Task<IReadOnlyList<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int Id, CancellationToken ct = default , Func<IQueryable<T>, IQueryable<T>>? include = null);
    }
}
