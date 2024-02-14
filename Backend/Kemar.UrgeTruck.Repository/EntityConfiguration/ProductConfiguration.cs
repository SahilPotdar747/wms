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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.ProductId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Product)
                .HasForeignKey(x => x.ProductCategoryId)
                .IsRequired(true);

            builder.HasOne(x => x.Uom)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UomId)
                .IsRequired(true);
        }
    }
}
