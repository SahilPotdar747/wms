using Kemar.UrgeTruck.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class LaneOCRMappingRequest : CommonEntityModel
    {
        public int LaneOcrId { get; set; }

        [Required]
        public int LaneId { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string LicenseNo { get; set; }
        public bool IsActive { get; set; }
    }
}
