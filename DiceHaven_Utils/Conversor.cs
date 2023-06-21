using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Utils
{
    public class Conversor
    {
        public static byte[] ConvertToByteArray(string fileBase64)
        {
            return Convert.FromBase64String(fileBase64);
        }
        public static string ConvertToBase64(byte[] fileByteArray)
        {
            return Convert.ToBase64String(fileByteArray);
        }

        public static string HashPassword(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes =Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
