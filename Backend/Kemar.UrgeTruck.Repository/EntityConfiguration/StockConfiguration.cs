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
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.StockId);
            builder.Property(x => x.StockId).ValueGeneratedOnAdd();
            builder.Property(x => x.StockEntryDate).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.StockExitDate).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ItemBarcode).IsRequired(true).HasMaxLength(50);

            builder.HasOne(x => x.Products)
                .WithMany(x => x.Stocks)
                .HasForeignKey(x => x.ProductId)
                .IsRequired(true);

            builder.HasOne(x => x.Locations)
               .WithMany(x => x.Stock)
               .HasForeignKey(x => x.StockId)
               .IsRequired(true);

        }
    }
}
