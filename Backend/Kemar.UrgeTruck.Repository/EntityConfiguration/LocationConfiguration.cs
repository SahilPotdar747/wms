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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.LocationId);
            builder.Property(x => x.LocationId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LocationCode).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ParentLocationCode).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LocationType).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.DIsplayName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Latitude).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Longitude).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.HasOne(x => x.LocationCategory)
               .WithMany(x => x.Locations)
               .HasForeignKey(x => x.LocationCategoryId)
               .IsRequired(true);

        }
    }
}
