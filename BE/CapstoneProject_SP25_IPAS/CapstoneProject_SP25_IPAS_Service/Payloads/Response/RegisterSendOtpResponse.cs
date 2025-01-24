using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Payloads.Response
{
    public class RegisterSendOtpResponse
    {
        [JsonPropertyName("Otp-hash-sha256-base64")]
        public string OtpHashSHA256 { get; set; }
    }
}
