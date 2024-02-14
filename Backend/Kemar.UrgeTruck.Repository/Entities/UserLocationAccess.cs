using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class UserLocationAccess
    {
        public int UserLocationAccessId { get; set; }
        public long LocationId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public virtual Location Location { get; set; }
        public virtual UserManager UserManager { get; set; }
    }
}
