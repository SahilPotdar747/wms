using Kemar.UrgeTruck.Domain.Common;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class LaneOCRResponse : CommonEntityModel
    {
        public int LaneOcrId { get; set; }
        public int LaneId { get; set; }
        public string LaneName { get; set; }
        public string LicenseNo { get; set; }
        public bool IsActive { get; set; }
    }
}
