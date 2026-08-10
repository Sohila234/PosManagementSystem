using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Data.Configurations
{
    public class PosDevicConfiguration : IEntityTypeConfiguration<PosDevice>
    {
        public void Configure(EntityTypeBuilder<PosDevice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property( x=> x.SerialNumber ).IsRequired().HasMaxLength(150);
            builder.HasIndex(p => p.SerialNumber) .IsUnique();

            builder.HasOne(x=> x.Model)
                .WithMany(x => x.PosDevices)
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vendor)
               .WithMany(x => x.PosDevices)
               .HasForeignKey(x => x.VendorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
