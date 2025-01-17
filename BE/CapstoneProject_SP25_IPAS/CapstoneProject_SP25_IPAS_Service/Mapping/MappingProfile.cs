using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.FarmBsModels;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.UserBsModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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

        }
    }
}
