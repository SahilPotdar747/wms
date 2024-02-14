using Kemar.UrgeTruck.Domain.Common;
using System.Collections.Generic;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class PurchaseOrderDetails : CommonEntityModel
    {
        
        public int PODId { get; set; }
        public int POId { get; set; }
        public long ProductMasterId { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

       public virtual PurchaseOrderMaster PurchaseOrderMaster { get; set; }
       public virtual ProductMaster ProductMaster { get; set; }

        

    }
}
