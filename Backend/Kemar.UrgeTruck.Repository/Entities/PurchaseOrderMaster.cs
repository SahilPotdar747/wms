using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class PurchaseOrderMaster : CommonEntityModel
    {
        public PurchaseOrderMaster()
        {
            PurchaseOrderDetails = new List<PurchaseOrderDetails>();
            GatePassMaster = new List<GatePassMaster>();
            //RGPMaster = new List<RGPMaster>();
        }
        public int POId { get; set; }
        public int SupplierId { get; set; }
        public string PONumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public virtual SupplierMaster SupplierMaster { get; set; }
        public virtual ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public virtual ICollection<GatePassMaster> GatePassMaster { get; set; }
        //public virtual ICollection<RGPMaster> RGPMaster { get; set; }

    }
}
