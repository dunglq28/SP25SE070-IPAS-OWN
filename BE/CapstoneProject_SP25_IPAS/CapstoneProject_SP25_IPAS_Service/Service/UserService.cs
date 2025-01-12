using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Common;
using CapstoneProject_SP25_IPAS_Common.Mail;
using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.Base;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.AuthensModel;
using CapstoneProject_SP25_IPAS_Service.BusinessModel.UserBsModels;
using CapstoneProject_SP25_IPAS_Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService mailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<BusinessResult> BannedUser(int userId)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (existUser != null && !existUser.Status.ToLower().Equals("Banned".ToLower()))
            {
                existUser.Status = "Banned";
                var result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_BANNED_USER_CODE, Const.SUCCESS_BANNED_USER_MSG);
                }
                return new BusinessResult(Const.FAIL_BANNED_USER_CODE, Const.FAIL_BANNED_USER_MSG);
            }
            return new BusinessResult(Const.WARNING_BANNED_USER_CODE, Const.WARNING_BANNED_USER_MSG);

        }

        public async Task<BusinessResult> ConfirmResetPassword(ConfirmOtpModel confirmOtpModel)
        {
            try
            {
                var checkExistUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(confirmOtpModel.Email);
                if(checkExistUser != null && checkExistUser.Otp != null)
                {
                    if(checkExistUser.Otp.Equals(confirmOtpModel.OtpCode) && checkExistUser.ExpiredOtpTime >= DateTime.Now)
                    {
                        return new BusinessResult(Const.SUCCESS_CONFIRM_RESET_PASSWORD_CODE, Const.SUCCESS_CONFIRM_RESET_PASSWORD_MESSAGE, true);
                    }
                    return new BusinessResult(Const.FAIL_CONFIRM_RESET_PASSWORD_CODE, Const.FAIL_CONFIRM_RESET_PASSWORD_MESSAGE, false);
                }
                return new BusinessResult(Const.WARNING_SIGN_IN_CODE, Const.WARNING_SIGN_IN_MSG, false);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message, false);
            }
        }

        public Task<bool> CreateUser(CreateAccountModel createAccountModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessResult> ExecuteResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var checkUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(resetPasswordModel.Email);
                if(checkUser != null && checkUser.Otp.Equals(resetPasswordModel.OtpCode))
                {
                    checkUser.Password = PasswordHelper.HashPassword(resetPasswordModel.NewPassword);
                    var result = await _unitOfWork.UserRepository.UpdateUserAsync(checkUser);
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_RESET_PASSWORD_CODE, Const.SUCCESS_RESET_PASSWORD_MSG, true);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_RESET_PASSWORD_CODE, Const.FAIL_RESET_PASSWORD_MSG, false);
                    }

                }
                return new BusinessResult(Const.WARNING_RESET_PASSWORD_CODE, Const.WARNING_RESET_PASSWORD_MSG, false);
             }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message, false);
            }
        }

        public Task<List<User>> GetAllUsersByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessResult> GetUserByEmail(string email)
        {
            var getUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if(getUser != null)
            {
                var result =  _mapper.Map<UserModel>(getUser);
                return new BusinessResult(Const.SUCCESS_GET_USER_CODE, Const.SUCCESS_GET_USER_BY_EMAIL_MSG, result);
            }
            return new BusinessResult(Const.FAIL_GET_USER_CODE, Const.FAIL_GET_USER_BY_EMAIL_MSG, null);
        }

        public async Task<BusinessResult> GetUserById(int userId)
        {
            var getUser = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if(getUser != null)
            {
                var result = _mapper.Map<UserModel>(getUser);
                return new BusinessResult(Const.SUCCESS_GET_USER_CODE, Const.FAIL_GET_USER_BY_ID_MSG, result);
            }
            return new BusinessResult(Const.FAIL_GET_USER_CODE, Const.FAIL_GET_USER_BY_ID_MSG, null);
        }

        public async Task<BusinessResult> LoginByEmailAndPassword(string email, string password)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                    if (existUser == null)
                    {
                        return new BusinessResult(Const.WARNING_SIGN_IN_CODE, Const.WARNING_SIGN_IN_MSG);
                    }

                    var verifyPassword = PasswordHelper.VerifyPassword(password, existUser.Password);
                    if (verifyPassword || existUser.Password == null)
                    {
                        if (existUser.Status.ToLower().Equals("Banned".ToLower()) || existUser.IsDelete == true)
                        {
                            return new BusinessResult(Const.WARNING_ACCOUNT_BANNED_CODE, Const.WARNING_ACCOUNT_BANNED_MSG);
                        }
                        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
                        string accessToken = await GenerateAccessToken(email, existUser);

                        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInDays);
                        string refreshToken = await GenerateRefreshToken(email, null, tokenValidityInDays);


                        await _unitOfWork.RefreshTokenRepository.AddRefreshToken(new RefreshToken()
                        {
                            UserId = existUser.UserId,
                            RefreshTokenCode = "IPAS-" + "RFT-" + NumberHelper.GenerateRandomByDate(),
                            RefreshTokenValue = refreshToken,
                            CreateDate = DateTime.Now,
                            ExpiredDate = DateTime.Now.AddDays(tokenValidityInDays)
                        });
                        await transaction.CommitAsync();
                        return new BusinessResult(Const.SUCCESS_LOGIN_CODE, Const.SUCCESS_LOGIN_MSG, new AuthenModel()
                        {
                            AccessToken = accessToken,
                            RefreshToken = refreshToken,
                        });
                    }
                    else
                    {
                        return new BusinessResult(Const.WARNING_PASSWORD_INCORRECT_CODE, Const.WARNING__PASSWORD_INCORRECT_MSG);
                    }
                }
                catch (Exception ex)
                {

                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.FAIL_CREATE_CODE, ex.Message);
                }
            }
        }

        public async Task<BusinessResult> Logout(string refreshToken)
        {
            var checkExistRefreshToken = await _unitOfWork.RefreshTokenRepository.GetRefrshTokenByRefreshTokenValue(refreshToken);
            if (checkExistRefreshToken != null)
            {
                var result = await _unitOfWork.RefreshTokenRepository.DeleteToken(refreshToken);
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_LOGOUT_CODE, Const.SUCCESS_LOGOUT_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_LOGOUT_CODE, Const.FAIL_LOGOUT_MSG);
                }
            }
            return new BusinessResult(Const.WARNING_RFT_NOT_EXIST_CODE, Const.WARNING_RFT_NOT_EXIST_MSG);
        }

        public async Task<BusinessResult> RefreshToken(string jwtToken)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authSigningKey,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            try
            {
                SecurityToken validatedToken;
                var principal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);
                var email = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                if (email != null)
                {
                    if (principal != null)
                    {
                        var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                        if (existUser != null)
                        {
                            var checkExistRefreshToken = await _unitOfWork.RefreshTokenRepository.GetRefrshTokenByRefreshTokenValue(jwtToken);
                            if (checkExistRefreshToken == null)
                            {
                                return new BusinessResult(Const.WARNING_RFT_NOT_EXIST_CODE, Const.WARNING_RFT_NOT_EXIST_MSG);

                            }
                            else
                            {
                                if (checkExistRefreshToken.ExpiredDate >= DateTime.Now)
                                {
                                    var newAccessToken = await GenerateAccessToken(email, existUser);
                                    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int newTokenValidityInMinutes);

                                    var newRefreshToken = await GenerateRefreshToken(email, checkExistRefreshToken.ExpiredDate, 0);
                                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInDays);

                                    await _unitOfWork.RefreshTokenRepository.AddRefreshToken(new RefreshToken()
                                    {
                                        UserId = checkExistRefreshToken.UserId,
                                        RefreshTokenCode = "IPAS-" + "RFT-" + NumberHelper.GenerateRandomByDate(),
                                        RefreshTokenValue = newRefreshToken,
                                        CreateDate = DateTime.Now,
                                        ExpiredDate = checkExistRefreshToken.ExpiredDate
                                    });
                                    return new BusinessResult(Const.SUCCESS_RFT_CODE, Const.SUCCESS_RFT_MSG, new AuthenModel
                                    {
                                        AccessToken = newAccessToken,
                                        RefreshToken = newRefreshToken
                                    });

                                }
                                else
                                {
                                    await _unitOfWork.RefreshTokenRepository.DeleteToken(jwtToken);
                                    return new BusinessResult(Const.WARNING_INVALID_REFRESH_TOKEN_CODE, Const.WARNING_INVALID_REFRESH_TOKEN_MSG);
                                }
                            }
                        }
                    }
                }
                return new BusinessResult(Const.WARNING_SIGN_IN_CODE, Const.WARNING_SIGN_IN_MSG);
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<BusinessResult> RegisterAsync(SignUpModel model)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new User()
                    {
                        Email = model.Email,
                        UserCode = "IPAS-" + "USR-" + NumberHelper.GenerateRandomByDate(),
                        FullName = model.FullName,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        AvatarURL = model.Avatar ?? "",
                        Status = "Active",
                        IsDelete = false,
                    };

                    var checkExistUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(model.Email);
                    if (checkExistUser != null)
                    {
                        return new BusinessResult(Const.WARNING_ACCOUNT_IS_EXISTED_CODE, Const.WARNING_ACCOUNT_IS_EXISTED_MSG);
                    }
                    if (model.Password != null)
                    {
                        newUser.Password = PasswordHelper.HashPassword(model.Password);
                    }
                    var role = await _unitOfWork.RoleRepository.GetRoleByName(model.Role.ToString());
                    if (role != null)
                    {
                        newUser.RoleId = role.RoleId;
                    }
                    else
                    {
                        return new BusinessResult(Const.WARNING_ROLE_IS_NOT_EXISTED_CODE, Const.WARNING_ROLE_IS_NOT_EXISTED_MSG);
                    }
                    await _unitOfWork.UserRepository.AddUserAsync(newUser);
                    await transaction.CommitAsync();
                    return new BusinessResult(Const.SUCCESS_REGISTER_CODE, Const.SUCCESS_REGISTER_MSG);

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
                }
            }
        }

        public async Task<BusinessResult> RequestResetPassword(string email)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (existUser != null)
            {
                if(existUser.Status.ToLower() == "Active".ToLower() && existUser.IsDelete == false)
                {
                    bool checkSendOtp = await CreateOtpAsync(email);
                    return new BusinessResult(Const.SUCCESS_SEND_OTP_RESET_PASSWORD_CODE, Const.SUCCESS_SEND_OTP_RESET_PASSWORD_USER_MSG, true);
                }
            }
            return new BusinessResult(Const.WARNING_SIGN_IN_CODE, Const.WARNING_SIGN_IN_MSG, false);
        }

        public async Task<BusinessResult> SoftDeleteUser(int userId)
        {
            try
            {
                var softDeleteUser = await _unitOfWork.UserRepository.SoftDeleteUserAsync(userId);
                if (softDeleteUser > 0)
                {
                    return new BusinessResult(Const.SUCCESS_SOFT_DELETE_USER_CODE, Const.SUCCESS_SOFT_DELETE_USER_MSG);
                }
                return new BusinessResult(Const.FAIL_SOFT_DELETE_USER_CODE, Const.FAIL_SOFT_DELETE_USER_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public Task<string> UpdateAvatarOfUser(IFormFile avatarOfUser, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UpdateUserModel updateUserRequestModel)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateAccessToken(string email, User user)
        {
            var role = await _unitOfWork.RoleRepository.GetRoleById(user.RoleId);
            var authClaims = new List<Claim>();
            if (role != null)
            {
                authClaims.Add(new Claim("email", email));
                authClaims.Add(new Claim("role", role.RoleName));
                authClaims.Add(new Claim("UserId", user.UserId.ToString()));
                authClaims.Add(new Claim("Status", user.Status.ToString()));
                authClaims.Add(new Claim("FullName", user.FullName));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            }
            var accessToken = GenerateJWTToken.CreateAccessToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private async Task<string> GenerateRefreshToken(string email, DateTime? beginTimeRefreshToken, int expiredDays)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            var role = await _unitOfWork.RoleRepository.GetRoleById(user.RoleId);
            var authClaims = new List<Claim>
            {
                 new Claim("email", email),
                 new Claim("role", role.RoleName),
                 new Claim("UserId", user.UserId.ToString()),
                 new Claim("Status", user.Status.ToString()),
                 new Claim("FullName", user.FullName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            JwtSecurityToken refreshToken = null;
            if (beginTimeRefreshToken != null)
            {
                DateTime utcTime = (beginTimeRefreshToken ?? DateTime.UtcNow).ToUniversalTime();
                refreshToken = GenerateJWTToken.CreateRefreshToken(authClaims, _configuration, utcTime, expiredDays);
            }
            else
            {
                refreshToken = GenerateJWTToken.CreateRefreshToken(authClaims, _configuration, DateTime.UtcNow, expiredDays);
            }
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }

        private async Task<bool> CreateOtpAsync(string email)
        {
            try
            {
                string otpCode = NumberHelper.GenerateSixDigitNumber().ToString();
                var expiredTime = DateTime.Now.AddMinutes(5);
                var checkExistUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                if(checkExistUser != null)
                {
                    if(checkExistUser.Otp != null)
                    {
                        checkExistUser.Otp = otpCode;
                        checkExistUser.ExpiredOtpTime = expiredTime;
                        await _unitOfWork.UserRepository.UpdateUserAsync(checkExistUser);
                    }
                    else
                    {
                        await _unitOfWork.UserRepository.AddOtpToUser(email, otpCode, expiredTime);
                    }
                    bool checkSendMail = await SendOtpResetPasswordAsync(email, otpCode);
                    return checkSendMail;
                }
                else
                {
                    throw new Exception("Account does not exist. Can not send Otp");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> SendOtpResetPasswordAsync(string email, string otpCode)
        {
            // create new email
            MailRequest newEmail = new MailRequest()
            {
                ToEmail = email,
                Subject = "IPAS Reset Password",
                Body = SendOTPTemplate.EmailSendOTPResetPassword(email, otpCode)
            };

            // send email
            await _mailService.SendEmailAsync(newEmail);
            return true;
        }


    }
}
