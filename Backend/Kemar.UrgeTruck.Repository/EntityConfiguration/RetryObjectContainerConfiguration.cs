using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class RetryObjectContainerConfiguration : IEntityTypeConfiguration<RetryObjectContainer>
    {
        public void Configure(EntityTypeBuilder<RetryObjectContainer> builder)
        {
            builder.HasKey(x => x.RetryObjectContainerId);
            builder.Property(x => x.RetryObjectContainerId).ValueGeneratedOnAdd();
            builder.Property(x => x.NotifierName).HasMaxLength(20).IsRequired(true);
            builder.Property(x => x.ReceiverName).HasMaxLength(20).IsRequired(true);
            builder.Property(x => x.IsProcessed).HasDefaultValue(false);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.NoOfRetry).HasDefaultValue(0);
        }
    }
}
