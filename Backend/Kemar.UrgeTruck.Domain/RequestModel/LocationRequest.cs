using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class LocationRequest:CommonEntityModel
    {
        public long LocationId { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string ParentLocationCode { get; set; }
        public string LocationType { get; set; }
        public string DIsplayName { get; set; }
        public bool IsActive { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public int LocationCategoryId { get; set; }
    }
}
