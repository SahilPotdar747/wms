using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.FRDB.FRDBModel
{
    public class Mx_ATDEventTrn
    {
        //public int Type { get; set; }
        public decimal MID { get; set; }
        public decimal EventID { get; set; }
        [Key]
        public decimal IndexNo { get; set; }
        public string UserID { get; set; }

    }
}
