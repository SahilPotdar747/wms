using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class Product
    {
        public Product()
        {
            Rate = new List<Rate>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public int UomId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Uom Uom { get; set; }
        public virtual ICollection<Rate> Rate { get; set; }
        //public virtual ICollection<GRNDetails> GRNDetails { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }

    }
}
