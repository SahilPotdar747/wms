using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IOutward
    {
        public Task<List<DeliveryChallanMasterResponse>> GetOutwardRecord(int dcmId);
        Task<ResultModel> SaveOutwardRecord(List<DeliveryChallanDetailsRequest> request);
        public Task<List<GRNDetailsResponse>> GetDCGeneratedRecord(int id);


    }
}
