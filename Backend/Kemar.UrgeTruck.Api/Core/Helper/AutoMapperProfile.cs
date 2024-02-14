using AutoMapper;
using Kemar.UrgeTruck.Domain.DTOs;
using Kemar.UrgeTruck.Domain.RequestModel;
using Kemar.UrgeTruck.Domain.ResponseModel;
using Kemar.UrgeTruck.Repository.Entities;

namespace Kemar.UrgeTruck.Api.Core.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserRequest, UserManager>().ReverseMap();
            CreateMap<UserManager, AuthenticateResponse>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleMaster.RoleName));
            CreateMap<RoleMaster, RoleMasterResponse>();
            CreateMap<RoleMasterResponse, RoleMasterRequest>();
            CreateMap<RoleMasterRequest, RoleMaster>().ReverseMap();
            CreateMap<UserScreenMasterRequest, UserScreenMaster>();
            CreateMap<UserScreenMaster, UserScreenMasterResponse>();
            CreateMap<UserAccessManagerRequest, UserAccessManager>();
            CreateMap<UserAccessManagerResponse, UserAccessManagerRequest>();
            CreateMap<UserAccessManager, UserAccessManagerResponse>()
                .ForMember(dest => dest.ScreenName, opt => opt.MapFrom(src => src.UserScreenMaster.ScreenName))
                .ForMember(dest => dest.ScreenCode, opt => opt.MapFrom(src => src.UserScreenMaster.ScreenCode))
                  .ForMember(dest => dest.MenuIcon, opt => opt.MapFrom(src => src.UserScreenMaster.MenuIcon))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleMaster.RoleName))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.UserScreenMaster.ParentId));

            CreateMap<UserManager, UserTokenDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleMaster.RoleName))
                .ForMember(dest => dest.UserAccess, opt => opt.MapFrom(src => src.RoleMaster.UserAccessManager));

            CreateMap<UserAccessManager, UserAccessDto>()
                .ForMember(dest => dest.ScreenCode, opt => opt.MapFrom(src => src.UserScreenMaster.ScreenCode))
                 .ForMember(dest => dest.CanRead, opt => opt.MapFrom(src => src.IsActive));
                    CreateMap<RoleMaster, UserControlResponse>()
          .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
          .ForMember(dest => dest.UserAccessManagerResonse, opt => opt.MapFrom(src => src.UserAccessManager));
            CreateMap<ApplicationConfigMaster, ApplicationConfigurationResponse>();
            CreateMap<ApplicationConfigurationRequest, ApplicationConfigMaster>();
            CreateMap<CommonMasterData, CommonMasterDataResponse>();
            CreateMap<CommonMasterDataRequest, CommonMasterData>();
            CreateMap<Location, LocationListResponse>();
            CreateMap<LocationRequest, Location>();
            CreateMap<LocationCategory,LocationCategoryResponse>();
            CreateMap<LocationCategoryRequest, LocationCategory>();
            CreateMap<ProductMasterRequest, ProductMaster>();
            CreateMap<ProductMaster, ProductMasterResponse>();
            CreateMap<SupplierMaster, SupplierMasterResponse>();
            CreateMap<SupplierMasterRequest, SupplierMaster>();
            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<ProductCategoryRequest, ProductCategory>();
            CreateMap<GRN, GRNMasterResponse>();
            CreateMap<GRNMasterRequest, GRN>();
            CreateMap<GRNDetails, GRNDetailsResponse>();
            CreateMap<GRNDetailsRequest, GRNDetails>();
            CreateMap<DeliveryChallanMaster, DeliveryChallanMasterResponse>();
            CreateMap<DeliveryChallanMasterRequest, DeliveryChallanMaster>();

            CreateMap<DeliveryChallanDetails, DeliveryChallanDetailsResponse>();
            CreateMap<DeliveryChallanDetailsRequest, DeliveryChallanDetails>();

            CreateMap<CustomerMaster, CustomerResponse>();
            CreateMap<CustomerRequest, CustomerMaster>();

            CreateMap<PurchaseOrderMaster, PurchaseOrderResponse>();
            CreateMap<PurchaseOrderRequest, PurchaseOrderMaster>();
            CreateMap<PurchaseOrderDetails, PurchaseOrderDetailsResponse>()
                .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.ProductMaster.ProductName))
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductMaster.Price));

            CreateMap<PurchaseOrdeDetailsrRequest, PurchaseOrderDetails>();

            CreateMap<GatePassMaster, GatePassMasterResponse>()
                .ForMember(dest => dest.PurchaseOrderMaster, opt => opt.MapFrom(src => src.PurchaseOrderMaster));
           //.ForMember(dest => dest.PurchaseOrderDetails, opt => opt.MapFrom(src => src.PurchaseOrderMaster.PurchaseOrderDetails));
           
            CreateMap<GatePassMasterRequest, GatePassMaster>();

            CreateMap<GatePassDetails, GatePassDetailResponse>()
               .ForMember(x => x.GatePassMasterResponse, opt => opt.MapFrom(src => src.GatePassMaster));
             CreateMap<GatePassDetailsRequest, GatePassDetails>();


            CreateMap<RGPMaster, RGPMasterResponse>()
             .ForMember(x => x.PurchaseOrderMaster, opt => opt.MapFrom(src => src.GatePassMaster.PurchaseOrderMaster));
            CreateMap<RGPMasterRequest, RGPMaster>();


            
           





        }
    }
}
