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
    public class DeliveryChallanMasterConfiguration :IEntityTypeConfiguration<DeliveryChallanMaster>

    {
        public void Configure(EntityTypeBuilder<DeliveryChallanMaster> builder)
        {
            builder.HasKey(x => x.DCMId);
            builder.Property(x => x.DCMId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.GRN)
                  .WithMany(x => x.DeliveryChallanMaster)
                  .HasForeignKey(x => x.GRNId)
                  .IsRequired(true);
            builder.Property(x => x.DcStatus).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.DeliveryDate).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Status).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);

        }
    }
}
