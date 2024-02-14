using Kemar.UrgeTruck.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class CommonMasterData:CommonEntityModel
    {
        [Key]
        public int Id { get; set; }
        public int DataItemId { get; set; }
        public string Type { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
