using Kemar.UrgeTruck.Api.Core.Helper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;
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
    public class StorageController:BaseController
    {
        private readonly IStorage _storage;
        
        public StorageController(IStorage storage)
        {
            _storage = storage;
           
        }


        [HttpGet]
        [Route("GetStorageDetails")]
        public async Task<List<GRNDetailsResponse>> GetStorageDetails(int id)
        {
            return await _storage.GetStorageDetails(id);
        }

        [HttpPost]
        [Route("UpdateStorageLocation")]
        public async Task<IActionResult> UpdateStorage(GRNDetailsRequest requestModel)
        {
            CommonHelper.SetUserInformation(ref requestModel, Convert.ToInt32(requestModel.GRNDetailsId), HttpContext);
            var result = await _storage.UpdateStorage(requestModel);
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
