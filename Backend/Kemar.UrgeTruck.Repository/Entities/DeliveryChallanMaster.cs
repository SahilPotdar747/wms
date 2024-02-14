using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class DeliveryChallanMaster : CommonEntityModel
    {
        public int DCMId { get; set; }
        public long GRNId { get; set; }
        public string DcStatus { get; set; } 
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }  //instock,deliverd
        public bool IsActive { get; set; }

       public virtual GRN GRN { get; set; }
        public virtual ICollection<DeliveryChallanDetails> DeliveryChallanDetails { get; set; }
    }
}
