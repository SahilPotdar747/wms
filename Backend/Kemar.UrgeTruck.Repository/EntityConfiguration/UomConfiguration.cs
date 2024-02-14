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
    public class UomConfiguration : IEntityTypeConfiguration<Uom>
    {
        public void Configure(EntityTypeBuilder<Uom> builder)
        {
            builder.HasKey(x => x.UomId);
            builder.Property(x => x.UomId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
        }
    }
}
