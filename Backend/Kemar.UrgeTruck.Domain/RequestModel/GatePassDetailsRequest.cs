using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class GatePassDetailsRequest : CommonEntityModel
    {
        public int GatePassId { get; set; }
        public int GPDId { get; set; }
        public int POId { get; set; }
        public int ProductMasterId { get; set; }
        public string PartCode { get; set; }
        public int AcceptedQuantity { get; set; }
        public int RejectedQuantity { get; set; }
        public string PONumber { get; set; }


        public string Status { get; set; }
        public bool IsActive { get; set; }
       


        public string ProductSerialKey { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }


        public virtual ProductMasterRequest ProductMasterRequest { get; set; }
    }
}

