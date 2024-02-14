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
using Kemar.UrgeTruck.Repository.Entities;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderDetailsController : ControllerBase
    {
        private readonly IPurchaseOrderDetails _PODetails;
        public PurchaseOrderDetailsController(IPurchaseOrderDetails PODetails)
        {
            _PODetails = PODetails;

        }

        [HttpGet]
        [Route("GetPurchaseOrderDetails")]
        public async Task<List<PurchaseOrderDetailsResponse>> GetPurchaseOrderDetails([FromQuery] int id)
        {
            return await _PODetails.GetPurchaseOrderDetails(id);
        }

        [HttpPost]
        [Route("SavePODetails")]
        public async Task<IActionResult> SavePODetails(List<PurchaseOrdeDetailsrRequest> request)
        {
            ResultModel result = new ResultModel();
           foreach(var requests in request)
            {
                var req = requests;
                CommonHelper.SetUserInformation(ref req, requests.PODId, HttpContext);                 
            }
            result = await _PODetails.SavePODetails(request);
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
