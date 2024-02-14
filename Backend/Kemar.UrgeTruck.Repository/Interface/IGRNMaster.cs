using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IGRNMaster
    {
        Task<List<GRNMasterResponse>> GetGRNMaster(long grnId);
        Task<ResultModel> AddORUpdateGRNMasterAsync(GRNMasterRequest requestModel);
       // Task<ResultModel> AddOrUpdateGRNAsync(GRNMasterRequest requestModel, IEnumerable<GRNDetailsRequest> detailsRequestModels);
    }
}
