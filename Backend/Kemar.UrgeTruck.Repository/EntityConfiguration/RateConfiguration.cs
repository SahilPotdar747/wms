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
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(x => x.RateId);
            builder.Property(x => x.RateId).ValueGeneratedOnAdd();
            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(50);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Rate)
                .HasForeignKey(x => x.ProductId)
                .IsRequired(true);

            builder.HasOne(x => x.LocationCategory)
               .WithMany(x => x.Rates)
               .HasForeignKey(x => x.LocationCategoryId)
               .IsRequired(true);

         
        }
    }
}
