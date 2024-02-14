using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class GatePassDetailResponse : CommonEntityModel
    {
        public int GatePassId { get; set; }

        public int GPDId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public int SumQuantity { get; set; }

        public int AcceptedQuantity { get; set; }
        public int RejectedQuantity { get; set; }
        public long ProductMasterId { get; set; }
        public int POId { get; set; }
        public string PartCode { get; set; }
        public GatePassMasterResponse GatePassMasterResponse { get; set; }
        public PurchaseOrderDetailsResponse purchaseOrderDetailsResponse { get; set; }
        public PurchaseOrderResponse purchaseOrderResponse { get; set; }







    }
}


