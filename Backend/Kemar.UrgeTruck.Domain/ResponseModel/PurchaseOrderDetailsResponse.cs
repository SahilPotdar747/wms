using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class PurchaseOrderDetailsResponse:CommonEntityModel
    {

        public PurchaseOrderDetailsResponse()
        {
            
            ProductMasterResponse = new List<ProductMasterResponse>();
            PurchaseOrderResponse = new List<PurchaseOrderResponse>();
            GatePassDetailResponse = new List<GatePassDetailResponse>();
        }
        public int PODId { get; set; }
        public int POId { get; set; }
        public long ProductMasterId { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string productName { get; set; }
        public string Price { get; set; }
        public ProductMasterResponse productMaster { get; set; }
        
        public virtual List<ProductMasterResponse> ProductMasterResponse { get; set; }
        public virtual List<PurchaseOrderResponse> PurchaseOrderResponse { get; set; }
        public virtual ICollection<GatePassDetailResponse> GatePassDetailResponse { get; set; }
        
    }
}
