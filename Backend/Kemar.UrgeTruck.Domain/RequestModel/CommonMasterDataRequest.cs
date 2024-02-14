using Kemar.UrgeTruck.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class CommonMasterDataRequest:CommonEntityModel
    {
        public int Id { get; set; }
        public int DataItemId { get; set; }
        public string Type { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
