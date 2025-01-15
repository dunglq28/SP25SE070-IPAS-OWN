using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CapstoneProject_SP25_IPAS_Common
{
    public static class Const
    {

        public static string APIIpasEndPoint = "https://localhost:7111";
        public static string APIEndPoint = "https://localhost:7111";
        #region Error Codes

        public static int ERROR_EXCEPTION = 400;

        #endregion

        #region Success Codes
        #region UserService
        public static int SUCCESS_LOGIN_CODE = 200;
        public static string SUCCESS_LOGIN_MSG = "Login Successfully";
        public static int SUCCESS_LOGOUT_CODE = 200;
        public static string SUCCESS_LOGOUT_MSG = "Log out success";
        public static int SUCCESS_RFT_CODE = 200;
        public static string SUCCESS_RFT_MSG = "Refresh Token successfully";
        public static int SUCCESS_REGISTER_CODE = 200;
        public static string SUCCESS_REGISTER_MSG = "Register success. You can login now";
        public static int SUCCESS_SOFT_DELETE_USER_CODE = 200;
        public static string SUCCESS_SOFT_DELETE_USER_MSG = "Soft Delete User Success";
        public static int SUCCESS_BANNED_USER_CODE = 200;
        public static string SUCCESS_BANNED_USER_MSG = "Banned User Success";
        public static int SUCCESS_SEND_OTP_RESET_PASSWORD_CODE = 200;
        public static string SUCCESS_SEND_OTP_RESET_PASSWORD_USER_MSG = "Otp has sended. Please check your mail";
        public static int SUCCESS_GET_USER_CODE = 200;
        public static string SUCCESS_GET_USER_BY_EMAIL_MSG = "Get user by email success";
        public static string SUCCESS_GET_USER_BY_ID_MSG = "Get user by id success";
        public static int SUCCESS_RESET_PASSWORD_CODE = 200;
        public static string SUCCESS_RESET_PASSWORD_MSG = "Reset password success";
        public static int SUCCESS_CONFIRM_RESET_PASSWORD_CODE = 200;
        public static string SUCCESS_CONFIRM_RESET_PASSWORD_MESSAGE = "You can reset password now";
        public static int SUCCESS_OTP_VALID_CODE = 200;
        public static string SUCCESS_OTP_VALID_MESSAGE = "Otp is valid";
        public static int SUCCESS_VALIDATE_TOKEN_GOOGLE_CODE = 200;
        public static string SUCCESS_TOKEN_GOOGLE_VALIDATE_MSG = "Validate google token success";
        public static int SUCCESS_FECTH_GOOGLE_USER_INFO_CODE = 200;
        public static string SUCCESS_FECTH_GOOGLE_USER_INFO_MSG = "Fecth info of user from google success";
        #endregion
        #endregion

        #region Fail code
        #region User code
        public static int FAIL_CREATE_CODE = -1;
        public static int FAIL_LOGOUT_CODE = 500;
        public static string FAIL_LOGOUT_MSG = "Have an error when logout";
        public static int FAIL_SOFT_DELETE_USER_CODE = 500;
        public static string FAIL_SOFT_DELETE_USER_MSG = "Have an error when soft delete user";
        public static int FAIL_BANNED_USER_CODE = 500;
        public static string FAIL_BANNED_USER_MSG = "Have an error when banned user";
        public static int FAIL_GET_USER_CODE = 500;
        public static string FAIL_GET_USER_BY_EMAIL_MSG = "Have an error when get user by email";
        public static string FAIL_GET_USER_BY_ID_MSG = "Have an error when get user by id";
        public static int FAIL_RESET_PASSWORD_CODE = 500;
        public static string FAIL_RESET_PASSWORD_MSG = "Have an error when reset password";
        public static int FAIL_CONFIRM_RESET_PASSWORD_CODE = 500;
        public static string FAIL_CONFIRM_RESET_PASSWORD_MESSAGE = "Otp does not correct or expired. Please try again or another";
        public static int FAIL_LOGIN_WITH_GOOGLE_CODE = 500;
        public static string FAIL_LOGIN_WITH_GOOGLE_MSG = "Your email has not exist in system";
        public static int FAIL_VALIDATE_GOOGLE_TOKEN_INVALID_CODE = 500;
        public static string FAIL_VALIDATE_GOOGLE_TOKEN_INVALID_MSG = "Your google code not valid. Please try again";
        public static int FAIL_USER_INFO_FETCH_CODE = 500;
        public static string FAIL_USER_INFO_FETCH_MSG = "Fetch info of user from google fail";

        #endregion
        #endregion

        #region Warning Code

        public static int WARNING_INVALID_LOGIN_CODE = 4;
        public static string WARNING_INVALID_LOGIN_MSG = "UserName or Password is wrong";
        public static int WARNING_INVALID_REFRESH_TOKEN_CODE = 4;
        public static string WARNING_INVALID_REFRESH_TOKEN_MSG = "Refresh Token is expired time. Please log out";

        public static int WARNING_ACCOUNT_BANNED_CODE = 400;
        public static string WARNING_ACCOUNT_BANNED_MSG = "Your account is banned";
        public static int WARNING_PASSWORD_INCORRECT_CODE = 401;
        public static string WARNING__PASSWORD_INCORRECT_MSG = "Password is not correct";
        public static int WARNING_SIGN_IN_CODE = 404;
        public static string WARNING_SIGN_IN_MSG = "Account does not exist. Please try again!";
        public static int WARNING_RFT_NOT_EXIST_CODE = 404;
        public static string WARNING_RFT_NOT_EXIST_MSG = "Refresh token does not exist in system";
        public static int WARNING_ACCOUNT_IS_EXISTED_CODE = 400;
        public static string WARNING_ACCOUNT_IS_EXISTED_MSG = "Account is existed";
        public static int WARNING_ROLE_IS_NOT_EXISTED_CODE = 400;
        public static string WARNING_ROLE_IS_NOT_EXISTED_MSG = "Role does not existed";
        public static int WARNING_BANNED_USER_CODE = 404;
        public static string WARNING_BANNED_USER_MSG = "User does not existed or is banned";
        public static int WARNING_RESET_PASSWORD_CODE = 404;
        public static string WARNING_RESET_PASSWORD_MSG = "Account does not exist or otp incorrect";
        public static int WARNING_CHECK_MAIL_REGISTER_CODE = 400;
        public static string WARNING_CHECK_MAIL_REGISER_MSG = "Email and OTP are required";


        #endregion
    }
}
