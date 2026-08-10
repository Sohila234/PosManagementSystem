using Microsoft.EntityFrameworkCore;
using PosManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasOne( x=> x.Manufacturer)
                .WithMany(x=>x.Models)
                .HasForeignKey( x=> x.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
