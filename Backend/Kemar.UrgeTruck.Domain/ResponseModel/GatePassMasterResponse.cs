using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
   public class GatePassMasterResponse : CommonEntityModel
    {
       
        public int GatePassId { get; set; }
        public string GatePassNo { get; set; }
        //public int SupplierId { get; set; }
        public int POId { get; set; }
        public string PONumber { get; set; }
        public string DeliveryMode { get; set; }
        public bool RGPGenerated { get; set; }
        public string VehicleNo { get; set; }
        public DateTime GatePassDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public SupplierMasterResponse SupplierMaster { get; set; }
        public virtual PurchaseOrderResponse PurchaseOrderMaster { get; set; }
       //public virtual PurchaseOrderDetailsResponse PurchaseOrderDetails { get; set; }//new


    }
}
