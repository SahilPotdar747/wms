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
    public class RGPMasterController : ControllerBase
    {
        private readonly IRGPMaster _rgpMaster;
        public RGPMasterController(IRGPMaster rgpMaster)
        {
            _rgpMaster = rgpMaster;
        }

        [HttpGet]
        [Route("GetRGPRecord")]
        public async Task<List<RGPMasterResponse>> GetRGPRecord(int id)
        {
            return await _rgpMaster.GetRGPRecord(id);
        }

        [HttpPost]
        [Route("SaveRGPRecords")]
        public async Task<IActionResult> SaveRGPRecords(List<RGPMasterRequest> request)
        {
            ResultModel result = new ResultModel();
            foreach (var requests in request)
            {
                var req = requests;
                CommonHelper.SetUserInformation(ref req, requests.RGPId, HttpContext);
            }
            result = await _rgpMaster.SaveRGPRecords(request);
            return ReturnResposneType(result);

        }

        //[HttpGet]
        //[Route("GenerateRGP")]
        //public async Task<List<RGPMasterResponse>> GenerateRGP(int id,long productmasterid)
        //{
        //    return await _rgpMaster.GenerateRGP(id, productmasterid);
        //}



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
