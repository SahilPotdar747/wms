using Kemar.UrgeTruck.Repository.Interface;
using Kemar.UrgeTruck.Repository.Repositories.Interface;
using Kemar.UrgeTruck.ServiceIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kemar.UrgeTruck.Api.Core.Helper;
using System;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.Common;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierMasterController:BaseController
    {
        private readonly ISupplierMaster _supplier;
        public SupplierMasterController(ISupplierMaster supplier)
        {
            _supplier = supplier;
        }

        [HttpGet]
        [Route("GetSuppliers")]
        public async Task<List<SupplierMasterResponse>> GetSuppliersAsync()
        {
            return await _supplier.GetSuppliersAsync();
        }

        [HttpPost]
        [Route("SaveSuppliersDetails")]
        public async Task<IActionResult> SaveSuppliersDetailsAsync(SupplierMasterRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Invalid data model");
            CommonHelper.SetUserInformation(ref request, request.SupplierId, HttpContext);
            var resultModel = await _supplier.SaveSuppliersDetailsAsync(request);
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
