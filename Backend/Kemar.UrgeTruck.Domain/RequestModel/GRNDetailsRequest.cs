using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class GRNDetailsRequest:CommonEntityModel
    {
        public long GRNDetailsId { get; set; }
        public long GRNId { get; set; }
        public string DropLoc { get; set; }
        public string RackNo { get; set; }
        public string SubRack { get; set; }
        public string ProductSerialKey { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

    }
}
