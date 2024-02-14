using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class DeliveryChallanMasterRequest : CommonEntityModel
    {
        public int DCMId { get; set; }
        public long GRNId { get; set; }
        public string DcStatus { get; set; }   
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }  //instock,deliverd
        public bool IsActive { get; set; }

        public string ProductSerialKey { get; set; }
    }
}
