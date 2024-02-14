using Kemar.UrgeTruck.Domain.ResponseModel;

namespace Kemar.UrgeTruck.Domain.DTOs
{
    public class NotificationRequestDto
    {
        public int VehicleTransactionId { get; set; }
        public int JobMilestoneId { get; set; }
        public int MilestoneActionsTrackingId { get; set; }
        public string LocationCode { get; set; }
        public string ActionCode { get; set; }
        public string MilestoneEvent { get; set; }
        public string MessageCode { get; set; }
        public int AnyId { get; set; }
        public bool IsAnyReOpenAction { get; set; }
        public string TransactionType { get; set; }
        public string Direction { get; set; }
        public string ActualDeviceLocation { get; set; }
        public string Lane { get; set; }
    
    }
}
