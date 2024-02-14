using System;
using System.Collections.Generic;

namespace Kemar.UrgeTruck.Domain.DTOs
{
    public class InPlantTransactionDto
    {
        public InPlantTransactionDto()
        {
            InPlantTransactionTracking = new List<InPlantTransactionTrackingDto>();
            InPlantWBRules = new List<InPlantWBRulesDto>();
        }

        public int InPlantTransactionId { get; set; }
        public int JobId { get; set; }
        public string VRN { get; set; }
        public int NoOfTrips { get; set; }
        public string TareWeightFrequncy { get; set; } // //"OnceInShift" , "Always" , "OnceIn24Hours" 
        public bool IsJobCompleted { get; set; } // This will be notified by AX4
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime? TransactionStarted { get; set; }
        public DateTime? TransactionCompleted { get; set; }
        public int NoOfTripsCompleted { get; set; }

        public virtual ICollection<InPlantTransactionTrackingDto> InPlantTransactionTracking { get; set; }
        public virtual ICollection<InPlantWBRulesDto> InPlantWBRules { get; set; }
    }

    public class InPlantTransactionTrackingDto
    {
        public int InPlantTrackingId { get; set; }
        public int InPlantTransactionId { get; set; }
        public string VrnDetectionLocCode { get; set; }
        public DateTime? InDateTime { get; set; }
        public DateTime? OutDateTime { get; set; }

        public virtual InPlantTransactionDto InPlantTransaction { get; set; }
    }

    public class InPlantWBRulesDto
    {
        public int InPlantWBRulesId { get; set; }
        public int InPlantTransactionId { get; set; }
        public string WeighmentType { get; set; }
        public DateTime? WeighmentExecution { get; set; }
        public bool IsWeighmentExecuted { get; set; }

        public virtual InPlantTransactionDto InPlantTransaction { get; set; }
    }

}
