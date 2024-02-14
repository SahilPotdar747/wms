using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class CommonMasterDataConfiguration : IEntityTypeConfiguration<CommonMasterData>
    {
        public void Configure(EntityTypeBuilder<CommonMasterData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.DataItemId).IsUnique(true);
            builder.Property(x => x.Parameter).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.Value).IsRequired(true).HasMaxLength(150);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
