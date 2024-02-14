using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class GRNMasterResponse
    {

        public GRNMasterResponse()
        {
           // GRNDetailsResponse = new List<GRNDetailsResponse>();
            DeliveryChallanMasterResponse = new List<DeliveryChallanMasterResponse>();
        }
        
        [JsonIgnore]
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
        public ProductMasterResponse ProductMaster { get; set; }
        public SupplierMasterResponse SupplierMaster { get; set; }
        public LocationListResponse Location { get; set; }
        public GRNDetailsResponse GRNDetailsResponse { get; set; }
        public virtual List<DeliveryChallanMasterResponse> DeliveryChallanMasterResponse { get; set; }

    }
}
