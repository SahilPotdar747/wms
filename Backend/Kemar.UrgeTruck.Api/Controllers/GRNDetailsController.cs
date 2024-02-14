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
    public class GRNDetailsController:BaseController
    { 
        private readonly IGRNDetails  _GRNDetails;
        private readonly ILocation _location;
        public GRNDetailsController(IGRNDetails GRNDetails, ILocation location)
        {
            _GRNDetails = GRNDetails;
            _location = location;
        }


        [HttpGet]
        [Route("GetGRNDetails")]
        public async Task<List<GRNDetailsResponse>> GetGRNDetailsAsync([FromQuery] long id)
        {
            return await _GRNDetails.GetGRNDetailsAsync(id);
        }

        [HttpGet]
        [Route("GetAllGrnDetails")]
        public async Task<List<GRNDetailsResponse>> GetAllGrnDetails()
        {
            return await _GRNDetails.GetAllGrnDetails();
        }



        [HttpPost]
        [Route("addGRNDetails")]
        public async Task<IActionResult> addGRNDetails(List<GRNDetailsRequest> requestModels)
        {
            foreach (var requestModel in requestModels)
            {
                var copyRequestModel = requestModel;
                CommonHelper.SetUserInformation(ref copyRequestModel, Convert.ToInt32(requestModel.GRNDetailsId), HttpContext);
            }
            var result = await _GRNDetails.addGRNDetails(requestModels);
            return ReturnResposneType(result);
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> TestAysnc()
        {
            var ss = ResultModelFactory.CreateSucess("Test");
            return ReturnResposneType(ss);
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
