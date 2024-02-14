using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories 
{
    public class ProductMasterRepository : IProductMaster
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public ProductMasterRepository(IKUrgeTruckContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<List<ProductMasterResponse>> GetAnyProductListAsync(string searchQuery)
        {
            
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            IQueryable<ProductMaster> queryable = kUrgeTruckContext.ProductMaster.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                queryable = queryable.Where(pm => pm.ProductName.Contains(searchQuery) ||
                                                   pm.PartCode.Contains(searchQuery) ||
                                                   pm.HSNCode.Contains(searchQuery));
            }

            var productMasters = await queryable.Select(pm => new ProductMasterResponse
            {
                ProductMasterId = pm.ProductMasterId,
                ProductName = pm.ProductName,
                PartCode = pm.PartCode,
                HSNCode = pm.HSNCode,
                ProductCategoryId = pm.ProductCategoryId,
                Description = pm.Description,
                Make =pm.Make,
                IsActive=pm.IsActive
            }).ToListAsync();

            return (productMasters);
        }
        public async Task<List<ProductMasterResponse>> getProductMaster()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var proList = await kUrgeTruckContext.ProductMaster.Include(x => x.ProductCategory).Include(x => x.PurchaseOrderDetails).ToListAsync();
                return _mapper.Map<List<ProductMasterResponse>>(proList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductMasterResponse>> GetProductPrice(int productMasterId)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var proList = await kUrgeTruckContext.ProductMaster
                        .Include(x => x.ProductCategory)
                        .Include(x => x.PurchaseOrderDetails)
                        .ToListAsync();
                     foreach (var product in proList)
                    {
                        //var matchingPurchaseOrder = product.PurchaseOrderDetails.FirstOrDefault();
                        var matchingPurchaseOrder = product.PurchaseOrderDetails.FirstOrDefault(x => x.ProductMasterId == productMasterId);
                   
                    if (matchingPurchaseOrder != null)
                        {
                        product.Price = matchingPurchaseOrder.ProductMaster.Price;
                        // matchingPurchaseOrder = new PurchaseOrderDetails
                        //{
                        matchingPurchaseOrder.Amount = product.Price;
                        kUrgeTruckContext.SaveChangesAsync();

                        //};
                        // product.PurchaseOrderDetails.Add(matchingPurchaseOrder);
                    }
                    }

                return _mapper.Map<List<ProductMasterResponse>>(proList);

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<ResultModel> AddORUpdateProductMasterAsync(ProductMasterRequest requestModel)
        {
            var resMessage = "Please ensure that the Part Code ";
            var addupdateMessage = "Product ";

            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var productList = await kUrgeTruckContext.ProductMaster.ToListAsync();
                if (requestModel.ProductMasterId == null || requestModel.ProductMasterId == 0)
                {
                    if (!productList.Any(x =>x.PartCode == requestModel.PartCode && x.Make == requestModel.Make))
                    {
                        //requestModel.CreatedDate = DateTime.Now;
                        kUrgeTruckContext.Add(_mapper.Map<ProductMaster>(requestModel));
                        addupdateMessage += "added successfully.";
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(addupdateMessage);
                    }
                    else
                    {
                        resMessage += "and Make values are different.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!productList.Any(x => x.ProductMasterId != requestModel.ProductMasterId && x.PartCode == requestModel.PartCode && x.Make == requestModel.Make))
                    {
                        var productMasterdata = await kUrgeTruckContext.ProductMaster.Where(x => x.ProductMasterId == requestModel.ProductMasterId).FirstOrDefaultAsync();
                        productMasterdata.ProductName = requestModel.ProductName;
                        productMasterdata.PartCode = requestModel.PartCode;
                        productMasterdata.Price = requestModel.Price;
                        productMasterdata.HSNCode = requestModel.HSNCode;
                        productMasterdata.ProductCategoryId = requestModel.ProductCategoryId;
                        productMasterdata.Make = requestModel.Make;
                        productMasterdata.Description = requestModel.Description;
                        productMasterdata.IsActive = requestModel.IsActive;
                        productMasterdata.ModifiedDate = DateTime.Now;
                        productMasterdata.ModifiedBy = requestModel.CreatedBy;
                        addupdateMessage += "updated successfully.";
                        kUrgeTruckContext.Update(productMasterdata);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(addupdateMessage);
                    }
                    else
                    {
                        resMessage += "and Make values are different.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductCategoryResponse>> GetProductCategoryAsync()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var categoryList = await kUrgeTruckContext.ProductCategory.ToListAsync();
                return _mapper.Map<List<ProductCategoryResponse>>(categoryList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<ResultModel> SaveProductCategory(ProductCategoryRequest request)
        {
            var resMessage = "Product Category ";
            try
            {
                using var kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var locList = await kUrgeTruckContext.ProductCategory.ToListAsync();
                if (request.ProductCategoryId == null || request.ProductCategoryId == 0)
                {
                    if (!locList.Any(x => x.ProductCategoryName == request.ProductCategoryName))
                    {
                        kUrgeTruckContext.Add(_mapper.Map<Entities.ProductCategory>(request));
                        resMessage += "added successfully.";
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.CreateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
                else
                {
                    if (!locList.Any(x => (x.ProductCategoryName == request.ProductCategoryName) && x.ProductCategoryId != request.ProductCategoryId))
                    {
                        var productcategory = await kUrgeTruckContext.ProductCategory.Where(x => x.ProductCategoryId == request.ProductCategoryId).FirstOrDefaultAsync();

                        productcategory.ProductCategoryName = request.ProductCategoryName;
                        resMessage += "updated successfully.";
                        kUrgeTruckContext.Update(productcategory);
                        await kUrgeTruckContext.SaveChangesAsync();
                        return ResultModelFactory.UpdateSucess(resMessage);
                    }
                    else
                    {
                        resMessage += "already exists.";
                        return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, resMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductMasterResponse> GetSelectedProductMaster(string productid)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var proList = await kUrgeTruckContext.ProductMaster.Include(x => x.ProductCategory).Where(x => x.ProductMasterId == Convert.ToInt32(productid)).FirstOrDefaultAsync();
                return _mapper.Map<ProductMasterResponse>(proList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


       



        //public async Task<ProductMasterResponse> GetProductPrice(int productMasterId)
        //{
        //    try
        //    {
        //        using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
        //        var proList = await kUrgeTruckContext.ProductMaster.Include(x => x.ProductCategory).Where(x=>x.ProductMasterId && x.Price).FirstOrDefaultAsync();
        //        return _mapper.Map<ProductMasterResponse>(proList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
