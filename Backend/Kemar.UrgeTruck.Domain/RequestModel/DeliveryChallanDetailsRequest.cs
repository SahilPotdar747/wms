using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class DeliveryChallanDetailsRequest:CommonEntityModel
    {
        public int DCDId { get; set; }
        public int DCMId { get; set; }
        public long GRNDetailsId { get; set; }
        public string ChallanNumber { get; set; }
        public string Status { get; set; }  //instock,deliverd,Dc Generated
        public bool IsActive { get; set; }
        public string ProductSerialKey { get; set; }
        public virtual DeliveryChallanMasterRequest DeliveryChallanMaster { get; set; }
        
    }
}
