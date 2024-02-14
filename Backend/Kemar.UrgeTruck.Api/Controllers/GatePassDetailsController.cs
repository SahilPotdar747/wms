using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatePassDetailsController : ControllerBase
    {
        private readonly IGatePassDetails _gatePass;
        public GatePassDetailsController(IGatePassDetails gatePass)
        {
            _gatePass = gatePass;
        }

        [HttpGet]
        [Route("GetGatePassDetailData")]
        public async Task<List<GatePassDetailResponse>> GetGatePassDetailData()
        {
            return await _gatePass.GetGatePassDetailData();
        }

        [HttpPost]
        [Route("SaveGatePassDetails")]
        public async Task<IActionResult> SaveGatePassDetails(List<GatePassDetailsRequest> detailsRequest)
        {
            ResultModel result = new ResultModel();
            foreach (var request in detailsRequest)
            {
                var req = request;
                CommonHelper.SetUserInformation(ref req, request.GPDId, HttpContext);
            }
            result = await _gatePass.SaveGatePassDetails(detailsRequest);
            return ReturnResposneType(result);

        }
        [HttpGet]
        [Route("GetSumOfProductQuantity")]
        public async Task<GatePassDetailResponse> GetSumOfProductQuantity(string productid, int poid)
        {
            return await _gatePass.GetSumOfProductQuantity(productid, poid);
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
