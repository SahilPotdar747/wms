using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class GRNConfiguration : IEntityTypeConfiguration<GRN>
    {
        public void Configure(EntityTypeBuilder<GRN> builder)
        {     
            builder.HasKey(x => x.GRNId);
            builder.Property(x => x.GRNId).ValueGeneratedOnAdd();
            builder.Property(x => x.GRNNumber).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.PONumber).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.POProductQuantity).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.InvoiceNumber).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.InvoiceProductQuantity).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.POFile).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.InvoiceFile).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Remark).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Status).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.HasOne(x => x.ProductMaster)
                .WithMany(x => x.GRN)
                .HasForeignKey(x => x.ProductMasterId)
                .IsRequired(true);
            builder.HasOne(x => x.SupplierMaster)
                .WithMany(x => x.GRN)
                .HasForeignKey(x => x.SupplierId)
                .IsRequired(true);
            builder.HasOne(x => x.Location)
                .WithMany(x => x.GRN)
                .HasForeignKey(x => x.LocationId)
                .IsRequired(true);

        }
    }
}
