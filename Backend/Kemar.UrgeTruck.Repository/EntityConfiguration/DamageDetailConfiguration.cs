using Kemar.UrgeTruck.Repository.Context;
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
    public class DamageDetailConfiguration : IEntityTypeConfiguration<DamageDetail>
    {
        public void Configure(EntityTypeBuilder<DamageDetail> builder)
        {
            builder.HasKey(x => x.DamageDetailId);
            builder.Property(x => x.DamageDetailId).ValueGeneratedOnAdd();
            builder.Property(x => x.description).IsRequired(true).HasMaxLength(50);

            //builder.HasOne(x => x.GRNDetails)
            //    .WithMany(x => x.DamageDetail)
            //    .HasForeignKey(x => x.GRNDetailsId)
            //    .IsRequired(true);

        }
    }
}
