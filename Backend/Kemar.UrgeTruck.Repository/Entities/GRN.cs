 using Kemar.UrgeTruck.Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class GRN : CommonEntityModel
    {
        public GRN()
        {
            GRNDetails = new List<GRNDetails>();
            DeliveryChallanMaster = new List<DeliveryChallanMaster>();
        }
       
        public long GRNId { get; set; }
        public string GRNNumber { get; set; }
        public long ProductMasterId { get; set; }
        public int SupplierId { get; set; }
        public long LocationId { get; set; }
        public string PONumber { get; set; }
        public string POProductQuantity { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceProductQuantity { get; set; }
        public string POFile { get; set; }
        public string InvoiceFile { get; set; }
        public string Remark { get; set; }
        public DateTime EntryDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
       
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual SupplierMaster SupplierMaster { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<GRNDetails> GRNDetails { get; set; }
        public virtual ICollection<DeliveryChallanMaster> DeliveryChallanMaster { get; set; }

    }
}
