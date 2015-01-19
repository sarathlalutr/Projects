using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class encryptSHA1
    {
        /// <summary>
        ///     take any string and encrypt it using SHA1 then
        ///     return the encrypted data
        /// </summary>
        /// <param name="data">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public static string GetSHA1HashData(string data)
        {
            SHA1 shaM = new SHA1Managed();
            byte[] hash =
                shaM.ComputeHash(Encoding.ASCII.GetBytes(data));

            var stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     encrypt input text using SHA1 and compare it with
        ///     the stored encrypted text
        /// </summary>
        /// <param name="inputData">input text you will enterd to encrypt it</param>
        /// <param name="storedHashData">
        ///     the encrypted
        ///     text stored on file or database ... etc
        /// </param>
        /// <returns>true or false depending on input validation</returns>
        public static bool ValidateSHA1HashData(string inputData, string storedHashData)
        {
            //hash input text and save it string variable
            string getHashInputData = GetSHA1HashData(inputData);

            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
