using System;
using System.Collections.Generic;
using System.Text;

namespace Kemar.UrgeTruck.Domain.ResponseModel
{
   public class SearchTransactionModel
    {
        public int VehicleTransactionId { get; set; }
        public string VehicleTransactionCode { get; set; }
        public string vrn { get; set; }
        public string TranType { get; set; }
        public DateTime? TransactionStartTime { get; set; }
        public DateTime? TransactionEndTime { get; set; }
        public string TranStatus { get; set; }
        public string DriverName { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public DateTime? ExitDateTime { get; set; }
        public string PreviousMilestone { get; set; }
        public string CurrentMilestone { get; set; }
        public int TotalRecord { get; set; }
    }


   public class PageResult<T>
    {
        public int Count { get; set; }
        public int FilterRecordCount { get; set; }
        public int TotalTransactionCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> items { get; set; }
    }
}
