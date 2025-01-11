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

        public static int ERROR_EXCEPTION = -4;

        #endregion

        #region Success Codes

        public static int SUCCESS_CREATE_CODE = 1;
        public static string SUCCESS_CREATE_MSG = "Save data success";
        public static int SUCCESS_READ_CODE = 1;
        public static string SUCCESS_READ_MSG = "Get data success";
        public static int SUCCESS_UPDATE_CODE = 1;
        public static string SUCCESS_UPDATE_MSG = "Update data success";
        public static int SUCCESS_DELETE_CODE = 1;
        public static string SUCCESS_DELETE_MSG = "Delete data success";
        public static int SUCCESS_CHECK_CODE = 1;
        public static string SUCCESS_CHECK_MSG = "Check data is valid";
        public static int SUCCESS_CREATE_PAYMENT_URL_CODE = 1;
        public static string SUCCESS_CREATE_PAYMENT_URL_MSG = "Create payment link success";
        public static int SUCCESS_PAYMENT_EXCUTE_CODE = 1;
        public static string SUCCESS_PAYMENT_EXCUTE_MSG = "Create payment link success";
        public static int SUCCESS_UPDATE_AFTER_PAYMENT_CODE = 1;
        public static string SUCCESS_UPDATE_AFTER_PAYMENT_MSG = "Update data after payment success";
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

        #endregion

        #region Fail code

        public static int FAIL_CREATE_CODE = -1;
        public static string FAIL_CREATE_MSG = "Save data fail";
        public static int FAIL_READ_CODE = -1;
        public static string FAIL_READ_MSG = "Get data fail";
        public static int FAIL_UPDATE_CODE = -1;
        public static string FAIL_UPDATE_MSG = "Update data fail";
        public static int FAIL_DELETE_CODE = -1;
        public static string FAIL_DELETE_MSG = "Delete data fail";
        public static int FAIL_CHECK_ID_CODE = -1;
        public static string FAIL_CHECK_ID_MSG = "Invalid ID format";
        public static int FAIL_CHECK_DATE_FILTER_CODE = -1;
        public static string FAIL_CHECK_DATE_FILTER_MSG = "Date 'To' must greater than Date 'From'";
        public static int FAIL_CHECK_NUMBER_FILTER_CODE = -1;
        public static string FAIL_CHECK_NUMBER_FILTER_MSG = "Number 'To' must greater than Number 'From'";
        public static int FAIL_PAYMENT_EXCUTE_CODE = -1;
        public static string FAIL_PAYMENT_EXCUTE_MSG = "Payment excute fail";
        public static int FAIL_UPDATE_AFTER_PAYMENT_CODE = -1;
        public static string FAIL_UPDATE_AFTER_PAYMENT_MSG = "Update data fail";

        public static int FAIL_LOGOUT_CODE = 500;
        public static string FAIL_LOGOUT_MSG = "Have an error when logout";
        public static int FAIL_SOFT_DELETE_USER_CODE = 500;
        public static string FAIL_SOFT_DELETE_USER_MSG = "Have an error when soft delete user";

        #endregion

        #region Warning Code

        public static int WARNING_NO_DATA_CODE = 4;
        public static string WARNING_NO_DATA_MSG = "No data";
        public static int WARNING_INVALID_ID_CODE = 4;
        public static string WARNING_INVALID_ID_MSG = "Invalid ID format";
        public static int WARNING_INVALID_USER_ID_CODE = 4;
        public static string WARNING_INVALID_USER_ID_MSG = "Invalid User ID format";
        public static int WARNING_INVALID_DETAIL_PROPOSAL_ID_CODE = 4;
        public static string WARNING_INVALID_DETAIL_PROPOSAL_ID_MSG = "Invalid Detail Proposal ID format";
        public static int WARNING_WRONG_ROLE_CODE = 4;
        public static string WARNING_WRONG_ROLE_MSG = "Users are not authorized to make bids";
        public static int WARNING_INVALID_DATE_FILTER_CODE = 4;
        public static string WARNING_INVALID_DATE_FILTER_MSG = "Date 'To' must greater than Date 'From'";
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

        #endregion
    }
}
