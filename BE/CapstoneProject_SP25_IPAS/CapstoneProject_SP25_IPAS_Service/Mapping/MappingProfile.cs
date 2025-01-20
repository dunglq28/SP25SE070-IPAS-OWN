using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PlantLotModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.FarmBsModels;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.UserBsModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PartnerModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.GrowthStageModel;

namespace CapstoneProject_SP25_IPAS_Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>()
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role.RoleName))
                .ReverseMap();

            CreateMap<PlantLot, PlantLotModel>()
                .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(x => x.Partner.PartnerName))
               .ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Farm, FarmModel>()
            .ForMember(dest => dest.FarmCoordinations, opt => opt.MapFrom(src => src.FarmCoordinations))
            //.ForMember(dest => dest.LandPlots, opt => opt.MapFrom(src => src.LandPlots))
            //.ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
            //.ForMember(dest => dest.Processes, opt => opt.MapFrom(src => src.Processes))
            .ForMember(dest => dest.UserFarms, opt => opt.MapFrom(src => src.UserFarms))
            .ReverseMap();
            CreateMap<FarmCoordination, FarmCoordinationModel>();

            CreateMap<LandPlot, LandPlotModel>();

            CreateMap<UserFarm, UserFarmModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) 
                .ForMember(dest => dest.Farm, opt => opt.MapFrom(src => src.Farm))
                .ReverseMap();

            CreateMap<CriteriaType, CriteriaTypeModel>()
                .ForMember(dest => dest.GrowthStageName, opt => opt.MapFrom(src => src.GrowthStage.GrowthStageName))
                .ForMember(dest => dest.ListCriteria, opt => opt.MapFrom(src => src.Criteria))
                .ReverseMap();

            CreateMap<Partner, PartnerModel>()
               .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName)).ReverseMap();

            CreateMap<GrowthStage, GrowthStageModel>().ReverseMap();

        }
    }
}
