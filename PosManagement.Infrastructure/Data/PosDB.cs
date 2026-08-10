using Microsoft.EntityFrameworkCore;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Data
{
    public  class PosDB :DbContext
    {
        public PosDB(DbContextOptions<PosDB> options) :base(options)
        {
            
        }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<PosDevice> PosDevices { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PosDB).Assembly);
        }


    }
}
