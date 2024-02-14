using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Kemar.UrgeTruck.Repository.FRDB.FRDBEntity
{

    public class FRDBContextFactory : IFRDBContext
    {
        private readonly IConfiguration _configuration;


        public FRDBContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FRDBContext CreateKGASContext()
        {
            var options = new DbContextOptionsBuilder<FRDBContext>();
            options.UseSqlServer(_configuration.GetConnectionString("FRDataSQLContext"));
            return new FRDBContext(options.Options);
        }
    }
}
