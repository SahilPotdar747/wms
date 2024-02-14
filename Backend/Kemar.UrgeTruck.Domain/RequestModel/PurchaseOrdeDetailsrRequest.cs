using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class PurchaseOrdeDetailsrRequest:CommonEntityModel
    {
        public int PODId { get; set; }
        public int POId { get; set; }
        public long ProductMasterId { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string PONumber { get; set; }
        public DateTime? DeliveryDate { get; set; }

       // public virtual PurchaseOrderRequest PurchaseOrderRequest { get; set; }
        //public virtual ProductMasterRequest ProductMasterRequest { get; set; }
    }
}
