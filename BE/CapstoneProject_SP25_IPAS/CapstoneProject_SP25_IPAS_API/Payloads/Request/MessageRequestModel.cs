namespace CapstoneProject_SP25_IPAS_API.Payloads.Request
{
    public class MessageRequestModel
    {
        public string Message { get; set; }
    }
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }
    public class EmailModel
    {
        public string Email { get; set; }
    }
    public class VerifyOtpRequest
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
