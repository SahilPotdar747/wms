using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IStorage
    {
        public Task<List<GRNDetailsResponse>>GetStorageDetails(int id);
        Task<ResultModel> UpdateStorage(GRNDetailsRequest requestModel);
    }
}
