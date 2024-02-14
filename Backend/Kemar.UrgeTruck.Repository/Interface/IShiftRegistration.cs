using Kemar.UrgeTruck.Domain.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Interface
{
    public interface IShiftRegistration
    {
        Task<ShiftResponse> GetCurrentShiftAsync();
        Task<List<ShiftResponse>> GetAllShiftsAsync();
    }
}
