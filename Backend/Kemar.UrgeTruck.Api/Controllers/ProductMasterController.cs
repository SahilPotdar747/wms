using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMasterController : ControllerBase
    {

        private readonly IProductMaster _product;
        public ProductMasterController(IProductMaster product)
        {
            _product = product;
        }

        [HttpGet]
        [Route("GetAnyProductList")]
        public async Task<ActionResult<List<ProductMasterResponse>>> GetAnyProductListAsync(string searchQuery)
        {
            return await _product.GetAnyProductListAsync(searchQuery);
        }

        [HttpGet]
        [Route("GetAllProductMaster")]
        public async Task<List<ProductMasterResponse>> getProductMaster()
        {
            return await _product.getProductMaster();
        }

        [HttpGet]
        [Route("GetProductPrice")]
        public async Task<List<ProductMasterResponse>> GetProductPrice(int productMasterId)
        {
            return await _product.GetProductPrice(productMasterId);
        }

        //[HttpGet]
        //[Route("GetSelectedProductMaster")]
        //public async Task<ProductMasterResponse> GetSelectedProductMasterAsync([FromQuery] string productid)
        //{
        //    return await _product.GetSelectedProductMaster(productid);
        //}
        [HttpGet]
        [Route("GetSelectedProductMaster")]
        public async Task<ProductMasterResponse> GetSelectedProductMasterAsync([FromQuery] string productid)
        {
            return await _product.GetSelectedProductMaster(productid);
        }

        [HttpPost]
        [Route("SaveProductMaster")]
        public async Task<IActionResult> AddORUpdateProductMasterAsync(ProductMasterRequest requestModel)
        {
            CommonHelper.SetUserInformation(ref requestModel, Convert.ToInt32(requestModel.ProductMasterId), HttpContext);
            var result = await _product.AddORUpdateProductMasterAsync(requestModel);
            return ReturnResposneType(result);
        }

        [HttpGet]
        [Route("GetProductCategory")]
        public async Task<List<ProductCategoryResponse>> GetProductCategoryAsync()
        {
            return await _product.GetProductCategoryAsync();
        }

        [HttpPost]
        [Route("SaveProductCategory")]
        public async Task<IActionResult> SaveProductCategory(ProductCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data model");

            var resultModel = await _product.SaveProductCategory(request);

            return ReturnResposneType(resultModel);
        }



        

        private IActionResult ReturnResposneType(ResultModel result)
        {
            if (result.StatusCode == ResultCode.SuccessfullyCreated)
                return Created("", result);
            else if (result.StatusCode == ResultCode.SuccessfullyUpdated)
                return Ok(result);
            else if (result.StatusCode == ResultCode.Unauthorized)
                return Unauthorized(result);
            else if (result.StatusCode == ResultCode.DuplicateRecord)
                return Conflict(result);
            else if (result.StatusCode == ResultCode.RecordNotFound)
                return NotFound(result);
            else if (result.StatusCode == ResultCode.NotAllowed)
                return NotFound(result);

            return null;
        }


    }
}
