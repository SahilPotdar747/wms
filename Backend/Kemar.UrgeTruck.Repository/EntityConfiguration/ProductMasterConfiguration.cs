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
    public class ProductMasterConfiguration : IEntityTypeConfiguration<ProductMaster>
    {
        public void Configure(EntityTypeBuilder<ProductMaster> builder)
        {
            builder.HasKey(x => x.ProductMasterId);
            builder.Property(x => x.ProductMasterId).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.PartCode).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.HSNCode).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Make).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(250);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.HasOne(x => x.ProductCategory)
               .WithMany(x => x.ProductMaster)
               .HasForeignKey(x => x.ProductCategoryId)
               .IsRequired(true);
        }
    }
}
