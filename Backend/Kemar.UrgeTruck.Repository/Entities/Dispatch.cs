using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class Dispatch
    {
        public int DispatchId { get; set; }
        public string RefNum { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
    }
}
