using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Common.Utils
{
    public class NumberHelper
    {
        public static bool IsValidInteger(int value)
        {
            return value >= int.MinValue && value <= int.MaxValue;
        }
        public static bool IsValidDecimal(decimal value)
        {
            return value >= decimal.MinValue && value <= decimal.MaxValue;
        }
        public static int GenerateSixDigitNumber()
        {
            Random random = new Random();
            int number = random.Next(100000, 1000000);
            return number;
        }

        public static long GenerateRandomLong()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            return BitConverter.ToInt64(bytes, 0);
        }

        public static string GenerateRandomCode(string aliasEntity)
        {
            return "IPAS-" + aliasEntity + "-" + DateTime.Now.Ticks;
        }
        public static string GenerateOtp(string email)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / (60 * 60); // Làm mới mỗi 60 phút
            var message = $"{email}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("IPAS"));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            var otp = BitConverter.ToString(hash).Replace("-", "").Substring(0, 6); // Lấy 6 ký tự đầu
            return otp.ToUpper();
        }
    }
}
