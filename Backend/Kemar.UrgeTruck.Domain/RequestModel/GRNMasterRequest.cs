using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class GRNMasterRequest : CommonEntityModel
    {
        public GRNMasterRequest()
        {
            GRNDetails = new List<GRNDetailsRequest>();
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
        public virtual ICollection<GRNDetailsRequest> GRNDetails { get; set; }
    }
}
