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
    class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrderMaster>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderMaster> builder)
        {
            builder.HasKey(x => x.POId);
            builder.Property(x => x.POId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.SupplierMaster)
                .WithMany(x => x.PurchaseOrderMaster)
                .HasForeignKey(x => x.SupplierId)
                .IsRequired(true);
            builder.Property(x => x.PONumber).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.DeliveryDate).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.Status).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }
    }
}
