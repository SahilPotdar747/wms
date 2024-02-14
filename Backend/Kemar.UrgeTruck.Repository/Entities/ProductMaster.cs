using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class ProductMaster:CommonEntityModel
    {
        public ProductMaster() 
        {
            GRN = new List<GRN>();
            PurchaseOrderDetails = new List<PurchaseOrderDetails>();
         
        }
        public long ProductMasterId { get; set; }
        public string ProductName { get; set;}
        public string PartCode { get; set;}
        public string HSNCode { get; set;}
        public string Make { get; set;}
        public string? Price { get; set; }
        public int ProductCategoryId { get; set;}
        public string Description { get; set;}
        public bool IsActive { get; set;}
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<GRN> GRN { get; set; }
        public virtual ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; } 
        
}
}
