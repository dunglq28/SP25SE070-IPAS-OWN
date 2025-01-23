using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Common.Constants;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels;
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
    public class CriteriaTypeService : ICriteriaTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CriteriaTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BusinessResult> CreateCriteriaType(CreateCriteriaTypeModel createCriteriaTypeModel)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newCriteriaType = new CriteriaType()
                    {
                        CriteriaTypeCode = NumberHelper.GenerateRandomCode(CodeAliasEntityConst.CRITERIA_TYPE),
                        CriteriaTypeName = createCriteriaTypeModel.CriteriaTypeName,
                        GrowthStageID = createCriteriaTypeModel.GrowthStageID
                    };
                    await _unitOfWork.CriteriaTypeRepository.Insert(newCriteriaType);

                    foreach(var criteria in createCriteriaTypeModel.ListCritera)
                    {
                        var newCriteria = new Criteria()
                        {
                            CriteriaCode = NumberHelper.GenerateRandomCode("CTA"),
                            CriteriaDescription = criteria.CriteriaDescription,
                            CriteriaName = criteria.CriteriaName,
                            IsActive = true,
                            Priority = criteria.Priority,
                        };
                        newCriteriaType.Criteria.Add(newCriteria);
                    }

                    var checkInsertCriteriaType = await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();
                    if (checkInsertCriteriaType > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CRITERIA_TYPE_CODE, Const.SUCCESS_CREATE_CRITERIA_TYPE_MESSAGE, true);
                    }
                    return new BusinessResult(Const.FAIL_CREATE_CRITERIA_TYPE_CODE, Const.FAIL_CREATE_CRITERIA_TYPE_MESSAGE, false);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }

            }
        }

        public async Task<BusinessResult> GetCriteriaTypeByName(string criteriaTypeName)
        {
            try
            {
                var listCriteriaType = await _unitOfWork.CriteriaTypeRepository.GetCriteriaTypeByName(criteriaTypeName);
                if(listCriteriaType.Count() > 0)
                {
                    var listCriteriaTypeModel = _mapper.Map<List<CriteriaTypeModel>>(listCriteriaType);
                    return new BusinessResult(Const.SUCCESS_GET_CRITERIA_TYPE_BY_NAME_CODE, Const.SUCCESS_GET_CRITERIA_TYPE_BY_NAME_MESSAGE, listCriteriaTypeModel);
                }
                return new BusinessResult(Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> GetAllCriteriaTypePagination(PaginationParameter paginationParameter)
        {
            try
            {
                Expression<Func<CriteriaType, bool>> filter = null!;
                Func<IQueryable<CriteriaType>, IOrderedQueryable<CriteriaType>> orderBy = null!;
                if (!string.IsNullOrEmpty(paginationParameter.Search))
                {
                    int validInt = 0;
                    var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                    if (checkInt)
                    {
                        filter = x => x.CriteriaTypeId == validInt;
                    }
                    else
                    {
                        filter = x => x.CriteriaTypeCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.CriteriaTypeName.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.GrowthStage.GrowthStageName.ToLower().Contains(paginationParameter.Search.ToLower());
                    }
                }
                switch (paginationParameter.SortBy)
                {
                    case "criteriatypeid":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CriteriaTypeId)
                                   : x => x.OrderBy(x => x.CriteriaTypeId)) : x => x.OrderBy(x => x.CriteriaTypeId);
                        break;
                    case "criteriatypecode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CriteriaTypeCode)
                                   : x => x.OrderBy(x => x.CriteriaTypeCode)) : x => x.OrderBy(x => x.CriteriaTypeCode);
                        break;
                    case "criteriatypename":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CriteriaTypeName)
                                   : x => x.OrderBy(x => x.CriteriaTypeName)) : x => x.OrderBy(x => x.CriteriaTypeName);
                        break;
                    case "growthstagename":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.GrowthStage.GrowthStageName)
                                   : x => x.OrderBy(x => x.GrowthStage.GrowthStageName)) : x => x.OrderBy(x => x.GrowthStage.GrowthStageName);
                        break;
                    default:
                        orderBy = x => x.OrderBy(x => x.CriteriaTypeId);
                        break;
                }
                string includeProperties = "GrowthStage,Criteria";
                var entities = await _unitOfWork.CriteriaTypeRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
                var pagin = new PageEntity<CriteriaTypeModel>();
                pagin.List = _mapper.Map<IEnumerable<CriteriaTypeModel>>(entities).ToList();
                pagin.TotalRecord = await _unitOfWork.CriteriaTypeRepository.Count();
                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
                if (pagin.List.Any())
                {
                    return new BusinessResult(Const.SUCCESS_GET_ALL_CRITERIA_TYPE_CODE, Const.SUCCESS_GET_ALL_CRITERIA_TYPE_MESSAGE, pagin);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_MSG, new PageEntity<CriteriaTypeModel>());
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> GetCriteriaTypeByID(int criteriaTypeId)
        {
            try
            {
                var criteriaType = await _unitOfWork.CriteriaTypeRepository.GetByCondition(x => x.CriteriaTypeId == criteriaTypeId, "GrowthStage,Criteria");
                if(criteriaType != null)
                {
                    var result = _mapper.Map<CriteriaTypeModel>(criteriaType);
                    return new BusinessResult(Const.SUCCESS_GET_CRITERIA_TYPE_BY_ID_CODE, Const.SUCCESS_GET_CRITERIA_TYPE_BY_ID_MESSAGE, result);
                }
                return new BusinessResult(Const.FAIL_CREATE_CRITERIA_TYPE_CODE, Const.FAIL_CREATE_CRITERIA_TYPE_MESSAGE);

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> PermanentlyDeleteCriteriaType(int criteriaTypeId)
        {
            try
            {
                var checkExistCriteriaType = await _unitOfWork.CriteriaTypeRepository.GetByCondition(x => x.CriteriaTypeId == criteriaTypeId, "Criteria");
                if(checkExistCriteriaType != null)
                {
                    foreach(var criteria in checkExistCriteriaType.Criteria.ToList())
                    {
                        criteria.CriteriaTypeId = null;
                    }
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.CriteriaTypeRepository.Delete(checkExistCriteriaType);
                    var result = await _unitOfWork.SaveAsync();
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CRITERIA_TYPE_CODE, Const.SUCCESS_DELETE_CRITERIA_TYPE_MESSAGE, result > 0);
                    }
                    return new BusinessResult(Const.FAIL_DELETE_CRITERIA_TYPE_CODE, Const.FAIL_DELETE_CRITERIA_TYPE_MESSAGE, false);
                }
                return new BusinessResult(Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_MSG);

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> UpdateCriteriaTypeInfo(UpdateCriteriaTypeModel updateCriteriaTypeModel)
        {
            try
            {
                var checkExistCriteriaType = await _unitOfWork.CriteriaTypeRepository.GetByID(updateCriteriaTypeModel.CriteriaTypeId);
                if (checkExistCriteriaType != null)
                {
                    if (updateCriteriaTypeModel.CriteriaTypeName != null)
                    {
                        checkExistCriteriaType.CriteriaTypeName = updateCriteriaTypeModel.CriteriaTypeName;
                    }
                    if (updateCriteriaTypeModel.GrowthStageID != null)
                    {
                        checkExistCriteriaType.GrowthStageID = updateCriteriaTypeModel.GrowthStageID;
                    }
                    if(updateCriteriaTypeModel.ListUpdateCritera != null && updateCriteriaTypeModel.ListUpdateCritera.Count() > 0)
                    {
                        foreach(var criteria in updateCriteriaTypeModel.ListUpdateCritera)
                        {
                            var criteriaUpdate = await _unitOfWork.CriteriaRepository.GetByID(criteria.CriteriaId);
                            if(criteriaUpdate != null)
                            {
                                criteriaUpdate.CriteriaDescription = criteria.CriteriaDescription;
                                criteriaUpdate.CriteriaName = criteria.CriteriaName;
                                criteriaUpdate.Priority = criteria.Priority;
                                criteriaUpdate.IsActive = criteria.IsActive;
                            }
                        }
                    }
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CRITERIA_TYPE_CODE, Const.SUCCESS_UPDATE_CRITERIA_TYPE_MESSAGE, checkExistCriteriaType);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CRITERIA_TYPE_CODE, Const.FAIL_UPDATE_CRITERIA_TYPE_MESSAGE, false);
                    }

                }
                else
                {
                    return new BusinessResult(Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_CODE, Const.WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_MSG);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
