using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatePassMasterController : ControllerBase
    {
        private readonly IGatePassMaster _gatePass;
        public GatePassMasterController(IGatePassMaster gatePass)
        {
            _gatePass = gatePass;
        }

        [HttpGet]
        [Route("GetGatePassMasterData")]
        public async Task<List<GatePassMasterResponse>> GetGatePassMasterData(int id)
        {
            return await _gatePass.GetGatePassMasterData(id);
        }

        [HttpPost]
        [Route("SaveGatePassRecord")]
        public async Task<IActionResult> SaveGatePassRecord(GatePassMasterRequest request)
        {
            CommonHelper.SetUserInformation(ref request, request.GatePassId, HttpContext);
            var result = await _gatePass.SaveGatePassRecord(request);
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
