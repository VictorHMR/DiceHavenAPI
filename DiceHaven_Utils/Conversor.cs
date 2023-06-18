using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
