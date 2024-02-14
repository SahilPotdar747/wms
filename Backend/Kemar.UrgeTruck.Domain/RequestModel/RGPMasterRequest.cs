using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class RGPMasterRequest: CommonEntityModel
    {
        public int RGPId { get; set; }
        public int GatePassId { get; set; }
        //public int GPDId { get; set; }
        public int POId { get; set; }
        public int TotalNoOfRGPItems { get; set; }
        public string ProductSerialKey { get; set; }
        public DateTime RGPDate { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }

        public virtual GatePassDetailsRequest GatePassDetails { get; set; }
       

    }
}
