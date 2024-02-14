using Kemar.UrgeTruck.Repository.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.EntityConfiguration
{
    public class CustomerMasterConfiguration : IEntityTypeConfiguration<CustomerMaster>
    {
        public void Configure(EntityTypeBuilder<CustomerMaster> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ContactNo).IsRequired(false).HasMaxLength(12);
            builder.Property(x => x.EmailId).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.PinCode).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.City).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.State).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Country).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Remark).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedBy).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ModifiedBy).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.CreatedDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);

        }
    }
    
    
}
