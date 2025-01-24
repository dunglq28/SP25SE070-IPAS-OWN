using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest.PlantCriteriaRequest;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class PlantCriteriaService : IPlantCriteriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlantCriteriaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BusinessResult> ApplyCriteriasForPlants(PlantCriteriaCreateRequest plantCriteriaCreateRequest)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    if (plantCriteriaCreateRequest.PlantIds.IsNullOrEmpty())
                        return new BusinessResult(Const.FAIL_PLANTS_REQUEST_EMPTY_CODE, Const.FAIL_PLANT_REQUEST_EMPTY_MSG);
                    if (plantCriteriaCreateRequest.CriteriaData.IsNullOrEmpty())
                        return new BusinessResult(Const.FAIL_CRITERIA_REQUEST_EMPTY_CODE, Const.FAIL_CRITERIA_REQUEST_EMPTY_MSG);
                    var plantCriteriaList = new List<PlantCriteria>();

                    foreach (var plantId in plantCriteriaCreateRequest.PlantIds)
                    {
                        foreach (var criteria in plantCriteriaCreateRequest.CriteriaData)
                        {
                            plantCriteriaList.Add(new PlantCriteria
                            {
                                PlantId = plantId,
                                CriteriaId = criteria.CriteriaId,
                                IsChecked = criteria.IsChecked
                            });
                        }
                    }

                    await _unitOfWork.PlantCriteriaRepository.InsertRangeAsync(plantCriteriaList);
                    var resultSave = await _unitOfWork.SaveAsync();
                    if (resultSave > 0)
                    {
                        await _unitOfWork.CommitAsync();
                        return new BusinessResult(Const.FAIL_APPLY_LIST_CRITERIA_PLANTS_CODE, Const.FAIL_APPLY_LIST_CRITERIA_PLANTS_MSG, new { success = false });
                    }
                    return new BusinessResult();
                }
                catch (Exception ex)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

        public async Task<BusinessResult> DeleteCriteriaForPlant(int plantId, int criteriaId)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    if (criteriaId <= 0)
                    {
                        return new BusinessResult();
                    }
                    if (plantId <= 0)
                    {
                        return new BusinessResult();
                    }
                    return new BusinessResult();
                    //var plantcriteria
                }
                catch (Exception ex)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

        public Task<BusinessResult> CheckCriteriaForPlant(CheckPlantCriteriaRequest checkPlantCriteriaRequest)
        {
            throw new NotImplementedException();
        }

    }
}
