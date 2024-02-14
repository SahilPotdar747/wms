using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class PurchaseOrderRequest : CommonEntityModel
    {


        public PurchaseOrderRequest()
        {
            PurchaseOrdeDetails = new List<PurchaseOrdeDetailsrRequest>();
        }
        public int POId { get; set; }
        public int SupplierId { get; set; }
        
        public string PONumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<PurchaseOrdeDetailsrRequest> PurchaseOrdeDetails { get; set; }
    }
}
