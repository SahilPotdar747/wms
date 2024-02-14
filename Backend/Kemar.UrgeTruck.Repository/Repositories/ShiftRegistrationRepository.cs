using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class ShiftRegistrationRepository : IShiftRegistration
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public ShiftRegistrationRepository(IKUrgeTruckContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<ShiftResponse>> GetAllShiftsAsync()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var shifts = await kUrgeTruckContext.ShiftMaster
                                .Where(x => x.IsActive == true).ToListAsync();
            return _mapper.Map<List<ShiftResponse>>(shifts);
        }

        public async Task<ShiftResponse> GetCurrentShiftAsync()
        {
            string crrentDate = DateTimeFormatter.GetISDTime(DateTime.Now).ToString("MM/dd/yyyy hh:mm tt");
            TimeSpan currntTime = Convert.ToDateTime(crrentDate).TimeOfDay;

            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var shift = await kUrgeTruckContext.ShiftMaster
                                .FirstOrDefaultAsync(x => Convert.ToDateTime(x.StartTime.ToString("MM/dd/yyyy hh:mm tt")).TimeOfDay <= currntTime
                                                       && Convert.ToDateTime(x.EndTime.ToString("MM/dd/yyyy hh:mm tt")).TimeOfDay >= currntTime);
            return _mapper.Map<ShiftResponse>(shift);
        }
    }
}
