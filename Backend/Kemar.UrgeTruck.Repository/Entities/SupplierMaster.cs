using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class SupplierMaster : CommonEntityModel
    {
        public SupplierMaster() 
        {
            GRN = new List<GRN>();
            PurchaseOrderMaster = new List<PurchaseOrderMaster>();
            GatePassMaster = new List<GatePassMaster>();
        }
        public int SupplierId { get; set; }
         public string SupplierName { get; set; }
        public  string ContanctNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        

        public virtual ICollection<GRN> GRN { get; set; }
        public virtual ICollection<PurchaseOrderMaster> PurchaseOrderMaster { get; set; }
        public virtual ICollection<GatePassMaster> GatePassMaster { get; set; }
    }

}
