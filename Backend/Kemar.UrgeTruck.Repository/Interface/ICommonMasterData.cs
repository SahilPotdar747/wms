using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface ICommonMasterData
    {
        Task<List<CommonMasterDataResponse>> GetAllCommonMasterData();
        Task<List<string>> GetTypeListAsync();
        Task<List<string>> GetParameterListAsync();
        Task<List<CommonMasterDataResponse>> GetCommonMasterDataOnKeyAndParameter(string key, string Parameter);
        Task<ResultModel> AddCommonMasterDataAsync(CommonMasterDataRequest commonMasterDataRequest);
    }
}
