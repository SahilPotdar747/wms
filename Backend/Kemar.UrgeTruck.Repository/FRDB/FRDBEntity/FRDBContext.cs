using Kemar.UrgeTruck.Repository.FRDB.FRDBModel;
using Microsoft.EntityFrameworkCore;

namespace Kemar.UrgeTruck.Repository.FRDB.FRDBEntity
{
    public class FRDBContext : DbContext
    {
        public DbContextOptions<FRDBContext> Options { get; }

        public FRDBContext() { }

        public FRDBContext(DbContextOptions<FRDBContext> options)
           : base(options)
        {
            Options = options;
        }

        public virtual DbSet<Mx_ATDEventTrn> Mx_ATDEventTrn { get; set; }
    }
}
