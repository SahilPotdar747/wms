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
    public class GatePassMasterConfiguration : IEntityTypeConfiguration<GatePassMaster>
    {
        public void Configure(EntityTypeBuilder<GatePassMaster> builder)
        {
            builder.HasKey(x => x.GatePassId);
            builder.Property(x => x.GatePassId).ValueGeneratedOnAdd();
            builder.Property(x => x.GatePassNo).IsRequired(false);
            builder.HasOne(x => x.PurchaseOrderMaster)
                .WithMany(x => x.GatePassMaster)
                .HasForeignKey(x => x.POId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.DeliveryMode).IsRequired(false);
            builder.Property(x => x.RGPGenerated).IsRequired(true);
            builder.Property(x => x.VehicleNo).IsRequired(false);
            builder.Property(x => x.GatePassDate).IsRequired(true);
            builder.Property(x => x.InvoiceNo).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);

        }
    }
}
