using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class FailureLogConfiguration : IEntityTypeConfiguration<FailureLog>
    {
        public void Configure(EntityTypeBuilder<FailureLog> builder)
        {
            builder.HasKey(x => x.FailureLogId);
            builder.Property(x => x.FailureLogId).ValueGeneratedOnAdd();
            builder.Property(x => x.LocationId).IsRequired(true);
            builder.Property(x => x.Category).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.FailureTime).IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.RepairTime).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.IsActive).IsRequired(true);
        }
    }
}
