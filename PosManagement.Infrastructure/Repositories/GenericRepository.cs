using Microsoft.EntityFrameworkCore;
using PosManagement.Domain.Interfaces;
using PosManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PosDB posDB;

        public GenericRepository(PosDB posDB ) 
        {
            this.posDB = posDB;
        }
        public void Add(T entity)
        {
            posDB.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            posDB.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            posDB.Set<T>().Update(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
     Func<IQueryable<T>, IQueryable<T>>? include = null,
     CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = posDB.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int Id, CancellationToken ct = default , Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = posDB.Set<T>();

            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == Id, ct);
        }

        
    }
}
