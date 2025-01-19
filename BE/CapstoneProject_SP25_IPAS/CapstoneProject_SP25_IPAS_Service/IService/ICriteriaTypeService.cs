using CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.IService
{
    public interface ICriteriaTypeService
    {
        public Task<BusinessResult> GetCriteriaTypeByID(int criteriaTypeId);

        public Task<BusinessResult> GetAllCriteriaTypePagination(PaginationParameter paginationParameter);

        public Task<BusinessResult> CreateCriteriaType(CreateCriteriaTypeModel createCriteriaTypeModel);

        public Task<BusinessResult> UpdateCriteriaTypeInfo(UpdateCriteriaTypeModel updateriteriaTypeModel);

        public Task<BusinessResult> PermanentlyDeleteCriteriaType(int criteriaTypeId);

        public Task<BusinessResult> GetCriteriaTypeByName(string criteriaTypeName);
    }
}
