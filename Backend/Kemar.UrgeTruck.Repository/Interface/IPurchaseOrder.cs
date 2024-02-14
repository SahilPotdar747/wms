using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IPurchaseOrder
    {
        public Task<List<PurchaseOrderResponse>> GetAllPurchaseOrderRecord(int poId);
        public Task<List<PurchaseOrderResponse>> GetDraftRecord(int poId);
        Task<ResultModel> SavePurchaseOrderRecords(PurchaseOrderRequest request);
    }
}
