using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Interface;
using Kemar.UrgeTruck.Repository.Repositories.Interface;
using Kemar.UrgeTruck.ServiceIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocation _location;

        public LocationController(ILocation location)
        {
            _location = location;
        }

        [HttpGet]
        [Route("GetLocations")]
        public async Task<List<LocationListResponse>> GetLocations([FromQuery] string location)
        {
            List<LocationListResponse> LocationList = new List<LocationListResponse>();
            return await _location.GetLocationAsync(location, LocationList);
        }
    }
}
