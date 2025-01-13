using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Common.Utils
{
    public static class PasswordHelper
    {
        private static readonly byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 };
        private static readonly byte[] _iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 };


        public static string ConvertToEncrypt(string password)
        {
            //if (string.IsNullOrEmpty(password)) return "";
            //byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
            //string encryptedPassword = Convert.ToBase64String(storePassword);
            //return encryptedPassword;
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                var encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(password), 0, password.Length);
                return Convert.ToBase64String(encrypted);
            }
        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            //if (string.IsNullOrEmpty(base64EncodeData)) return "";
            //byte[] encryptedPassword = Convert.FromBase64String(base64EncodeData);
            //string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
            //return decryptedPassword;
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(base64EncodeData), 0, base64EncodeData.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);
        }

        public static bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword, HashType.SHA256);
        }

        public static string GeneratePassword()
        {
            Random random = new Random();
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var password = new char[6];
            for (int i = 0; i < password.Length; i++)
            {
                int index = random.Next(allowedChars.Length);
                password[i] = allowedChars[index];
            }
            return new string(password);
        }
    }
}
