using System;

namespace Kemar.UrgeTruck.Repository.Entities
{
    public class RetryObjectContainer
    {
        public int RetryObjectContainerId { get; set; }
        public string NotifierName { get; set; }
        public string ReceiverName { get; set; }
        public int VehicleTransactionId { get; set; }
        public int JobMilestoneId { get; set; }
        public int MilestoneActionsTrackingId { get; set; }
        public DateTime TriggeredDateTime { get; set; }
        public int NoOfRetry { get; set; } // To be updated in each retry
        public int MaxRetry { get; set; } // This value to be set when record is entered in this table from app config key
        public bool IsProcessed { get; set; }
        public bool IsActive { get; set; }
        public string RetryFailureReason { get; set; }
    }
}
