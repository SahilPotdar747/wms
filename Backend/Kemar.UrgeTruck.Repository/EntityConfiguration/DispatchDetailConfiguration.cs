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
    public class DispatchDetailConfiguration : IEntityTypeConfiguration<DispatchDetail>
    {
        public void Configure(EntityTypeBuilder<DispatchDetail> builder)
        {
            builder.HasKey(x => x.DispatchDetailId);
            builder.Property(x => x.DispatchDetailId).ValueGeneratedOnAdd();
            builder.Property(x => x.DispatchDate).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.PartyDetails).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.VehicleNumber).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ItemBarcode).IsRequired(true).HasMaxLength(50);
            builder.HasOne(x => x.Dispatch)
                .WithMany(x => x.DispatchDetails)
                .HasForeignKey(x => x.DispatchId)
                .IsRequired(true);
            builder.HasOne(x => x.Products)
               .WithMany(x => x.DispatchDetails)
               .HasForeignKey(x => x.ProductId)
               .IsRequired(true);
        }
    }
}
