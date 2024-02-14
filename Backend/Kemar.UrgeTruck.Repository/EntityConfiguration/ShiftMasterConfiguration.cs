using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class ShiftMasterConfiguration : IEntityTypeConfiguration<ShiftMaster>
    {
        public void Configure(EntityTypeBuilder<ShiftMaster> builder)
        {
            builder.HasKey(x => x.ShiftId);
            builder.Property(x => x.ShiftName).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.ShiftId).ValueGeneratedOnAdd();
            builder.Property(x => x.StartTime).IsRequired(true);
            builder.Property(x => x.EndTime).IsRequired(true);
        }
    }
}
