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
        public static string SUCCESS_UNBANNED_USER_MSG = "Active User Success";
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
        public static int SUCCESS_UPLOAD_IMAGE_CODE = 200;
        public static string SUCCESS_UPLOAD_IMAGE_MESSAGE = "Upload image success";
        public static int SUCCESS_UPLOAD_VIDEO_CODE = 200;
        public static string SUCCESS_UPLOAD_VIDEO_MESSAGE = "Upload video success";
        public static int SUCCESS_DELETE_IMAGE_CODE = 200;
        public static string SUCCESS_DELETE_IMAGE_MESSAGE = "Delete image success";
        public static int SUCCESS_DELETE_VIDEO_CODE = 200;
        public static string SUCCESS_DELETE_VIDEO_MESSAGE = "Delete video success";
        public static int SUCCESS_DELETE_USER_CODE = 200;
        public static string SUCCESS_DELETE_USER_MESSAGE = "Delete user success";
        public static int SUCCESS_GET_ALL_USER_CODE = 200;
        public static string SUCCESS_GET_ALL_USER_MESSAGE = "Get all user success";
        public static int SUCCESS_GET_ALL_USER_BY_ROLE_CODE = 200;
        public static string SUCCESS_GET_ALL_USER_BY_ROLE_MESSAGE = "Get all user by role success";
        public static int SUCCESS_UPDATE_USER_CODE = 200;
        public static string SUCCESS_UPDATE_MESSAGE = "Update user success";
        public static int SUCCESS_GET_PLANT_LOT_BY_ID_CODE = 200;
        public static string SUCCESS_GET_PLANT_LOT_BY_ID_MESSAGE = "Get plant lot by id success";
        public static int SUCCESS_CREATE_PLANT_LOT_CODE = 200;
        public static string SUCCESS_CREATE_PLANT_LOT_MESSAGE = "Create plant lot success";
        public static int SUCCESS_UPDATE_PLANT_LOT_CODE = 200;
        public static string SUCCESS_UPDATE_PLANT_LOT_MESSAGE = "Update plant lot success";
        public static int SUCCESS_DELETE_PLANT_LOT_CODE = 200;
        public static string SUCCESS_DELETE_PLANT_LOT_MESSAGE = "Delete plant lot success";
        public static int SUCCESS_GET_ALL_PLANT_LOT_CODE = 200;
        public static string SUCCESS_GET_ALL_PLANT_LOT_MESSAGE = "Get all plant lot success";
        public static int SUCCESS_CREATE_MANY_PLANT_FROM_PLANT_LOT_CODE = 200;
        public static string SUCCESS_CREATE_MANY_PLANT_FROM_PLANT_LOT_MESSAGE = "Create many plant from plant lot success";

        #endregion

        #region Fail code
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
        public static int FAIL_UPLOAD_IMAGE_CODE = 500;
        public static string FAIL_UPLOAD_IMAGE_MESSAGE = "Upload image failed";
        public static int FAIL_UPLOAD_VIDEO_CODE = 500;
        public static string FAIL_UPLOAD_VIDEO_MESSAGE = "Upload video failed";
        public static int FAIL_DELETE_IMAGE_CODE = 500;
        public static string FAIL_DELETE_IMAGE_MESSAGE = "Delete image failed";
        public static int FAIL_DELETE_VIDEO_CODE = 500;
        public static string FAIL_DELETE_VIDEO_MESSAGE = "Delete video failed";
        public static int FAIL_DELETE_USER_CODE = 500;
        public static string FAIL_DELETE_USER_MESSAGE = "Delete user failed";
        public static int FAIL_GET_ALL_USER_BY_ROLE_CODE = 500;
        public static string FAIL_GET_ALL_USER_BY_ROLE_MESSAGE = "Get all user by role failed";
        public static int FAIL_GET_ALL_USER_CODE = 500;
        public static string FAIL_GET_ALL_USER_MESSAGE = "Get all user failed";
        public static int FAIL_UPDATE_USER_CODE = 500;
        public static string FAIL_UPDATE_USER_MESSAGE = "Update user failed";
        public static int FAIL_CREATE_PLANT_LOT_CODE = 500;
        public static string FAIL_CREATE_PLANT_LOT_MESSAGE = "Create plant lot failed";
        public static int FAIL_UPDATE_PLANT_LOT_CODE = 500;
        public static string FAIL_UPDATE_PLANT_LOT_MESSAGE = "Update plant lot failed";
        public static int FAIL_DELETE_PLANT_LOT_CODE = 500;
        public static string FAIL_DELETE_PLANT_LOT_MESSAGE = "Delete plant lot failed";
        public static int FAIL_CREATE_MANY_PLANT_FROM_PLANT_LOT_CODE = 500;
        public static string FAIL_CREATE_MANY_PLANT_FROM_PLANT_LOT_MESSAGE = "Create many plant from plant lot failed";
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
        public static int WARNING_USER_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_USER_DOES_NOT_EXIST_MSG = "User does not existed";
        public static int WARNING_GET_ALL_USER_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_ALL_USER_DOES_NOT_EXIST_MSG = "Does not have any user";
        public static int WARNING_GET_PLANT_LOT_BY_ID_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_PLANT_LOT_BY_ID_DOES_NOT_EXIST_MSG = "Does not have any plant lot";
        public static int WARNING_CREATE_MANY_PLANT_FROM_PLANT_LOT_CODE = 400;
        public static string WARNING_CREATE_MANY_PLANT_FROM_PLANT_LOT_MSG = "Some criteria does not pass. Please check all criteria again";

        #endregion
    }
}
