﻿using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.ProcessModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.IService
{
    public interface IProcessService
    {
        public Task<BusinessResult> GetProcessByID(int processId);

        public Task<BusinessResult> GetAllProcessPagination(PaginationParameter paginationParameter, ProcessFilters processFilters);

        public Task<BusinessResult> CreateProcess(CreateProcessModel createProcessModel);

        public Task<BusinessResult> UpdateProcessInfo(UpdateProcessModel updateProcessModel);

        public Task<BusinessResult> PermanentlyDeleteProcess(int processId);
        public Task<BusinessResult> SoftDeleteProcess(int processId);

        public Task<BusinessResult> GetProcessByName(string processName);

        public Task<BusinessResult> GetProcessDataByID(int processId);
        public Task<BusinessResult> InsertManyProcess(List<CreateProcessModel> listCreateProcessModel);
    }
}
