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
    public class GatePassDetailsConfiguration:IEntityTypeConfiguration<GatePassDetails>
    {
        public void Configure(EntityTypeBuilder<GatePassDetails> builder)

        {
            builder.HasKey(x => x.GPDId);
            builder.Property(x => x.GPDId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.GatePassMaster)
               .WithMany(x => x.GatePassDetails)
               .HasForeignKey(x => x.GatePassId)
               .IsRequired(true);

        

            builder.Property(x => x.PartCode).IsRequired(false);
            builder.Property(x => x.AcceptedQuantity).IsRequired(true);
            builder.Property(x => x.RejectedQuantity).IsRequired(true);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
  }
    }
}

