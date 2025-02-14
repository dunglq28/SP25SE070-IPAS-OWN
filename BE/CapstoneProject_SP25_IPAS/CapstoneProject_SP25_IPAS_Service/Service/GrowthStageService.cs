﻿using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Common.Constants;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.GrowthStageModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PlantLotModel;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class GrowthStageService : IGrowthStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GrowthStageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BusinessResult> CreateGrowthStage(CreateGrowthStageModel createGrowthStageModel)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var createGrowthStage = new GrowthStage()
                    {
                        GrowthStageCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.GROWTHSTAGE),
                        GrowthStageName = createGrowthStageModel.GrowthStageName,
                        MonthAgeStart = createGrowthStageModel.MonthAgeStart,
                        MonthAgeEnd = createGrowthStageModel.MonthAgeEnd
                    };
                    await _unitOfWork.GrowthStageRepository.Insert(createGrowthStage);
                    var result = await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_GROWTHSTAGE_CODE, Const.SUCCESS_CREATE_GROWTHSTAGE_MESSAGE, result > 0);
                    }
                    return new BusinessResult(Const.FAIL_CREATE_GROWTHSTAGE_CODE, Const.FAIL_CREATE_GROWTHSTAGE_MESSAGE, false);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

        public async Task<BusinessResult> GetAllGrowthStagePagination(PaginationParameter paginationParameter)
        {
            try
            {
                Expression<Func<GrowthStage, bool>> filter = null!;
                Func<IQueryable<GrowthStage>, IOrderedQueryable<GrowthStage>> orderBy = null!;
                if (!string.IsNullOrEmpty(paginationParameter.Search))
                {
                    int validInt = 0;
                    var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                    DateTime validDate = DateTime.Now;
                    if (checkInt)
                    {
                        filter = x => x.GrowthStageId == validInt;
                    }
                    else if (DateTime.TryParse(paginationParameter.Search, out validDate))
                    {
                        filter = x => x.MonthAgeStart == validDate || x.MonthAgeEnd == validDate;
                    }
                    else
                    {
                        filter = x => x.GrowthStageCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.GrowthStageName.ToLower().Contains(paginationParameter.Search.ToLower());
                    }
                }
                switch (paginationParameter.SortBy)
                {
                    case "growthstageid":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.GrowthStageId)
                                   : x => x.OrderBy(x => x.GrowthStageId)) : x => x.OrderBy(x => x.GrowthStageId);
                        break;
                    case "growthstagecode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.GrowthStageCode)
                                   : x => x.OrderBy(x => x.GrowthStageCode)) : x => x.OrderBy(x => x.GrowthStageCode);
                        break;
                    case "growthstagename":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.GrowthStageName)
                                   : x => x.OrderBy(x => x.GrowthStageName)) : x => x.OrderBy(x => x.GrowthStageName);
                        break;
                    case "monthagestart":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.MonthAgeStart)
                                   : x => x.OrderBy(x => x.MonthAgeStart)) : x => x.OrderBy(x => x.MonthAgeStart);
                        break;
                    case "monthageend":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.MonthAgeEnd)
                                   : x => x.OrderBy(x => x.MonthAgeEnd)) : x => x.OrderBy(x => x.MonthAgeEnd);
                        break;
                    default:
                        orderBy = x => x.OrderBy(x => x.GrowthStageId);
                        break;
                }
                string includeProperties = "";
                var entities = await _unitOfWork.GrowthStageRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
                var pagin = new PageEntity<GrowthStageModel>();
                pagin.List = _mapper.Map<IEnumerable<GrowthStageModel>>(entities).ToList();
                pagin.TotalRecord = await _unitOfWork.GrowthStageRepository.Count();
                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
                if (pagin.List.Any())
                {
                    return new BusinessResult(Const.SUCCESS_GET_ALL_GROWTHSTAGE_CODE, Const.SUCCESS_GET_ALL_GROWTHSTAGE_MESSAGE, pagin);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_MSG, new PageEntity<GrowthStageModel>());
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> GetGrowthStageByID(int growthStageId)
        {
            try
            {
                var getGrowthStage = await _unitOfWork.GrowthStageRepository.GetByID(growthStageId);
                if(getGrowthStage != null)
                {
                    return new BusinessResult(Const.SUCCESS_GET_GROWTHSTAGE_BY_ID_CODE, Const.SUCCESS_GET_GROWTHSTAGE_BY_ID_MESSAGE, getGrowthStage);
                }
                return new BusinessResult(Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> PermanentlyDeleteGrowthStage(int growthStageId)
        {
            try 
            { 
                string includeProperties = "CriteriaTypes,Plans,Processes";
                var getDeleteGrowthStage = await _unitOfWork.GrowthStageRepository.GetByCondition(x => x.GrowthStageId == growthStageId, includeProperties);
                if(getDeleteGrowthStage != null)
                {
                    foreach(var criteriaType in getDeleteGrowthStage.CriteriaTypes.ToList())
                    {
                        criteriaType.GrowthStageID = null;
                    }
                    foreach(var plan in getDeleteGrowthStage.Plans.ToList())
                    {
                        plan.GrowthStageID = null;
                    }
                    foreach (var process in getDeleteGrowthStage.Processes.ToList())
                    {
                        process.GrowthStageID = null;
                    }
                    await _unitOfWork.SaveAsync();
                     _unitOfWork.GrowthStageRepository.Delete(getDeleteGrowthStage);
                    var result = await _unitOfWork.SaveAsync();
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_GROWTHSTAGE_CODE, Const.SUCCESS_DELETE_GROWTHSTAGE_MESSAGE, result > 0);
                    }
                    return new BusinessResult(Const.FAIL_DELETE_GROWTHSTAGE_CODE, Const.FAIL_DELETE_GROWTHSTAGE_MESSAGE, false);
                }
                return new BusinessResult(Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> UpdateGrowthStageInfo(UpdateGrowthStageModel updateriteriaTypeModel)
        {
            try
            {
                var checkExistGrowthStage = await _unitOfWork.GrowthStageRepository.GetByID(updateriteriaTypeModel.GrowthStageId);
                if(checkExistGrowthStage != null)
                {
                    if(updateriteriaTypeModel.GrowthStageName != null)
                    {
                        checkExistGrowthStage.GrowthStageName = updateriteriaTypeModel.GrowthStageName;
                    }
                    if(updateriteriaTypeModel.MonthAgeStart != null)
                    {
                        checkExistGrowthStage.MonthAgeStart = updateriteriaTypeModel.MonthAgeStart;
                    }
                    if(updateriteriaTypeModel.MonthAgeEnd != null)
                    {
                        checkExistGrowthStage.MonthAgeEnd = updateriteriaTypeModel.MonthAgeEnd;
                    }
                    var result = await _unitOfWork.SaveAsync();
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_GROWTHSTAGE_CODE, Const.SUCCESS_UPDATE_GROWTHSTAGE_MESSAGE, checkExistGrowthStage);
                    }
                    return new BusinessResult(Const.FAIL_UPDATE_GROWTHSTAGE_CODE, Const.FAIL_UPDATE_GROWTHSTAGE_MESSAGE, false);
                }
                return new BusinessResult(Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
