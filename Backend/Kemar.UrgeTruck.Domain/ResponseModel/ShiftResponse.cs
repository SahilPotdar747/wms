using System;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class ShiftResponse
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
