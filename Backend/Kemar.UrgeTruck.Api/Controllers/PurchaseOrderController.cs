using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrder _purchase;
        public PurchaseOrderController(IPurchaseOrder purchase)
        {
            _purchase = purchase;
        }

        [HttpGet]
        [Route("GetAllPurchaseOrderRecord")]
        public async Task<List<PurchaseOrderResponse>> GetAllPurchaseOrderRecord(int poId)
        {
            return await _purchase.GetAllPurchaseOrderRecord(poId);
        }

        [HttpGet]
        [Route("GetDraftRecord")]
        public async Task<List<PurchaseOrderResponse>> GetDraftRecord([FromQuery] int poId)
        {
            return await _purchase.GetDraftRecord(poId);
        }

        [HttpPost]
        [Route("SavePurchaseOrderRecords")]
        public async Task<IActionResult> SavePurchaseOrderRecords(PurchaseOrderRequest request)
        {
            CommonHelper.SetUserInformation(ref request, request.POId, HttpContext);
            var result = await _purchase.SavePurchaseOrderRecords(request);
            return ReturnResposneType(result);
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
