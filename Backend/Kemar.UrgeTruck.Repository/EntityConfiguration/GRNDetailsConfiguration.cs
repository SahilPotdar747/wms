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
    public class GRNDetailsConfiguration : IEntityTypeConfiguration<GRNDetails>
    {
        public void Configure(EntityTypeBuilder<GRNDetails> builder)
        {
            builder.HasKey(x => x.GRNDetailsId);
            builder.Property(x => x.GRNDetailsId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.GRN)
                .WithMany(x => x.GRNDetails)
                .HasForeignKey(x => x.GRNId)
                .IsRequired(true) 
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.DropLoc).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.RackNo).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.SubRack).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.ProductSerialKey).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Status).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }
    }
}
