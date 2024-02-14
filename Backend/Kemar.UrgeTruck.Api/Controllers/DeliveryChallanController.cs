using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
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
    public class DeliveryChallanController : ControllerBase
    {
        private readonly IDeliveryChallan _deliverychallan;

        public DeliveryChallanController(IDeliveryChallan deliverychallan)
        {
            _deliverychallan = deliverychallan;

        }

        [HttpGet]
        [Route("GetDeliveryChallanRecord")]
        public async Task<List<DeliveryChallanMasterResponse>> GetDeliveryChallanRecord([FromQuery] int deliveryChallanId)
        {
            return await _deliverychallan.GetDeliveryChallanRecord(deliveryChallanId);
        }

        [HttpGet]
        [Route("GetInStockGRNDetails")]
        public async Task<List<GRNDetailsResponse>> GetInStockGRNDetails([FromQuery] int id)
        {
            return await _deliverychallan.GetInStockGRNDetails(id);
        }
        [HttpPost]
        [Route("SaveDeliveryChallan")]
        public async Task<IActionResult> SaveDeliveryChallanDetails(List<DeliveryChallanDetailsRequest> requestModels)
        {
            foreach (var request in requestModels)
            {
                var req = request;
                CommonHelper.SetUserInformation(ref req, request.DCDId, HttpContext);
            }

            var result = await _deliverychallan.SaveDeliveryChallanDetails(requestModels);
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
