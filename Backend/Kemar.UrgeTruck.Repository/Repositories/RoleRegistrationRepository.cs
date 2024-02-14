using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class RoleRegistrationRepository : IRoleRegistration
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public RoleRegistrationRepository(IKUrgeTruckContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<RoleMasterResponse>> GetAllActiveRolesAsync()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var roles = await kUrgeTruckContext.RoleMaster.Where(x => x.IsActive == true).ToListAsync();
            return _mapper.Map<List<RoleMasterResponse>>(roles);
        }

        public async Task<List<RoleMasterResponse>> GetAllRolesAsync()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var roles = await kUrgeTruckContext.RoleMaster.OrderBy(x => x.RoleId).ToListAsync();
            return _mapper.Map<List<RoleMasterResponse>>(roles);
        }

        public async Task<List<string>> GetAllRolegroupAsync()
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var roles = await kUrgeTruckContext.RoleMaster.Select(x => x.RoleGroup).Distinct().ToListAsync();
                return roles;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<RoleMasterResponse> GetRoleAsync(int roleId, string roleName = null)
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var role = await kUrgeTruckContext.RoleMaster.FirstOrDefaultAsync(x => x.RoleId == roleId || x.RoleName == roleName);
            return _mapper.Map<RoleMasterResponse>(role);
        }

        public async Task<ResultModel> RegisterRoleAsync(RoleMasterRequest request)
        {
            var resMessage = "Role ";
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var userRoleExists = kUrgeTruckContext.RoleMaster.Any(x => x.RoleName == request.RoleName && x.RoleId != request.RoleId);
                var role = await kUrgeTruckContext.RoleMaster
                                                 .FirstOrDefaultAsync(x => x.RoleId == request.RoleId || x.RoleName == request.RoleName);
                if (userRoleExists == true)
                {
                    role.RoleName = request.RoleName;
                    role.IsActive = request.IsActive;
                    role.RoleGroup = request.RoleGroup;
                    resMessage = resMessage + UrgeTruckMessages.updated_successfully;
                    kUrgeTruckContext.RoleMaster.Update(role);
                    request.RoleId = role.RoleId;
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
               
                if (role == null)
                {
                    var newRole = _mapper.Map<RoleMaster>(request);
                    kUrgeTruckContext.Add(newRole);
                    resMessage = resMessage + UrgeTruckMessages.added_successfully;
                    await kUrgeTruckContext.SaveChangesAsync();
                    request.RoleId = newRole.RoleId;
                    return ResultModelFactory.CreateSucess(resMessage);
                }
                return  null;
            }

            
            catch (Exception ex)
            {
                Logger.Error("Error while register role " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, UrgeTruckMessages.Error_while_addupdate_role, ex);
            }
        }
    }
}
