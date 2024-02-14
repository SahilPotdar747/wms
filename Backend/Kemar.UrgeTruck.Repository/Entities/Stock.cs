using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class Stock
    {
        public long StockId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public DateTime StockEntryDate { get; set; }
        public DateTime? StockExitDate { get; set; }
        public string ItemBarcode { get; set; }
        public virtual Product Products { get; set; }
        public virtual Location Locations { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
    }
}
