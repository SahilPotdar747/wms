using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class ProductMasterResponse
    {
        public ProductMasterResponse()
        {
            //GatePassDetailResponse = new List<GatePassDetailResponse>();
           // PurchaseOrderDetailsResponse = new List<PurchaseOrderDetailsResponse>();  
        }

        [JsonIgnore]
        public long ProductMasterId { get; set; }
        //public int POId { get; set; }   //new added
        public string ProductName { get; set; }
        public string PartCode { get; set; }
        public string HSNCode { get; set; }
        public string Make { get; set; }
        public string Amount { get; set; }

        public string Price { get; set; }
        public int ProductCategoryId { get; set; }
       // public int PODId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ProductCategoryResponse ProductCategory { get; set; }
      
       public virtual List<PurchaseOrderDetailsResponse> PurchaseOrderDetails { get; set; }
        //public PurchaseOrderDetailsResponse PurchaseOrderDetails { get; set; }
        public ICollection<GatePassDetailResponse> GatePassDetailResponse { get; set; }
    }
}
