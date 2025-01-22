using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.GrowthStageModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.ProcessStyleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.IService
{
    public interface IProcessStyleService
    {
        public Task<BusinessResult> GetProcessStyleByID(int processStyleId);

        public Task<BusinessResult> GetAllProcessStylePagination(PaginationParameter paginationParameter);

        public Task<BusinessResult> CreateProcessStyle(CreateProcessStyleModel createProcessStyleModel);

        public Task<BusinessResult> UpdateProcessStyleInfo(UpdateProcessStyleModel updateProcessTyleModel);

        public Task<BusinessResult> PermanentlyDeleteProcessStyle(int processStyleId);
    }
}
