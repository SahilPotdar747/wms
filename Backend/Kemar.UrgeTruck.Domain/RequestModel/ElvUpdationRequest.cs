using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
  public  class ElvUpdationRequest
    {
        public int ELVId { get; set; } // Inbound/outbound
        public string TransactionType { get; set; }
        public int LocationId { get; set; }
        public bool WithoutElv  { get; set; }
    }
}
