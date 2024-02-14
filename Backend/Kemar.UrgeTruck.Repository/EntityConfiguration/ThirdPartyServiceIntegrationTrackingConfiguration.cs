using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class ThirdPartyServiceIntegrationTrackingConfiguration : IEntityTypeConfiguration<ThirdPartyServiceIntegrationTracking>
    {
        public void Configure(EntityTypeBuilder<ThirdPartyServiceIntegrationTracking> builder)
        {
            builder.HasKey(x => x.IntegrationTrackingId);
            builder.Property(x => x.IntegrationTrackingId).ValueGeneratedOnAdd();

            builder.Property(x => x.RequestJson).IsRequired(true).HasColumnType("nvarchar(max)");
            builder.Property(x => x.TransactionStatus).HasMaxLength(20).IsRequired(true);
        }
    }
}
