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

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct =default)
        {
            return await posDB.Set<T>().AsNoTracking().ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(int Id, CancellationToken ct = default)
        {
            return await posDB.Set<T>().FindAsync(Id , ct);
        }

        
    }
}
