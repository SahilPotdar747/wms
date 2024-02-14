using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            ProductMaster = new List<ProductMaster>();
            Product = new List<Product>();
        }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string?  Price{get;set;}
        public virtual ICollection<ProductMaster> ProductMaster { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
