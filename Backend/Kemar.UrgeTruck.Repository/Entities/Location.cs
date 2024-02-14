using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class Location : CommonEntityModel
    {
        public Location() 
        {
            UserLocationAccess = new List<UserLocationAccess>();
            Stock = new List<Stock>();
            GRN = new List<GRN>();
        }
        public long LocationId { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string ParentLocationCode { get; set; }
        public string LocationType{get; set; }
        public string DIsplayName { get; set; }
        public bool IsActive { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        public int LocationCategoryId { get; set; }
        public virtual LocationCategory LocationCategory { get; set; }
        //public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<GRN> GRN { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
        public virtual ICollection<UserLocationAccess> UserLocationAccess { get; set; }

    }
}
