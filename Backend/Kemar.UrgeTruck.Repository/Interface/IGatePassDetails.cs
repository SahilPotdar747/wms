using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
     public interface IGatePassDetails
    {
         Task<List<GatePassDetailResponse>> GetGatePassDetailData();
        //Task<ResultModel> SaveGatePassDetails(List<GatePassDetailsRequest> detailsRequest);
       // Task<GatePassDetailResponse> GetSumOfProductQuantity(string productid, int poid);
         ///Task<List<GatePassDetails>> GetGatePassDetailData(int id);
        Task<ResultModel> SaveGatePassDetails(List<GatePassDetailsRequest> detailsRequest);
        
        Task<GatePassDetailResponse> GetSumOfProductQuantity(string productid, int poid);
    }
}
