using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IDeliveryChallan
    {
        public Task<List<DeliveryChallanMasterResponse>> GetDeliveryChallanRecord(int deliveryChallanId);
        public Task<List<GRNDetailsResponse>> GetInStockGRNDetails(int id);
        Task<ResultModel> SaveDeliveryChallanDetails(List<DeliveryChallanDetailsRequest> requestModels);
    }
}
