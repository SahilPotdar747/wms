using System;
using System.Collections.Generic;
using System.Text;

namespace Kemar.UrgeTruck.Domain.RequestModel
{
    public class BreathAnalysysRequest
    {
        public string BreathDeviceIp { get; set; }
        public bool result { get; set; }
    }
}
