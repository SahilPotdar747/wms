﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class SupplierMasterResponse
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContanctNo { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public string Remark { get; set; }
    }
}
