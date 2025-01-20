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
        public static int SUCCESS_VALIDATE_TOKEN_GOOGLE_CODE = 200;
        public static string SUCCESS_TOKEN_GOOGLE_VALIDATE_MSG = "Validate google token success";
        public static int SUCCESS_FECTH_GOOGLE_USER_INFO_CODE = 200;
        public static string SUCCESS_FECTH_GOOGLE_USER_INFO_MSG = "Fecth info of user from google success";
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
        public static int SUCCESS_GET_FARM_ALL_PAGINATION_CODE = 200;
        public static string SUCCESS_GET_FARM_ALL_PAGINATION_FARM_MSG = "Get all pagination farm success";
        #endregion
        #region PlantLotService code
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
        #region FarmService code
        public static int SUCCESS_GET_FARM_CODE = 200;
        public static string SUCCESS_FARM_GET_MSG = "Get farm by id success";
        public static int SUCCESS_GET_ALL_FARM_WITH_PAGIN_CODE = 200;
        public static string SUCCESS_GET_ALL_FARM_WITH_PAGIN_EMPTY_CODE = "Get all farm empty";
        public static int SUCCESS_CREATE_FARM_CODE = 201;
        public static string SUCCESS_CREATE_FARM_MSG = "Create farm success";
        public static int SUCCESS_UPDATE_FARM_CODE = 200;
        public static string SUCCESS_UPDATE_FARM_MSG = "Update farm success";
        public static int SUCCESS_UPDATE_FARM_COORDINATION_CODE = 200;
        public static string SUCCESS_UPDATE_FARM_COORDINATION_MSG = "Update farm success";
        public static int SUCCESS_DELETE_PERMANENTLY_FARM_CODE = 200;
        public static string SUCCESS_DELETE_PERMANENTLY_FARM_MSG = "Delete farm softed success";
        public static int SUCCESS_DELETE_SOFTED_FARM_CODE = 200;
        public static string SUCCESS_DELETE_SOFTED_FARM_MSG = "Delete farm softed success";
        public static int SUCCESS_GET_ALL_FARM_OF_USER_CODE = 200;
        public static string SUCCESS_GET_ALL_FARM_OF_USER_EMPTY_MSG = "No farm was found";
        public static string SUCCESS_GET_ALL_FARM_OF_USER_FOUND_MSG = "Get all farm of user success.";
        public static int SUCCESS_UPDATE_FARM_LOGO_CODE = 200;
        public static string SUCCESS_UPDATE_FARM_LOGO_MSG = "Update farm success";
        #endregion

        #region CriteriaType code
        public static int SUCCESS_GET_CRITERIA_TYPE_BY_ID_CODE = 200;
        public static string SUCCESS_GET_CRITERIA_TYPE_BY_ID_MESSAGE = "Get criteria type by id success";
        public static int SUCCESS_GET_CRITERIA_TYPE_BY_NAME_CODE = 200;
        public static string SUCCESS_GET_CRITERIA_TYPE_BY_NAME_MESSAGE = "Get criteria type by name success";
        public static int SUCCESS_CREATE_CRITERIA_TYPE_CODE = 200;
        public static string SUCCESS_CREATE_CRITERIA_TYPE_MESSAGE = "Create criteria type success";
        public static int SUCCESS_UPDATE_CRITERIA_TYPE_CODE = 200;
        public static string SUCCESS_UPDATE_CRITERIA_TYPE_MESSAGE = "Update criteria type success";
        public static int SUCCESS_DELETE_CRITERIA_TYPE_CODE = 200;
        public static string SUCCESS_DELETE_CRITERIA_TYPE_MESSAGE = "Delete criteria type success";
        public static int SUCCESS_GET_ALL_CRITERIA_TYPE_CODE = 200;
        public static string SUCCESS_GET_ALL_CRITERIA_TYPE_MESSAGE = "Get all criteria type success";
        #endregion

        #region Partner code
        public static int SUCCESS_GET_PARTNER_BY_ID_CODE = 200;
        public static string SUCCESS_GET_PARTNER_BY_ID_MESSAGE = "Get partner by id success";
        public static int SUCCESS_GET_PARTNER_BY_ROLE_NAME_CODE = 200;
        public static string SUCCESS_GET_PARTNER_BY_ROLE_NAME_MESSAGE = "Get partner by role name success";
        public static int SUCCESS_CREATE_PARTNER_CODE = 200;
        public static string SUCCESS_CREATE_PARTNER_MESSAGE = "Create partner success";
        public static int SUCCESS_UPDATE_PARTNER_CODE = 200;
        public static string SUCCESS_UPDATE_PARTNER_MESSAGE = "Update partner success";
        public static int SUCCESS_DELETE_PARTNER_CODE = 200;
        public static string SUCCESS_DELETE_PARTNER_MESSAGE = "Delete partner success";
        public static int SUCCESS_GET_ALL_PARTNER_CODE = 200;
        public static string SUCCESS_GET_ALL_PARTNER_MESSAGE = "Get all partner success";
        #endregion

        #region GrowthStage code
        public static int SUCCESS_GET_GROWTHSTAGE_BY_ID_CODE = 200;
        public static string SUCCESS_GET_GROWTHSTAGE_BY_ID_MESSAGE = "Get GrowthStage by id success";
        public static int SUCCESS_CREATE_GROWTHSTAGE_CODE = 200;
        public static string SUCCESS_CREATE_GROWTHSTAGE_MESSAGE = "Create GrowthStage success";
        public static int SUCCESS_UPDATE_GROWTHSTAGE_CODE = 200;
        public static string SUCCESS_UPDATE_GROWTHSTAGE_MESSAGE = "Update GrowthStage success";
        public static int SUCCESS_DELETE_GROWTHSTAGE_CODE = 200;
        public static string SUCCESS_DELETE_GROWTHSTAGE_MESSAGE = "Delete GrowthStage success";
        public static int SUCCESS_GET_ALL_GROWTHSTAGE_CODE = 200;
        public static string SUCCESS_GET_ALL_GROWTHSTAGE_MESSAGE = "Get all GrowthStage success";
        #endregion

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
        #endregion

        #region PlantLot Fail code
        public static int FAIL_CREATE_PLANT_LOT_CODE = 500;
        public static string FAIL_CREATE_PLANT_LOT_MESSAGE = "Create plant lot failed";
        public static int FAIL_UPDATE_PLANT_LOT_CODE = 500;
        public static string FAIL_UPDATE_PLANT_LOT_MESSAGE = "Update plant lot failed";
        public static int FAIL_DELETE_PLANT_LOT_CODE = 500;
        public static string FAIL_DELETE_PLANT_LOT_MESSAGE = "Delete plant lot failed";
        public static int FAIL_CREATE_MANY_PLANT_FROM_PLANT_LOT_CODE = 500;
        public static string FAIL_CREATE_MANY_PLANT_FROM_PLANT_LOT_MESSAGE = "Create many plant from plant lot failed";
        public static int FAIL_CREATE_MANY_PLANT_BECAUSE_CRITERIA_INVALID_CODE = 500;
        public static string FAIL_CREATE_MANY_PLANT_BECAUSE_CRITERIA_INVALID_MESSAGE = "Some criteria invalid";
        #endregion

        #region CriteriaType Fail code
        public static int FAIL_CREATE_CRITERIA_TYPE_CODE = 500;
        public static string FAIL_CREATE_CRITERIA_TYPE_MESSAGE = "Create criteria type failed";
        public static int FAIL_UPDATE_CRITERIA_TYPE_CODE = 500;
        public static string FAIL_UPDATE_CRITERIA_TYPE_MESSAGE = "Update criteria type failed";
        public static int FAIL_DELETE_CRITERIA_TYPE_CODE = 500;
        public static string FAIL_DELETE_CRITERIA_TYPE_MESSAGE = "Delete criteria type failed";
        public static int FAIL_GET_CRITERIA_TYPE_CODE = 500;
        public static string FAIL_GET_CRITERIA_TYPE_MESSAGE = "Get criteria type failed";
        #endregion

        #region Farm Fail code
        public static int FAIL_CREATE_FARM_CODE = 500;
        public static string FAIL_CREATE_FARM_MSG = "Create farm have server error";
        public static int FAIL_UPDATE_FARM_CODE = 201;
        public static string FAIL_UPDATE_FARM_MSG = "Farm Update fail";
        public static int FAIL_UPDATE_FARM_COORDINATION_CODE = 201;
        public static string FAIL_UPDATE_FARM_COORDINATION_MSG = "Farm Update fail";
        public static int FAIL_DELETE_PERMANENTLY_FARM_CODE = 200;
        public static string FAIL_DELETE_PERMANENTLY_FARM_MSG = "Delete farm softed fail";
        public static int FAIL_DELETE_SOFTED_FARM_CODE = 200;
        public static string FAIL_DELETE_SOFTED_FARM_MSG = "Delete farm softed fail";
        public static int FAIL_UPDATE_FARM_LOGO_CODE = 201;
        public static string FAIL_UPDATE_FARM_LOGO_MSG = "Farm Update fail";
        #endregion

        #region Partner Fail code
        public static int FAIL_CREATE_PARTNER_CODE = 500;
        public static string FAIL_CREATE_PARTNER_MESSAGE = "Create partner failed";
        public static int FAIL_UPDATE_PARTNER_CODE = 500;
        public static string FAIL_UPDATE_PARTNER_MESSAGE = "Update partner failed";
        public static int FAIL_DELETE_PARTNER_CODE = 500;
        public static string FAIL_DELETE_PARTNER_MESSAGE = "Delete partner failed";
        public static int FAIL_GET_PARTNER_CODE = 500;
        public static string FAIL_GET_PARTNER_MESSAGE = "Get partner failed";
        #endregion

        #region GrowthStage Fail code
        public static int FAIL_CREATE_GROWTHSTAGE_CODE = 500;
        public static string FAIL_CREATE_GROWTHSTAGE_MESSAGE = "Create GrowthStage failed";
        public static int FAIL_UPDATE_GROWTHSTAGE_CODE = 500;
        public static string FAIL_UPDATE_GROWTHSTAGE_MESSAGE = "Update GrowthStage failed";
        public static int FAIL_DELETE_GROWTHSTAGE_CODE = 500;
        public static string FAIL_DELETE_GROWTHSTAGE_MESSAGE = "Delete GrowthStage failed";
        public static int FAIL_GET_GROWTHSTAGE_CODE = 500;
        public static string FAIL_GET_GROWTHSTAGE_MESSAGE = "Get GrowthStage failed";
        #endregion


        #region Warning Code

        public static int WARNING_INVALID_LOGIN_CODE = 400;
        public static string WARNING_INVALID_LOGIN_MSG = "UserName or Password is wrong";
        public static int WARNING_INVALID_REFRESH_TOKEN_CODE = 400;
        public static string WARNING_INVALID_REFRESH_TOKEN_MSG = "Refresh Token is expired time. Please log out";
        #endregion

        #region UserService
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
        #endregion
        #region PlantLot
        public static int WARNING_GET_PLANT_LOT_BY_ID_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_PLANT_LOT_BY_ID_DOES_NOT_EXIST_MSG = "Does not have any plant lot";
        public static int WARNING_CREATE_MANY_PLANT_FROM_PLANT_LOT_CODE = 400;
        public static string WARNING_CREATE_MANY_PLANT_FROM_PLANT_LOT_MSG = "Some criteria does not pass. Please check all criteria again";
        #endregion

        #region CriteriaType
        public static int WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_CRITERIA_TYPE_DOES_NOT_EXIST_MSG = "Does not have any criteria type";
        #endregion

        #region FarmService
        public static int WARNING_GET_FARM_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_FARM_NOT_EXIST_MSG = "Farm Resource not found";
        public static int WARNING_GET_ALL_FARM_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_ALL_FARM_DOES_NOT_EXIST_MSG = "Does not have any farm";
        #endregion

        #region CriteriaType
        public static int WARNING_GET_PARTNER_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_PARTNER_DOES_NOT_EXIST_MSG = "Does not have any partner";
        #endregion

        #region GrowthStage
        public static int WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_CODE = 404;
        public static string WARNING_GET_GROWTHSTAGE_DOES_NOT_EXIST_MSG = "Does not have any GrowthStage";
        #endregion
    }
}
