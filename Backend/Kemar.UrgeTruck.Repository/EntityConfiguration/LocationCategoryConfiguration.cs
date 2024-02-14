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
    public class LocationCategoryConfiguration : IEntityTypeConfiguration<LocationCategory>
    {
        public void Configure(EntityTypeBuilder<LocationCategory> builder)
        {
            builder.HasKey(x => x.LocationCategoryId);
            builder.Property(x => x.LocationCategoryId).ValueGeneratedOnAdd();
            builder.Property(x => x.LocationCategoryName).IsRequired(true).HasMaxLength(50);

        }
    }
}
