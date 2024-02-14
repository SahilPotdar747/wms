using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class LocationCategory
    {
        public LocationCategory()
        {
            Locations = new List<Location>();
            Rates = new List<Rate>();
        }
        public int LocationCategoryId { get; set; }
        public string LocationCategoryName { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
