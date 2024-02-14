using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class DeliveryChallanDetails : CommonEntityModel
    {
        public int DCDId { get; set; }
        public int DCMId { get; set; }
        public long GRNDetailsId { get; set; }
        public string ChallanNumber { get; set; }
        public string Status { get; set; }  //instock,deliverd,Dc Generated
        public bool IsActive { get; set; }
        public virtual DeliveryChallanMaster DeliveryChallanMaster { get; set; }
        public virtual GRNDetails GRNDetails { get; set; }



    }
}
