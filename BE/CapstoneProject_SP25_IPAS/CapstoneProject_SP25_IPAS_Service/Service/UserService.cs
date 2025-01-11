using AutoMapper;
using CapstoneProject_SP25_IPAS_BussinessObject.Entities;
using CapstoneProject_SP25_IPAS_Common;
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
        private readonly IMailService _service;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService service, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _service = service;
            _mapper = mapper;
        }

        public Task<bool> BannedUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmResetPassword(ConfirmOtpModel confirmOtpModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUser(CreateAccountModel createAccountModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExecuteResetPassword(ResetPasswordModel resetPasswordModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessResult> LoginByEmailAndPassword(string email, string password)
        {
            using(var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                    if (existUser == null)
                    {
                        return new BusinessResult(Const.WARNING_SIGN_IN_CODE, Const.WARNING_SIGN_IN_MSG);
                    }
                 
                    var verifyPassword = PasswordHelper.VerifyPassword(password, existUser.Password);
                    if(verifyPassword || existUser.Password == null)
                    {
                        if(existUser.Status.ToLower().Equals("Banned".ToLower()) || existUser.IsDelete == true)
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
            if(checkExistRefreshToken != null)
            {
                var result = await _unitOfWork.RefreshTokenRepository.DeleteToken(refreshToken);
                if(result)
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
                if(email != null)
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
                                if(checkExistRefreshToken.ExpiredDate >= DateTime.Now)
                                {
                                    var newAccessToken = await GenerateAccessToken(email, existUser);
                                    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int newTokenValidityInMinutes);

                                    var newRefreshToken = await GenerateRefreshToken(email, checkExistRefreshToken.ExpiredDate,0);
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
                        UserCode = "IPAS-" + "USR-" + NumberHelper.GenerateSixDigitNumber(),
                        FullName = model.FullName,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = "Active",
                        IsDelete = false,
                    };

                    var checkExistUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(model.Email);
                    if(checkExistUser != null)
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

        public Task<bool> RequestResetPassword(string email)
        {
            throw new NotImplementedException();
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
               catch(Exception ex)
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
    }
}
