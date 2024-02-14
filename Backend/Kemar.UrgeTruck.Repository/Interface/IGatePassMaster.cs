using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IGatePassMaster
    {
        public Task<List<GatePassMasterResponse>> GetGatePassMasterData(int id);
        Task<ResultModel> SaveGatePassRecord(GatePassMasterRequest Request);



    }
}
