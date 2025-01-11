using CapstoneProject_SP25_IPAS_API.Payloads.Request;
using CapstoneProject_SP25_IPAS_API.Payloads.Response;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.AuthensModel;
using CapstoneProject_SP25_IPAS_Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject_SP25_IPAS_API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount(SignUpModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _userService.RegisterAsync(model);
                    return Ok(result);
                }
                return ValidationProblem(ModelState);
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmail([FromBody] AccountRequestModel accountRequestModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _userService.LoginByEmailAndPassword(accountRequestModel.Email, accountRequestModel.Password);
                    return Ok(result);  
                }
                else
                {
                    return ValidationProblem(ModelState);
                }
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

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel logoutModel)
        {
            try
            {
                var result = await _userService.Logout(logoutModel.RefreshToken);
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
