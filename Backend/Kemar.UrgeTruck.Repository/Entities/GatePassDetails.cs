using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Kemar.UrgeTruck.Repository.Entities
{
    public class GatePassDetails : CommonEntityModel
    {
        public GatePassDetails()
        {
            RGPMaster = new List<RGPMaster>();
        }

        
        public int GPDId { get; set; }
        public int GatePassId { get; set; }

        public string PartCode { get; set; }
        public int AcceptedQuantity { get; set; }
        public int RejectedQuantity { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public virtual GatePassMaster GatePassMaster { get; set; }
        public virtual ICollection<RGPMaster> RGPMaster { get; set; }

    }
}