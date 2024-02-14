using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IRGPMaster
    {
        public Task<List<RGPMasterResponse>> GetRGPRecord(int id);
       Task<ResultModel> SaveRGPRecords(List<RGPMasterRequest> request);
        //public Task<List<RGPMasterResponse>> GenerateRGP(int id,long productmasterid);
    }
}
