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
    public class RGPMasterConfiguration: IEntityTypeConfiguration<RGPMaster>
    {
        public void Configure(EntityTypeBuilder<RGPMaster>builder)
        {
            builder.HasKey(x => x.RGPId);
            builder.Property(x => x.RGPId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.GatePassMaster)
               .WithMany(x => x.RGPMaster)
               .HasForeignKey(x => x.GatePassId)
               .IsRequired(true)
             .OnDelete(DeleteBehavior.NoAction);
             builder.Property(x => x.TotalNoOfRGPItems).IsRequired(true);
            builder.Property(x => x.PartCode).IsRequired(true);
            builder.Property(x => x.ProductSerialKey).IsRequired(false);
            builder.Property(x => x.RGPDate).IsRequired(true);
            builder.Property(x => x.Reason).IsRequired(false);
            builder.Property(x => x.Remark).IsRequired(false);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);


        }
    }
}
