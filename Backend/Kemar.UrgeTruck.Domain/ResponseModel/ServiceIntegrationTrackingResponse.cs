using System;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
    public class ServiceIntegrationTrackingResponse
    {
        public int IntegrationTrackingId { get; set; }
        public string RequestId { get; set; }
        public string RequestJson { get; set; }
        public DateTime? InitiatedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string TransactionStatus { get; set; }
        public bool IsActive { get; set; }
        public string ResponseMessage { get; set; }
    }
}
