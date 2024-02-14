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
    public class UserLocationAccessConfiguration: IEntityTypeConfiguration<UserLocationAccess>
    {
        public void Configure(EntityTypeBuilder<UserLocationAccess> builder)
        {
            builder.Property(x => x.UserLocationAccessId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Location)
                   .WithMany(y => y.UserLocationAccess)
                   .HasForeignKey(x => x.LocationId)
                   .IsRequired(true);
            builder.HasOne(x => x.UserManager)
                   .WithMany(y => y.UserLocationAccess)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(true);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
