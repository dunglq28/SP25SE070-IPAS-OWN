using CapstoneProject_SP25_IPAS_API.Payload;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriaTypeController : ControllerBase
    {
        private readonly ICriteriaTypeService _criteriaTypeService;

        public CriteriaTypeController(ICriteriaTypeService criteriaTypeService)
        {
            _criteriaTypeService = criteriaTypeService;
        }

        [HttpGet(APIRoutes.CriteriaType.getCriteriaTypeWithPagination, Name = "getCriteriaTypeWithPagination")]
        public async Task<IActionResult> GetAllCriteriaType(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _criteriaTypeService.GetAllCriteriaTypePagination(paginationParameter);
                return Ok(result);
            }
            catch (Exception ex)
            {

                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet(APIRoutes.CriteriaType.getCriteriaTypeById, Name = "getCriteriaTypeById")]
        public async Task<IActionResult> GetCriteriaTypeById([FromRoute] int id)
        {
            try
            {
                var result = await _criteriaTypeService.GetCriteriaTypeByID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpGet(APIRoutes.CriteriaType.getCriteriaTypeByName, Name = "getCriteriaTypeByName")]
        public async Task<IActionResult> GetCriteriaTypeByName([FromRoute] string name)
        {
            try
            {
                var result = await _criteriaTypeService.GetCriteriaTypeByName(name);
                return Ok(result);
            }
            catch (Exception ex)
            {

                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPost(APIRoutes.CriteriaType.createCriteriaType, Name = "createCriteriaType")]
        public async Task<IActionResult> CreateCriteriaType(CreateCriteriaTypeModel createCriteriaTypeModel)
        {
            try
            {
                var result = await _criteriaTypeService.CreateCriteriaType(createCriteriaTypeModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpPut(APIRoutes.CriteriaType.updateCriteriaTypeInfo, Name = "updateCriteriaType")]
        public async Task<IActionResult> UpdateCriteriaType(UpdateCriteriaTypeModel updateCriteriaTypeModel)
        {
            try
            {
                var result = await _criteriaTypeService.UpdateCriteriaTypeInfo(updateCriteriaTypeModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

        [HttpDelete(APIRoutes.CriteriaType.permanenlyDelete, Name = "permentlyDeleteCriteriaType")]
        public async Task<IActionResult> DeleteCriteriaType([FromRoute] int id)
        {
            try
            {
                var result = await _criteriaTypeService.PermanentlyDeleteCriteriaType(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
                return BadRequest(response);
            }
        }

    }
}
