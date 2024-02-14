using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class BlackListHistoryResponse: CommonEntityModel
    {
        public int BlackListHistoryId { get; set; }
        public int Id { get; set; }
        
        
        public string Name { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime? IncidentDate { get; set; }
        public string DlNo { get; set; }
        public string MobileNo { get; set; }
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public string VRN { get; set; }
        public int TransporterId { get; set; }
        public string PlantCode { get; set; }




    }
}
