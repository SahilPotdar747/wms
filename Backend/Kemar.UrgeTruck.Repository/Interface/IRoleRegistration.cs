using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IRoleRegistration
    {
        Task<ResultModel> RegisterRoleAsync(RoleMasterRequest request);
        Task<List<RoleMasterResponse>> GetAllActiveRolesAsync();
        Task<List<RoleMasterResponse>> GetAllRolesAsync();
        Task<List<string>> GetAllRolegroupAsync();
        Task<RoleMasterResponse> GetRoleAsync(int roleId, string roleName = null);
    }
}
