using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class PurchaseOrderResponse
    {

        public int POId { get; set; }
        public int SupplierId { get; set; }
        
        public string PONumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }


        public SupplierMasterResponse SupplierMaster { get; set; }
        public List<PurchaseOrderDetailsResponse> PurchaseOrderDetails { get; set; }

    }
}
