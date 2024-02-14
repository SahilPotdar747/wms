using System;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class FailureLog
    {
        public int FailureLogId { get; set; }
        public int LocationId { get; set; }//device location id/WB Id/location id/
        public string Category { get; set; }//device/WB/location
        public DateTime? FailureTime { get; set; }
        public DateTime? RepairTime { get; set; }
        public bool IsActive { get; set; }
    }
}


