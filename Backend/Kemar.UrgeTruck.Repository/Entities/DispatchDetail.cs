using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class DispatchDetail
    {
        public int DispatchDetailId { get; set; }
        public int DispatchId { get; set; }
        public int ProductId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string PartyDetails { get; set; }
        public string VehicleNumber { get; set; }
        public string ItemBarcode { get; set; }
        public virtual Product Products { get; set; }
        public virtual Dispatch Dispatch { get; set; }

    }
}