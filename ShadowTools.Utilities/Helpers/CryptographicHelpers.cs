using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShadowTools.Utilities.Helpers
{
    public class CryptographicHelpers
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
