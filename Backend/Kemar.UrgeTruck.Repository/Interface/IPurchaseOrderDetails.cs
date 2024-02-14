using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IPurchaseOrderDetails
    {
        public Task<List<PurchaseOrderDetailsResponse>> GetPurchaseOrderDetails(int id);

        Task<ResultModel> SavePODetails(List<PurchaseOrdeDetailsrRequest> request);
        
    }
}
