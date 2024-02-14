using AutoMapper;
using Kemar.UrgeTruck.Domain.Common;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemar.UrgeTruck.Repository.Repositories
{
    public class ApplicationConfigurationRepository : IApplicationConfiguration
    {
        private readonly IKUrgeTruckContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public ApplicationConfigurationRepository(IKUrgeTruckContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<ApplicationConfigurationResponse>> GetAllActiveApplicationConfigs()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var appConfigs = await kUrgeTruckContext.ApplicationConfigMaster
                                                    .Where(x => x.IsActive == true)
                                                    .ToListAsync();
            return _mapper.Map<List<ApplicationConfigurationResponse>>(appConfigs);
        }

        public async Task<ApplicationConfigurationResponse> GetApplicationConfigByKey(string key)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var appConfig = await kUrgeTruckContext.ApplicationConfigMaster.FirstOrDefaultAsync(x => x.Key == key);
                return _mapper.Map<ApplicationConfigurationResponse>(appConfig);
            }
            catch (System.Exception ex)
            {
                Logger.Error("Error while getting app config " + ex);
                throw;
            }
        }

        public async Task<List<ApplicationConfigurationResponse>> GetAllApplicationConfigs()
        {
            using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var appConfigs = await kUrgeTruckContext.ApplicationConfigMaster
                                                    .ToListAsync();
            return _mapper.Map<List<ApplicationConfigurationResponse>>(appConfigs);
        }

        public async Task<string> GetParameterValue(string key)
        {
            try
            {
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var appConfig = await kUrgeTruckContext.ApplicationConfigMaster.FirstOrDefaultAsync(x => x.Key == key);
                return appConfig.Value;
            }
            catch (System.Exception ex)
            {
                Logger.Error("Error while getting app config " + ex);
                throw;
            }
        }

        public async Task GenerateAppConfig(string Key, string Value)
        {
            try
            {
                ApplicationConfigMaster AppConfig = new ApplicationConfigMaster();
                using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var appConfig = await kUrgeTruckContext.ApplicationConfigMaster.FirstOrDefaultAsync(x => x.Key == Key);
                if (appConfig == null)
                {
                    AppConfig.Key = Key;
                    AppConfig.Value = Value;
                    AppConfig.IsActive = true;
                    AppConfig.CreatedBy = "System";
                    AppConfig.CreatedDate = System.DateTime.Now;

                    kUrgeTruckContext.ApplicationConfigMaster.Add(AppConfig);
                }
                else
                {
                    appConfig.Value = Value;
                    kUrgeTruckContext.ApplicationConfigMaster.Update(appConfig);
                }
                await kUrgeTruckContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                Logger.Error("Error while getting app config " + ex);
                throw;
            }
        }
    }
}
