﻿using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Common.Constants;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PartnerModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.ProcessModel;
using CapstoneProject_SP25_IPAS_Service.ConditionBuilder;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Pagination;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Process = CapstoneProject_SP25_IPAS_BussinessObject.Entities.Process;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class ProcessService : IProcessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public ProcessService(IUnitOfWork unitOfWork, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<BusinessResult> CreateProcess(CreateProcessModel createProcessModel)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newProcess = new Process()
                    {
                        ProcessCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.PROCESS),
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        FarmId = createProcessModel.FarmId,
                        GrowthStageID = createProcessModel.GrowthStageID,
                        ProcessStyleId = createProcessModel.ProcessStyleId,
                        ProcessName = createProcessModel.ProcessName,
                        IsDefault = false,
                        IsActive = createProcessModel.IsActive,
                        IsDeleted = false
                    };
                    if(createProcessModel.ListProcessData != null)
                    {
                        foreach (var newData in createProcessModel.ListProcessData)
                        {
                            var getLink = "";
                            if (IsImageFile(newData))
                            {
                                getLink = await _cloudinaryService.UploadImageAsync(newData, "process/data");
                            }
                            else
                            {
                                getLink = await _cloudinaryService.UploadVideoAsync(newData, "process/data");
                            }
                            newProcess.ProcessData.Add(new ProcessData()
                            {
                                ResourceUrl = getLink,
                                CreateDate = DateTime.Now,
                                ProcessDataCode = NumberHelper.GenerateRandomCode("PCD")
                            });
                        }
                    }
                    await _unitOfWork.ProcessRepository.Insert(newProcess);
                    
                    if (createProcessModel.ListSubProcess != null)
                    {
                       
                        foreach (var subProcessRaw in createProcessModel.ListSubProcess)
                        {
                            var subProcess = JsonConvert.DeserializeObject <AddSubProcessModel>(subProcessRaw);
                            var newSubProcess = new SubProcess()
                            {
                                SubProcessCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.SUB_PROCESS),
                                SubProcessName = subProcess.SubProcessName,
                                CreateDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                IsDefault = false,
                                IsActive = subProcess.IsActive,
                                IsDeleted = false,
                                ParentSubProcessId = subProcess.ParentSubProcessId,
                                ProcessStyleId = subProcess.ProcessStyleId,
                            };

                            newProcess.SubProcesses.Add(newSubProcess);
                        }
                    }
                    var checkInsertProcess = await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    if (checkInsertProcess > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_PROCESS_CODE, Const.SUCCESS_CREATE_PROCESS_MESSAGE, checkInsertProcess > 0); ;
                    }
                    return new BusinessResult(Const.FAIL_CREATE_PROCESS_CODE, Const.FAIL_CREATE_PROCESS_MESSAGE, false);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

    public async Task<BusinessResult> GetAllProcessPagination(PaginationParameter paginationParameter, ProcessFilters processFilters)
    {
            try
            {
                Expression<Func<Process, bool>> filter = null!;
                Func<IQueryable<Process>, IOrderedQueryable<Process>> orderBy = null!;
                if (!string.IsNullOrEmpty(paginationParameter.Search))
                {
                    int validInt = 0;
                    var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                    DateTime validDate = DateTime.Now;
                    bool validBool = false;
                    if (checkInt)
                    {
                        filter = filter.And(x => x.ProcessId == validInt);
                    }
                    else if (DateTime.TryParse(paginationParameter.Search, out validDate))
                    {
                        filter = filter.And(x => x.CreateDate == validDate
                                      || x.UpdateDate == validDate);
                    }
                    else if (Boolean.TryParse(paginationParameter.Search, out validBool))
                    {
                        filter = filter.And(x => x.IsDeleted == validBool || x.IsDefault == validBool || x.IsActive == validBool);
                    }
                    else
                    {
                        filter = filter.And(x => x.ProcessCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.ProcessName.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.ProcessStyle.ProcessStyleName.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Farm.FarmName.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.GrowthStage.GrowthStageName.ToLower().Contains(paginationParameter.Search.ToLower()));
                    }
                }

                if (processFilters.createDateFrom.HasValue || processFilters.createDateTo.HasValue)
                {
                    if (!processFilters.createDateFrom.HasValue || !processFilters.createDateTo.HasValue)
                    {
                        return new BusinessResult(Const.WARNING_MISSING_DATE_FILTER_CODE, Const.WARNING_MISSING_DATE_FILTER_MSG);
                    }

                    if (processFilters.createDateFrom.Value > processFilters.createDateTo.Value)
                    {
                        return new BusinessResult(Const.WARNING_INVALID_DATE_FILTER_CODE, Const.WARNING_INVALID_DATE_FILTER_MSG);
                    }
                    filter = filter.And(x => x.CreateDate >= processFilters.createDateFrom &&
                                             x.CreateDate <= processFilters.createDateTo);
                }

                if (processFilters.isActive != null)
                    filter = filter.And(x => x.IsActive == processFilters.isActive);
                if(processFilters.ProcessType != null)
                {
                    filter = filter.And(x => x.ProcessStyle.ProcessStyleName.Contains(processFilters.ProcessType));
                }

                if (processFilters.GrowthStage != null)
                {
                    filter = filter.And(x => x.GrowthStage.GrowthStageName.Contains(processFilters.GrowthStage));
                }

                switch (paginationParameter.SortBy)
                {
                    case "processid":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.ProcessId)
                                   : x => x.OrderBy(x => x.ProcessId)) : x => x.OrderBy(x => x.ProcessId);
                        break;
                    case "processcode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.ProcessCode)
                                   : x => x.OrderBy(x => x.ProcessCode)) : x => x.OrderBy(x => x.ProcessCode);
                        break;
                    case "processname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.ProcessName)
                                   : x => x.OrderBy(x => x.ProcessName)) : x => x.OrderBy(x => x.ProcessName);
                        break;
                    case "processstylename":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.ProcessStyle.ProcessStyleName)
                                   : x => x.OrderBy(x => x.ProcessStyle.ProcessStyleName)) : x => x.OrderBy(x => x.ProcessStyle.ProcessStyleName);
                        break;
                    case "growthstagename":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.GrowthStage.GrowthStageName)
                                   : x => x.OrderBy(x => x.GrowthStage.GrowthStageName)) : x => x.OrderBy(x => x.GrowthStage.GrowthStageName);
                        break;
                    case "farmname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Farm.FarmName)
                                   : x => x.OrderBy(x => x.Farm.FarmName)) : x => x.OrderBy(x => x.Farm.FarmName);
                        break;
                    case "isdeleted":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.IsDeleted)
                                   : x => x.OrderBy(x => x.IsDeleted)) : x => x.OrderBy(x => x.IsDeleted);
                        break;
                    case "isdefault":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.IsDefault)
                                   : x => x.OrderBy(x => x.IsDefault)) : x => x.OrderBy(x => x.IsDefault);
                        break;
                    case "isactive":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.IsActive)
                                   : x => x.OrderBy(x => x.IsActive)) : x => x.OrderBy(x => x.IsActive);
                        break;
                    case "createdate":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CreateDate)
                                   : x => x.OrderBy(x => x.CreateDate)) : x => x.OrderBy(x => x.CreateDate);
                        break;
                    case "updatedate":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.UpdateDate)
                                   : x => x.OrderBy(x => x.UpdateDate)) : x => x.OrderBy(x => x.UpdateDate);
                        break;
                    default:
                        orderBy = x => x.OrderBy(x => x.ProcessId);
                        break;
                }
                string includeProperties = "GrowthStage,Farm,ProcessStyle,ProcessData,SubProcesses";
                var entities = await _unitOfWork.ProcessRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
                var pagin = new PageEntity<ProcessModel>();
                pagin.List = _mapper.Map<IEnumerable<ProcessModel>>(entities).ToList();
                pagin.TotalRecord = await _unitOfWork.ProcessRepository.Count();
                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
                if (pagin.List.Any())
                {
                    return new BusinessResult(Const.SUCCESS_GET_ALL_PROCESS_CODE, Const.SUCCESS_GET_ALL_PROCESS_MESSAGE, pagin);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG, new PageEntity<ProcessModel>());
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

    public async Task<BusinessResult> GetProcessByID(int processId)
    {
            try
            {
                var getProcess = await _unitOfWork.ProcessRepository.GetByCondition(x => x.ProcessId == processId, "GrowthStage,Farm,ProcessStyle,ProcessData,SubProcesses");
                if (getProcess != null)
                {
                    var result = _mapper.Map<ProcessModel>(getProcess);
                    return new BusinessResult(Const.SUCCESS_GET_PROCESS_BY_ID_CODE, Const.SUCCESS_GET_PROCESS_BY_ID_MESSAGE, result);
                }
                return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
    }

    public async Task<BusinessResult> GetProcessByName(string processName)
    {
            try
            {
                var getProcess = await _unitOfWork.ProcessRepository.GetByCondition(x => x.ProcessName.ToLower().Contains(processName.ToLower()), "GrowthStage,Farm,ProcessStyle,ProcessData,SubProcesses");
                if (getProcess != null)
                {
                    var result = _mapper.Map<ProcessModel>(getProcess);
                    return new BusinessResult(Const.SUCCESS_GET_PROCESS_BY_NAME_CODE, Const.SUCCESS_GET_PROCESS_BY_NAME_MESSAGE, result);
                }
                return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

    public async Task<BusinessResult> GetProcessDataByID(int processId)
    {
            try
            {
                var checkExist = await _unitOfWork.ProcessRepository.GetByCondition(x => x.ProcessId == processId, "ProcessData");
                if(checkExist == null)
                {
                    return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG);
                }
                var processData = checkExist.ProcessData.ToList();
                if(processData.Count() > 0)
                {
                    return new BusinessResult(Const.SUCCESS_GET_PROCESS_DATA_OF_PROCESS_CODE, Const.SUCCESS_GET_PROCESS_DATA_OF_PROCESS_MESSAGE, processData); ;
                }
                return new BusinessResult(Const.WARNING_GET_PROCESS_DATA_OF_PROCESS_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DATA_OF_PROCESS_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
    }

        public async Task<BusinessResult> InsertManyProcess(List<CreateProcessModel> listCreateProcessModel)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    foreach(var createProcessModel in listCreateProcessModel)
                    {
                        var newProcess = new Process()
                        {
                            ProcessCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.PROCESS),
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            FarmId = createProcessModel.FarmId,
                            GrowthStageID = createProcessModel.GrowthStageID,
                            ProcessStyleId = createProcessModel.ProcessStyleId,
                            ProcessName = createProcessModel.ProcessName,
                            IsDefault = false,
                            IsActive = createProcessModel.IsActive,
                            IsDeleted = false
                        };
                        foreach (var newData in createProcessModel.ListProcessData)
                        {
                            var getLink = "";
                            if (IsImageFile(newData))
                            {
                                getLink = await _cloudinaryService.UploadImageAsync(newData, "process/data");
                            }
                            else
                            {
                                getLink = await _cloudinaryService.UploadVideoAsync(newData, "process/data");
                            }
                            newProcess.ProcessData.Add(new ProcessData()
                            {
                                ResourceUrl = getLink,
                                CreateDate = DateTime.Now,
                                ProcessDataCode = NumberHelper.GenerateRandomCode("PCD")
                            });
                        }
                        await _unitOfWork.ProcessRepository.Insert(newProcess);
                        if(createProcessModel.ListSubProcess != null)
                        {
                            foreach (var subProcessRaw in createProcessModel.ListSubProcess)
                            {
                                var subProcess = JsonConvert.DeserializeObject<AddSubProcessModel>(subProcessRaw);
                                var newSubProcess = new SubProcess()
                                {
                                    SubProcessCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.SUB_PROCESS),
                                    SubProcessName = subProcess.SubProcessName,
                                    CreateDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
                                    IsDefault = false,
                                    IsActive = subProcess.IsActive,
                                    IsDeleted = false,
                                    ParentSubProcessId = subProcess.ParentSubProcessId,
                                    ProcessStyleId = subProcess.ProcessStyleId,
                                };
                                newProcess.SubProcesses.Add(newSubProcess);
                            }
                        }
                    }
                    var checkInsertProcess = await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    if (checkInsertProcess > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_PROCESS_CODE, Const.SUCCESS_CREATE_PROCESS_MESSAGE, checkInsertProcess > 0); ;
                    }
                    return new BusinessResult(Const.FAIL_CREATE_PROCESS_CODE, Const.FAIL_CREATE_PROCESS_MESSAGE, false);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

        public async Task<BusinessResult> PermanentlyDeleteProcess(int processId)
    {
            try
            {
                string includeProperties = "GrowthStage,Farm,ProcessStyle,ProcessData,SubProcesses";
                var deleteProcess = await _unitOfWork.ProcessRepository.GetByCondition(x => x.ProcessId == processId, includeProperties);
                
                foreach(var data in deleteProcess.ProcessData.ToList())
                {
                    _unitOfWork.ProcessDataRepository.Delete(data);
                    if(IsImageLink(data.ResourceUrl))
                    {
                        await _cloudinaryService.DeleteImageByUrlAsync(data.ResourceUrl);
                    }
                    else
                    {
                        await _cloudinaryService.DeleteVideoByUrlAsync(data.ResourceUrl);
                    }
                }
                foreach(var subProcess in deleteProcess.SubProcesses.ToList())
                {
                    foreach (var dataSubProcess in subProcess.ProcessData)
                    {
                        _unitOfWork.ProcessDataRepository.Delete(dataSubProcess);
                        if (IsImageLink(dataSubProcess.ResourceUrl))
                        {
                            await _cloudinaryService.DeleteImageByUrlAsync(dataSubProcess.ResourceUrl);
                        }
                        else
                        {
                            await _cloudinaryService.DeleteVideoByUrlAsync(dataSubProcess.ResourceUrl);
                        }
                    }
                     _unitOfWork.SubProcessRepository.Delete(subProcess);
                }
                await _unitOfWork.SaveAsync();
                _unitOfWork.ProcessRepository.Delete(deleteProcess);
                var result = await _unitOfWork.SaveAsync();
                if(result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_PROCESS_CODE, Const.SUCCESS_DELETE_PROCESS_MESSAGE, true);
                }
                return new BusinessResult(Const.FAIL_DELETE_PROCESS_CODE, Const.FAIL_DELETE_PROCESS_MESSAGE, false);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
    }

    public async Task<BusinessResult> UpdateProcessInfo(UpdateProcessModel updateProcessModel)
    {
            try
            {
                var checkExistProcess = await _unitOfWork.ProcessRepository.GetByCondition(x => x.ProcessId == updateProcessModel.ProcessId, "ProcessData");
                if(checkExistProcess != null)
                {
                    if (updateProcessModel.ProcessName != null)
                    {
                        checkExistProcess.ProcessName = updateProcessModel.ProcessName;
                    }
                    if (updateProcessModel.IsActive != null)
                    {
                        checkExistProcess.IsActive = updateProcessModel.IsActive;
                    }
                    if (updateProcessModel.IsDefault != null)
                    {
                        checkExistProcess.IsDefault = updateProcessModel.IsDefault;
                    }
                    if (updateProcessModel.IsDeleted != null)
                    {
                        checkExistProcess.IsDeleted = updateProcessModel.IsDeleted;
                    }
                    if (updateProcessModel.FarmId != null)
                    {
                        checkExistProcess.FarmId = updateProcessModel.FarmId;
                    }
                    if (updateProcessModel.ProcessStyleId != null)
                    {
                        checkExistProcess.ProcessStyleId = updateProcessModel.ProcessStyleId;
                    }
                    if (updateProcessModel.GrowthStageID != null)
                    {
                        checkExistProcess.GrowthStageID = updateProcessModel.GrowthStageID;
                    }
                    var processData = checkExistProcess.ProcessData.ToList();
                    if (updateProcessModel.ListUpdateProcessData != null && updateProcessModel.ListUpdateProcessData.Count > 0)
                    {

                        foreach (var oldData in processData)
                        {
                            if (IsImageLink(oldData.ResourceUrl))
                            {
                                await _cloudinaryService.DeleteImageByUrlAsync(oldData.ResourceUrl);
                            }
                            else
                            {
                                await _cloudinaryService.DeleteVideoByUrlAsync(oldData.ResourceUrl);
                            }
                            checkExistProcess.ProcessData.Remove(oldData);
                        }
                        foreach (var newData in updateProcessModel.ListUpdateProcessData)
                        {
                            var getLink = "";
                            if (IsImageFile(newData))
                            {
                                getLink = await _cloudinaryService.UploadImageAsync(newData, "process/data");
                            }
                            else
                            {
                                getLink = await _cloudinaryService.UploadVideoAsync(newData, "process/data");
                            }
                            checkExistProcess.ProcessData.Add(new ProcessData()
                            {
                                ResourceUrl = getLink,
                                CreateDate = DateTime.Now,
                                ProcessDataCode = NumberHelper.GenerateRandomCode("PCD")
                            });
                        }
                    }
                    checkExistProcess.UpdateDate = DateTime.Now;
                    if (updateProcessModel.ListUpdateSubProcess != null)
                    {
                        foreach (var subProcessRaw in updateProcessModel.ListUpdateSubProcess)
                        {
                            var subProcess = JsonConvert.DeserializeObject<UpdateSubProcessModel>(subProcessRaw);
                            var getListSubProcess = await _unitOfWork.SubProcessRepository.GetAllNoPaging();
                            var getListSubProcessOfProcess = getListSubProcess.Where(x => x.ProcessId == checkExistProcess.ProcessId);
                            foreach (var item in getListSubProcessOfProcess)
                            {
                                if(item.SubProcessId != subProcess.SubProcessId)
                                {
                                    return new BusinessResult(Const.FAIL_UPDATE_SUB_PROCESS_OF_PROCESS_CODE, Const.FAIL_UPDATE_SUB_PROCESS_OF_PROCESS_MESSAGE);
                                }
                            }
                            var subProcessUpdate = await _unitOfWork.SubProcessRepository.GetByCondition(x => x.SubProcessId == subProcess.SubProcessId, "ProcessData");
                            if (subProcessUpdate != null)
                            {   
                                if(subProcess.ParentSubProcessId != null)
                                {
                                    subProcessUpdate.ParentSubProcessId = subProcess.ParentSubProcessId;
                                }
                                if(subProcess.SubProcessName != null)
                                {
                                    subProcessUpdate.SubProcessName = subProcess.SubProcessName;
                                }
                                if(subProcess.IsDefault != null)
                                {
                                    subProcessUpdate.IsDefault = subProcess.IsDefault;
                                }
                                if(subProcess.IsActive != null)
                                {
                                    subProcessUpdate.IsActive = subProcess.IsActive;
                                }
                                if(subProcess.IsDeleted != null)
                                {
                                    subProcessUpdate.IsDeleted = subProcess.IsDeleted;
                                }
                                if(subProcess.ProcessStyleId != null)
                                {
                                    subProcessUpdate.ProcessStyleId = subProcess.ProcessStyleId;
                                }
                                subProcessUpdate.UpdateDate = DateTime.Now;
                            }
                        }
                    }
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_PROCESS_CODE, Const.SUCCESS_UPDATE_PROCESS_MESSAGE, checkExistProcess);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_PROCESS_CODE, Const.FAIL_UPDATE_PROCESS_MESSAGE, false);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
    }
        public bool IsImageFile(IFormFile file)
        {
            string[] validImageTypes = { "image/jpeg", "image/png", "image/gif" };
            string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            string contentType = file.ContentType.ToLower();
            string extension = Path.GetExtension(file.FileName)?.ToLower();

            return validImageTypes.Contains(contentType) && validImageExtensions.Contains(extension);
        }
        public bool IsImageLink(string url)
        {
            string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            return url.Contains("/image/") && validImageExtensions.Contains(Path.GetExtension(url).ToLower());
        }

        public async Task<BusinessResult> SoftDeleteProcess(int processId)
        {
            try
            {
                var checkExistProcess = await _unitOfWork.ProcessRepository.GetByID(processId);
                if(checkExistProcess != null)
                {
                    checkExistProcess.IsDeleted = true;
                    var result = await _unitOfWork.SaveAsync();
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_SOFT_DELETE_PROCESS_CODE, Const.SUCCESS_SOFT_DELETE_PROCESS_MESSAGE, result > 0);
                    }
                    return new BusinessResult(Const.FAIL_SOFT_DELETE_PROCESS_CODE, Const.FAIL_SOFT_DELETE_PROCESS_MESSAGE, false);

                }
                return new BusinessResult(Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_CODE, Const.WARNING_GET_PROCESS_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
