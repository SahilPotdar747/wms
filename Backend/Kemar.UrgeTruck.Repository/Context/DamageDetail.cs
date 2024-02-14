using Kemar.UrgeTruck.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Context
{
    public class DamageDetail
    {
        public long DamageDetailId { get; set; }
        public long GRNDetailsId { get; set; }
        public string description { get; set; }
        public virtual GRNDetails GRNDetails { get; set; }
    }
}
