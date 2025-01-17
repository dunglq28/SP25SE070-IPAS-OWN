using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.UserBsModels;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUser(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _userService.GetAllUsers(paginationParameter);
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

        [HttpGet("get-user-by-id/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            try
            {

                var result = await _userService.GetUserById(userId);
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

        [HttpGet("get-user-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            try
            {
                var result = await _userService.GetUserByEmail(email);
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
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserInternal([FromBody] CreateAccountModel createAccountRequestModel)
        {
            try
            {
                var result = await _userService.CreateUser(createAccountRequestModel);
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

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel updateUserRequestModel)
        {
            try
            {
                var result = await _userService.UpdateUser(updateUserRequestModel);
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
        [HttpPut("banned-user/{userId}")]
        public async Task<IActionResult> BannedUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.BannedUser(userId);
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

        [HttpDelete("soft-delete-user/{userId}")]
        public async Task<IActionResult> SoftDeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.SoftDeleteUser(userId);
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

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
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

        [HttpPut("update-avatar/{userId}")]
        public async Task<IActionResult> UpdateAvatarOfUser(IFormFile avatarOfUser, [FromRoute] int userId)
        {
            try
            {
                var result = await _userService.UpdateAvatarOfUser(avatarOfUser, userId);
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

        [HttpGet("get-all-user-by-role/{roleName}")]
        public async Task<IActionResult> GetAllUserByRoleName([FromRoute] string roleName)
        {
            try
            {
                var result = await _userService.GetAllUsersByRole(roleName);
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
