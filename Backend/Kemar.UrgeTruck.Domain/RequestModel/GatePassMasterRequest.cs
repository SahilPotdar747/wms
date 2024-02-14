using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class GatePassMasterRequest:CommonEntityModel
    {
        public int GatePassId { get; set; }
        public string GatePassNo { get; set; }
        //public int SupplierId { get; set; }
        public int POId { get; set; }
        public string DeliveryMode { get; set; }
        public bool RGPGenerated { get; set; }
        public string VehicleNo { get; set; }
        public DateTime? GatePassDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public virtual SupplierMasterRequest SupplierMasterRequest { get; set; }
    }
}
