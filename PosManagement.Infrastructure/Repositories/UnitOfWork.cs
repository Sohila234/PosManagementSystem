using PosManagement.Domain.Interfaces;
using PosManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosDB posDB;
        private readonly Dictionary<string, object> Repos = [];
        public UnitOfWork(PosDB posDB)
        {
            this.posDB = posDB;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            var TypeName = typeof(T).Name;
            if (Repos.TryGetValue(TypeName, out object oldRepo))
                return (IGenericRepository<T>)oldRepo;
            var newRepo = new GenericRepository<T>(posDB);
            Repos[TypeName] = newRepo;
            return newRepo;

        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await posDB.SaveChangesAsync(ct);
        }
    }
}
