using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosManagement.Infrastructure.Data.Configurations
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
