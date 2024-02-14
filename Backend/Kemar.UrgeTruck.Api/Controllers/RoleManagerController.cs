using Kemar.UrgeTruck.Api.Core.Auth;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleManagerController : ControllerBase
    {
        private readonly IRoleRegistration _roleRegistration;

        public RoleManagerController(IRoleRegistration roleRegistration)
        {
            _roleRegistration = roleRegistration;
        }

        [HttpPost]
        [Route("registerrole")]
        public async Task<IActionResult> RegisterRoleAsync([FromBody] RoleMasterRequest roleRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data model");
           
            var resultModel = await _roleRegistration.RegisterRoleAsync(roleRequest);

            return ReturnResposneType(resultModel);
        }

        [HttpGet]
        [Route("getallroles")]
        public async Task<List<RoleMasterResponse>> GetAllRolesAsync()
        {
            return await _roleRegistration.GetAllRolesAsync();
        }

        [HttpGet]
        [Route("getallrolegroup")]
        public async Task<List<string>> GetAllRolegroupAsync()
        {
            var x = await _roleRegistration.GetAllRolegroupAsync();
            return x;
        }

        [HttpGet]
        [Route("getallactiveroles")]
        public async Task<List<RoleMasterResponse>> GetAllActiveRolesAsync()
        {
            return await _roleRegistration.GetAllActiveRolesAsync();
        }

        [HttpGet]
        [Route("getrole")]
        public async Task<RoleMasterResponse> GetRoleAsync(int roleId)
        {
            return await _roleRegistration.GetRoleAsync(roleId);
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
            else if (result.StatusCode == ResultCode.ExceptionThrown)
                return NotFound(result);

            return null;
        }
    }
}
