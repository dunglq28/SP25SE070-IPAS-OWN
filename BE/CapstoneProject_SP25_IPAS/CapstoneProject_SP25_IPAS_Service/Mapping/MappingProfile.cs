using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PlantLotModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.UserBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
