using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Domain.RequestModel;
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

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocation _location;
        //private readonly ILocationCategory _locationCategory;



        public LocationsController(ILocation location)
        {
            _location = location;
        }



        [HttpGet]
        [Route("GetLocations")]
        public async Task<List<LocationListResponse>> GetLocations([FromQuery] string locCode)
         {
            return await _location.GetLocationAsync(locCode);
        }

        [HttpGet]
        [Route("GetLocationsDropdown")]
        public async Task<List<LocationListResponse>> GetLocationsDropdown()
        {
            return await _location.GetLocationsdropdownAsync();
        }

        [HttpPost]
        [Route("AddLocations")]
        public async Task<IActionResult> AddOrUpdateLocationsAsync(LocationRequest request)
        {
            CommonHelper.SetUserInformation(ref request, Convert.ToInt32(request.LocationId), HttpContext);
            var result = await _location.AddOrUpdateLocationsAsync(request);
            return ReturnResposneType(result);
        }

        [HttpGet]
        [Route("GetLocationCategory")]
        public async Task<List<LocationCategoryResponse>> GetLocationCategoryAsync()
        {
            return await _location.GetLocationCategoryAsync();
        }

        [HttpGet]
        [Route("GetGRNDropDown")]
        public async Task<List<LocationListResponse>> GetGRNDropDownAsync(int userId)
        {
            return await _location.GetGRNDropDownAsync(userId);
        }


        [HttpGet]
        [Route("GerRackSubrackDropDown")]
         public async Task<List<LocationListResponse>> GerRackSubrackDropDownAsync(string name)
        {
            return await _location.GerRackSubrackDropDownAsync(name);
        }





        [HttpPost]
        [Route("SaveLocationCategory")]
        public async Task<IActionResult> SaveLocationCategory(LocationCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data model");

           //CommonHelper.SetUserInformation(ref request, request.LocationCategoryId, HttpContext);

            var resultModel = await _location.SaveLocationCategory(request);

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