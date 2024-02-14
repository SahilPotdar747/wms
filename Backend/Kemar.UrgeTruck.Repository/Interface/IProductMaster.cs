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
    public interface IProductMaster
    {
        Task<List<ProductMasterResponse>> getProductMaster();
      Task<ProductMasterResponse> GetSelectedProductMaster(string productid);
        Task<ResultModel> AddORUpdateProductMasterAsync(ProductMasterRequest requestModel);
        Task<List<ProductMasterResponse>> GetAnyProductListAsync(string searchQuery);
        Task<List<ProductCategoryResponse>> GetProductCategoryAsync();
        Task<ResultModel> SaveProductCategory(ProductCategoryRequest request);
        Task<List<ProductMasterResponse>> GetProductPrice(int productMasterId);
        
    }
}
