using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class LocationListResponse
    {
        [JsonIgnore]
        public long LocationId { get; set; }
        public string Name { get; set; }
        public string LocationCategoryId { get; set; }
        public string LocationCode { get; set; }
        public string ParentLocationCode { get; set; }
         public string LocationType { get; set; }
        public string DIsplayName { get; set; }
        public bool IsActive { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        public LocationCategoryResponse LocationCategory { get; set; }

    }
}
