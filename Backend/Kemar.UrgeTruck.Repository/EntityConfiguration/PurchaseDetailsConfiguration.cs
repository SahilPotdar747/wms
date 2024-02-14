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
    public class PurchaseDetailsConfiguration: IEntityTypeConfiguration<PurchaseOrderDetails>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetails> builder)
        {
            builder.HasKey(x => x.PODId);
            builder.Property(x => x.PODId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.PurchaseOrderMaster)
                .WithMany(x => x.PurchaseOrderDetails)
                .HasForeignKey(x => x.POId)
                .IsRequired(true);
            builder.HasOne(x => x.ProductMaster)
                .WithMany(x => x.PurchaseOrderDetails)
                .HasForeignKey(x => x.ProductMasterId)
                .IsRequired(true);
            builder.Property(x => x.Quantity).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Amount).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Status).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);



        }
    }
}
