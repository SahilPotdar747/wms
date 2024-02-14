using Kemar.UrgeTruck.Domain.SPDTOs;
using Kemar.UrgeTruck.Repository.Entities;
using Kemar.UrgeTruck.Repository.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Kemar.UrgeTruck.Repository.Context
{
    public class KUrgeTruckContext : DbContext
    {
        public DbContextOptions<KUrgeTruckContext> Options { get; }

        public KUrgeTruckContext() { }



        public KUrgeTruckContext(DbContextOptions<KUrgeTruckContext> options)
            : base(options)
        {
            Options = options;
        }

        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<UserScreenMaster> UserScreenMaster { get; set; }
        public virtual DbSet<UserAccessManager> UserAccessManager { get; set; }
        public virtual DbSet<UserManager> UserManager { get; set; }
        public virtual DbSet<ShiftMaster> ShiftMaster { get; set; }
        public virtual DbSet<ApplicationConfigMaster> ApplicationConfigMaster { get; set; }
        public virtual DbSet<TransGenerator> TransGenerator { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Uom> Uom { get; set; }
        public virtual DbSet<LocationCategory> LocationCategory { get; set; }
        //public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<GRN> GRN { get; set; }
        public virtual DbSet<GRNDetails> GRNDetails { get; set; }
        public virtual DbSet<DamageDetail> DamageDetail { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Dispatch> Dispatch { get; set; }
        public virtual DbSet<DispatchDetail> DispatchDetail { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ProductMaster> ProductMaster { get; set; }
        public virtual DbSet<UserLocationAccess> UserLocationAccess { get; set; }
        public virtual DbSet<SupplierMaster> SupplierMaster { get; set; }
        public virtual DbSet<DeliveryChallanMaster> DeliveryChallanMaster { get; set; }
        public virtual DbSet<DeliveryChallanDetails> DeliveryChallanDetails { get; set; }
        public virtual DbSet<CustomerMaster> CustomerMaster { get; set; }
        public virtual DbSet<PurchaseOrderMaster> PurchaseOrderMaster { get; set; }
        public virtual DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public virtual DbSet<GatePassMaster> GatePassMaster { get; set; }
        public virtual DbSet<GatePassDetails> GatePassDetails { get; set; }
        public virtual DbSet<RGPMaster> RGPMaster { get; set; }






        // Transaction tables
        public virtual DbSet<RetryObjectContainer> RetryObjectContainer { get; set; }
        public virtual DbSet<ThirdPartyServiceIntegrationTracking> ThirdPartyServiceIntegrationTracking { get; set; }

        //Stored Procedure Model
        //public virtual DbSet<CommonMasterData> CommonMasterData { get; set; }

        [System.Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleMasterConfiguration());
            modelBuilder.ApplyConfiguration(new UserScreenMasterConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccessManagerConfiguration());
            modelBuilder.ApplyConfiguration(new UserManagerConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftMasterConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationConfigMasterConfiguration());
            modelBuilder.ApplyConfiguration(new FailureLogConfiguration());
            modelBuilder.ApplyConfiguration(new TransGeneratorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UomConfiguration());
            modelBuilder.ApplyConfiguration(new LocationCategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new RateConfiguration());
            modelBuilder.ApplyConfiguration(new GRNConfiguration());
            modelBuilder.ApplyConfiguration(new GRNDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new DamageDetailConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new DispatchConfiguration());
            modelBuilder.ApplyConfiguration(new DispatchDetailConfiguration());
            modelBuilder.ApplyConfiguration(new UserLocationAccessConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());


            // Transaction
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ProductMasterConfiguration());
            modelBuilder.ApplyConfiguration(new RetryObjectContainerConfiguration());

            modelBuilder.ApplyConfiguration(new ThirdPartyServiceIntegrationTrackingConfiguration());

            modelBuilder.ApplyConfiguration(new EmailConfigConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierMasterConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryChallanMasterConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryChallanDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerMasterConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new GatePassMasterConfiguration());
            modelBuilder.ApplyConfiguration(new GatePassDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new RGPMasterConfiguration());


        }
    }
}
