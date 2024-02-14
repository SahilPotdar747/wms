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
    public class DeliveryChallanDetailsConfiguration : IEntityTypeConfiguration<DeliveryChallanDetails>
    {
        public void Configure(EntityTypeBuilder<DeliveryChallanDetails> builder)
        {
            builder.HasKey(x => x.DCDId);
            builder.Property(x => x.DCDId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.DeliveryChallanMaster)
                  .WithMany(x => x.DeliveryChallanDetails)
                  .HasForeignKey(x => x.DCMId)
                  .IsRequired(true)
                  .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.GRNDetailsId).IsRequired(true);
            builder.Property(x => x.ChallanNumber).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Status).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }

    }
}

