using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.PlantLotModel;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Payloads.Request;
using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantLotController : ControllerBase
    {
        private readonly IPlantLotService _plantLotService;

        public PlantLotController(IPlantLotService plantLotService)
        {
            _plantLotService = plantLotService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPlantLot(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _plantLotService.GetAllPlantLots(paginationParameter);
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

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetPlantLotById([FromRoute] int id)
        {
            try
            {
                var result = await _plantLotService.GetPlantLotById(id);
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

        [HttpPost("create")]
        public async Task<IActionResult> CreatePlantLot([FromBody] CreatePlantLotModel createPlantLotModel)
        {
            try
            {
                var result = await _plantLotService.CreatePlantLot(createPlantLotModel);
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

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePlantLot([FromBody] UpdatePlantLotModel updatePlantLotModel)
        {
            try
            {
                var result = await _plantLotService.UpdatePlantLot(updatePlantLotModel);
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
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeletePlantLot([FromRoute] int id)
        {
            try
            {
                var result = await _plantLotService.DeletePlantLot(id);
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

        [HttpPost("create-many-plant")]
        public async Task<IActionResult> CreateManyPlant([FromBody] List<CriteriaForPlantLotRequestModel> criteriaForPlantLotRequestModels, [FromQuery] int quantity)
        {
            try
            {
                var result = await _plantLotService.CreateManyPlant(criteriaForPlantLotRequestModels, quantity);
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
