using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IGRNDetails
    {
        public Task<List<GRNDetailsResponse>> GetGRNDetailsAsync(long id);
        public Task<List<GRNDetailsResponse>> GetAllGrnDetails();

        Task<ResultModel> addGRNDetails(List<GRNDetailsRequest> requestModel);
    }
}
