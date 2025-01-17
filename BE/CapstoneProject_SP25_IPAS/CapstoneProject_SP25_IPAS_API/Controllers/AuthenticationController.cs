﻿using CapstoneProject_SP25_IPAS_Service.Payloads.Response;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.AuthensModel;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Payloads.Request;
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

        [HttpPost("register/send-otp")]
        public async Task<IActionResult> RegisterSendOTPAccount(EmailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.RegisterSendMailAsync(model.Email);
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

        [HttpPost("register/verify-otp")]
        public IActionResult RegisterVerifyOTPAccount(VerifyOtpRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _userService.VerifyOtpRegisterAsync(model.Email, model.Otp);
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
        public async Task<IActionResult> Logout([FromBody] RefreshTokenModel removeRefreshTokenModel)
        {
            try
            {
                var result = await _userService.Logout(removeRefreshTokenModel.RefreshToken);
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel removeRefreshTokenModel)
        {
            try
            {
                var result = await _userService.RefreshToken(removeRefreshTokenModel.RefreshToken); 
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

        [HttpPost("forget-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] EmailModel emailModel) 
        {
            try
            {
                var result = await _userService.RequestResetPassword(emailModel.Email);
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

        [HttpPost("forget-password/confirm")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmOtpModel confirmOtpModel)
        {
            try
            {
                var result = await _userService.ConfirmResetPassword(confirmOtpModel);
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

        [HttpPost("forget-password/new-password")]
        public async Task<IActionResult> NewPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var result = await _userService.ExecuteResetPassword(resetPasswordModel);
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
