using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class Rate
    {
        public int RateId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int LocationCategoryId { get; set; }
        public string TimeUnit { get; set; }
        public int TimeUnitValue { get; set; }
        public decimal RateValue { get; set; }
        public int WarehouseId { get; set; }

        public virtual Product Product { get; set; }
        public virtual LocationCategory LocationCategory { get; set; }
    }
}
