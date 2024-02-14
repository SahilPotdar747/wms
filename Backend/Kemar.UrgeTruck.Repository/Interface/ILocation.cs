using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface ILocation
    {
        Task<List<LocationListResponse>> GetLocationAsync(string locCode);
        Task<List<LocationListResponse>> GetLocationsdropdownAsync();
        Task<ResultModel> AddOrUpdateLocationsAsync(LocationRequest request);
        Task<List<LocationCategoryResponse>> GetLocationCategoryAsync();
        Task<ResultModel> SaveLocationCategory(LocationCategoryRequest request);


        public Task<List<LocationListResponse>> GetGRNDropDownAsync(int userId);
        public Task<List<LocationListResponse>> GerRackSubrackDropDownAsync(string locCode);
    }
}