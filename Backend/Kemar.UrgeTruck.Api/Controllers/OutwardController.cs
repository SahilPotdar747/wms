using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutwardController:BaseController
    {
        private readonly IOutward _outward;

        public OutwardController(IOutward outward)
        {
            _outward = outward;

        }

        [HttpGet]
        [Route("GetOutwardRecord")]
        public async Task<List<DeliveryChallanMasterResponse>> GetOutwardRecord(int dcmId)
        {
            return await _outward.GetOutwardRecord(dcmId);
        }

        [HttpGet]
        [Route("GetDCGeneratedRecord")]
        public async Task<List<GRNDetailsResponse>> GetDCGeneratedRecord([FromQuery] int dcmId)
        {
            return await _outward.GetDCGeneratedRecord(dcmId);
        }



         [HttpPost]
        [Route("SaveOutwardRecord")]
        public async Task<IActionResult> SaveOutwardRecord(List<DeliveryChallanDetailsRequest> request)
        {
            foreach( var req in request)
            {
                var newReq = req;
                CommonHelper.SetUserInformation(ref newReq, req.DCDId, HttpContext);
            }
            var result = await _outward.SaveOutwardRecord(request);
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
            else if (result.StatusCode == ResultCode.Invalid)
                return NotFound(result);

            return null;
        }

    }
}
