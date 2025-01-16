using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_BussinessObject.RequestModel.FarmRequest;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Common.ObjectStatus;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.IService;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class FarmService : IFarmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FarmService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BusinessResult> CreateFarm(FarmCreateModel farmCreateModel)
        {
            try
            {

                using (var transaction = _unitOfWork.BeginTransactionAsync())
                {

                    var farmCreateEntity = new Farm()
                    {
                        FarmName = farmCreateModel.FarmName,
                        Address = farmCreateModel.Address,
                        Area = farmCreateModel.Area,
                        SoilType = farmCreateModel.SoilType,
                        ClimateZone = farmCreateModel.ClimateZone,
                        Province = farmCreateModel.Province,
                        Ward = farmCreateModel.Ward,
                        District = farmCreateModel.District,
                        Length = farmCreateModel.Length,
                        Width = farmCreateModel.Width,
                        Description = farmCreateModel.Description
                    };
                    farmCreateEntity.FarmCode = "FARM_123456"; // cho ham setup code
                    farmCreateEntity.Status = FarmStatus.Active.ToString();
                    farmCreateEntity.CreateDate = DateTime.Now;
                    farmCreateEntity.UpdateDate = DateTime.Now;
                    if (farmCreateModel.LogoUrl != null)
                    {
                        // gan va push len cloudinary
                        farmCreateEntity.LogoUrl = "123456";
                    }

                    if (!farmCreateModel.FarmCoordinations.IsNullOrEmpty())
                    {
                        foreach (var coordination in farmCreateModel.FarmCoordinations)
                        {
                            var farmCoordination = new FarmCoordination()
                            {
                                Lagtitude = coordination.Lagtitude,
                                Longitude = coordination.Longitude,
                            };
                            farmCreateEntity.FarmCoordinations.Add(farmCoordination);
                        }
                    }
                    // chua co add them UserFarm

                    await _unitOfWork.FarmRepository.Insert(farmCreateEntity);
                    int result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        await _unitOfWork.CommitAsync();
                        return new BusinessResult(Const.SUCCESS_CREATE_FARM_CODE, Const.SUCCESS_CREATE_FARM_MSG, farmCreateEntity);
                    }
                    else return new BusinessResult(Const.FAIL_CREATE_FARM_CODE, Const.FAIL_CREATE_FARM_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_CREATE_FARM_CODE, Const.FAIL_CREATE_FARM_MSG, ex.Message);
            }
        }

        public async Task<BusinessResult> GetFarmByID(int farmId)
        {
            Expression<Func<Farm, bool>> filter = x => x.FarmId == farmId && x.IsDelete != true;
            var farm = await _unitOfWork.FarmRepository.GetByCondition(filter);
            if (farm == null)
                return new BusinessResult(Const.FAIL_GET_FARM_NOT_EXIST_CODE, Const.FAIL_GET_FARM_NOT_EXIST_MSG);
            return new BusinessResult(Const.SUCCESS_GET_FARM_CODE, Const.SUCCESS_FARM_GET_MSG, farm);
        }

        public async Task<BusinessResult> GetAllFarmOfUser(int userId)
        {
            throw new Exception();
        }


        public Task<BusinessResult> GetFarmPagination()
        {
            //         Expression<Func<Farm, bool>> filter = !string.IsNullOrEmpty(searchKey)
            //? x => (x.FarmName!.ToLower().Contains(searchKey.ToLower())
            //        || x.Address!.ToLower().Contains(searchKey.ToLower()))
            //        && x.IsDelete != false
            //: x => x.IsDelete != false;

            //         Func<IQueryable<Farm>, IOrderedQueryable<Farm>> orderBy = q => q.OrderBy(x => x.PlayFieldName);

            //         string includePropoperties = "FarmCoordination";

            //         var entities = await _unitOfWork.PlayFieldRepository.Get(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize, includeProperties: includePropoperties);
            //         var pagin = new PageEntity<PlayFieldModel>();
            //         pagin.List = _mapper.Map<IEnumerable<PlayFieldModel>>(entities);
            //         pagin.TotalRecord = await _unitOfWork.PlayFieldRepository.Count();
            //         pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, pageSize!.Value);
            //         return pagin;
            throw new Exception();
        }

        public async Task<BusinessResult> permanentlyDeleteFarm(int farmId)
        {
            try
            {
                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    Expression<Func<Farm, bool>> filter = x => x.FarmId == farmId;
                    string includeProperties = "FarmCoordinations,LandPlot,Order,Process"; // chua co bang user farm nen chua xoa
                    // xoa anh tren cloudinary
                    // set up them trong context moi xoa dc tat ca 1 lan
                    var farm = await _unitOfWork.FarmRepository.GetByCondition(filter: filter, includeProperties: includeProperties);
                    if (farm == null) return new BusinessResult(Const.FAIL_GET_FARM_NOT_EXIST_CODE, Const.FAIL_GET_FARM_NOT_EXIST_MSG);

                    _unitOfWork.FarmRepository.Delete(farm);
                    int result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        await _unitOfWork.CommitAsync();
                        return new BusinessResult(Const.SUCCESS_UPDATE_FARM_CODE, Const.SUCCESS_UPDATE_FARM_MSG, new { success = true });
                    }
                    else return new BusinessResult(Const.FAIL_UPDATE_FARM_CODE, Const.FAIL_UPDATE_FARM_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


        public async Task<BusinessResult> SoftDeletedFarm(int farmId)
        {
            try
            {

                using (var transaction = _unitOfWork.BeginTransactionAsync())
                {
                    Expression<Func<Farm, bool>> condition = x => x.FarmId == farmId && x.IsDelete != true;
                    var farm = await _unitOfWork.FarmRepository.GetByCondition(condition);

                    if (farm == null)
                    {
                        return new BusinessResult(Const.FAIL_GET_FARM_NOT_EXIST_CODE, Const.FAIL_GET_FARM_NOT_EXIST_MSG);
                    }
                    farm.IsDelete = true;
                    _unitOfWork.FarmRepository.Update(farm);
                    int result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        await _unitOfWork.CommitAsync();
                        return new BusinessResult(Const.SUCCESS_DELETE_SOFTED_FARM_CODE, Const.SUCCESS_DELETE_SOFTED_FARM_MSG, farm);
                    }
                    else return new BusinessResult(Const.SUCCESS_DELETE_SOFTED_FARM_CODE, Const.SUCCESS_DELETE_SOFTED_FARM_MSG, new { success = false });
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> UpdateFarmInfo(FarmUpdateModel farmUpdateModel)
        {
            try
            {

                using (var transaction = _unitOfWork.BeginTransactionAsync())
                {
                    Expression<Func<Farm, bool>> condition = x => x.FarmId == farmUpdateModel.FarmId && x.IsDelete != true;
                    var farmEntityUpdate = await _unitOfWork.FarmRepository.GetByCondition(condition);

                    if (farmEntityUpdate == null)
                    {
                        return new BusinessResult(Const.FAIL_GET_FARM_NOT_EXIST_CODE, Const.FAIL_GET_FARM_NOT_EXIST_MSG);
                    }

                    farmEntityUpdate.FarmName = farmUpdateModel.FarmName;
                    farmEntityUpdate.Address = farmUpdateModel.Address;
                    farmEntityUpdate.Area = farmUpdateModel.Area;
                    farmEntityUpdate.SoilType = farmUpdateModel.SoilType;
                    farmEntityUpdate.ClimateZone = farmUpdateModel.ClimateZone;
                    farmEntityUpdate.Province = farmUpdateModel.Province;
                    farmEntityUpdate.Ward = farmUpdateModel.Ward;
                    farmEntityUpdate.District = farmUpdateModel.District;
                    farmEntityUpdate.Length = farmUpdateModel.Length;
                    farmEntityUpdate.Width = farmUpdateModel.Width;
                    farmEntityUpdate.Description = farmUpdateModel.Description;
                    farmEntityUpdate.UpdateDate = DateTime.Now;


                    _unitOfWork.FarmRepository.Update(farmEntityUpdate);
                    int result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        await _unitOfWork.CommitAsync();
                        return new BusinessResult(Const.SUCCESS_UPDATE_FARM_CODE, Const.SUCCESS_UPDATE_FARM_MSG, farmEntityUpdate);
                    }
                    else return new BusinessResult(Const.FAIL_UPDATE_FARM_CODE, Const.FAIL_UPDATE_FARM_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
