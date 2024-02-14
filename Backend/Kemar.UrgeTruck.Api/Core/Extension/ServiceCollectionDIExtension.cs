using Kemar.UrgeTruck.Business.BusinessLogic;
using Kemar.UrgeTruck.Business.Interfaces;
using Kemar.UrgeTruck.Repository;
using Kemar.UrgeTruck.Repository.Context;
using Kemar.UrgeTruck.Repository.FRDB.FRDBEntity;
using Kemar.UrgeTruck.Repository.Interface;
using Kemar.UrgeTruck.Repository.Repositories;
using Kemar.UrgeTruck.Repository.Repositories.Interface;
using Kemar.UrgeTruck.ServiceIntegration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Kemar.UrgeTruck.Api.Core.Extension
{
    public static class ServiceCollectionDIExtension
    {
        public static void ConfigureServicesDependency(IServiceCollection services)
        {
            RepositoryDependency(services);
            BusinessDepedency(services);
            ServiceDependency(services);
            ControlTowerDependency(services);
            SchedulerDependency(services);

            // Configure retry resilience service
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //services.AddHostedService<QuartzHostedService>();
        }

        private static void SchedulerDependency(IServiceCollection services)
        {
            
        }

        private static void RepositoryDependency(IServiceCollection services)
        {
            services.AddSingleton<IKUrgeTruckContextFactory, KUrgeTruckContextFactory>();
            services.AddScoped<IUserManager, UserManagerRepository>();
            services.AddScoped<ILocation , LocationRepository>();
          
            services.AddScoped<IShiftRegistration, ShiftRegistrationRepository>();
            services.AddSingleton<IApplicationConfiguration, ApplicationConfigurationRepository>();
            services.AddScoped<IRoleRegistration, RoleRegistrationRepository>();
            services.AddScoped<IUserScreen, UserScreenRepository>();
            services.AddScoped<IUserAccessManager, UserAccessManagerRepository>();
            services.AddScoped<IProjectVersioning, ProjectVersionRepository>();
            services.AddScoped<IProductMaster, ProductMasterRepository>();
            services.AddScoped<ISupplierMaster, SupplierMasterRepository>();
            services.AddScoped<IGRNMaster, GRNMasterRepository>();
            services.AddScoped<IGRNDetails, GRNDetailsRepository>();
            services.AddScoped<IStorage, StorageRepository>();
            services.AddScoped<IOutward, OutwardRepository>();
            services.AddScoped<IDeliveryChallan, DeliveryChallanRepository>();
            services.AddScoped<ICustomer, CustomerRepository>();
            services.AddScoped<IPurchaseOrder, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderDetails, PurchaseOrderDetailsRepository>();
            services.AddScoped<IGatePassMaster, GatePassMasterRepository>();
            services.AddScoped<IGatePassDetails, GatePassDetailRepository>();
            services.AddScoped<IRGPMaster, RGPMasterRepository>();



            // services.AddScoped<ICommonMasterData, CommonMasterDataRepository>();


        }

        private static void BusinessDepedency(IServiceCollection services)
        {
            services.AddScoped<ITransactionId, TransactionIdRepository>();
            services.AddScoped<IUserRoleAccessManager, UserRoleAccessManager>();
            services.AddScoped<IFRDBContext, FRDBContextFactory>();
        }

        private static void ServiceDependency(IServiceCollection services)
        {
            services.AddScoped<IEmailManager, EmailManager>();
        }

        private static void ControlTowerDependency(IServiceCollection services)
        {
        }
    }
}
