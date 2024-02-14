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
    public class CommonMasterDataController : ControllerBase
    {
        private readonly ICommonMasterData _commonMasterData;

        public CommonMasterDataController(ICommonMasterData commonMasterData)
        {
            _commonMasterData = commonMasterData;

        }

        [HttpGet]
        [Route("GetAllCommonMasterData")]
        public async Task<List<CommonMasterDataResponse>> GetAllCommonMasterDataAsync()
        {
            var x = await _commonMasterData.GetAllCommonMasterData();
            return x;
        }

        [HttpGet]
        [Route("GetTypeList")]
        public async Task<List<string>> GetTypeListAsync()
        {
            var x = await _commonMasterData.GetTypeListAsync();
            return x;
        }

        [HttpGet]
        [Route("GetParameterList")]
        public async Task<List<string>> GetParameterListAsync()
        {
            var x = await _commonMasterData.GetParameterListAsync();
            return x;
        }

        
        [HttpPost]
        [Route("AddCommonMasterData")]
        public async Task<IActionResult> AddCommonMasterDataAsync([FromBody] CommonMasterDataRequest commonMasterDataRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data model");

            CommonHelper.SetUserInformation(ref commonMasterDataRequest, commonMasterDataRequest.Id, HttpContext);
            var resultModel = await _commonMasterData.AddCommonMasterDataAsync(commonMasterDataRequest);

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
