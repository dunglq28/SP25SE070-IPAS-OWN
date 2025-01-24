using CapstoneProject_SP25_IPAS_API.Payload;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.GrowthStageModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.ProcessStyleModel;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using CapstoneProject_SP25_IPAS_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessStyleController : ControllerBase
    {
        private readonly IProcessStyleService _processStyleService;

        public ProcessStyleController(IProcessStyleService processStyleService)
        {
            _processStyleService = processStyleService;
        }

        [HttpGet(APIRoutes.ProcessStyle.getProcessStyleWithPagination, Name = "getAllProcessStylePaginationAsync")]
        public async Task<IActionResult> GetAllProcessStyle(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _processStyleService.GetAllProcessStylePagination(paginationParameter);
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

        [HttpGet(APIRoutes.ProcessStyle.getProcessStyleById, Name = "getProcessStyleByIdAsync")]
        public async Task<IActionResult> GetProcessStyleById([FromRoute] int id)
        {
            try
            {
                var result = await _processStyleService.GetProcessStyleByID(id);
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

        [HttpPost(APIRoutes.ProcessStyle.createProcessStyle, Name = "createProcessStyleAsync")]
        public async Task<IActionResult> CreateProcessStyle([FromBody] CreateProcessStyleModel createProcessStyleModel)
        {
            try
            {
                var result = await _processStyleService.CreateProcessStyle(createProcessStyleModel);
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

        [HttpPut(APIRoutes.ProcessStyle.updateProcessStyleInfo, Name = "updateProcessStyleAsync")]
        public async Task<IActionResult> UpdateProcessStyle([FromBody] UpdateProcessStyleModel updateProcessStyleModel)
        {
            try
            {
                var result = await _processStyleService.UpdateProcessStyleInfo(updateProcessStyleModel);
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

        [HttpDelete(APIRoutes.ProcessStyle.permanenlyDelete, Name = "permenlyDeleteProcessStyle")]
        public async Task<IActionResult> DeletProcessStyle([FromRoute] int id)
        {
            try
            {
                var result = await _processStyleService.PermanentlyDeleteProcessStyle(id);
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
