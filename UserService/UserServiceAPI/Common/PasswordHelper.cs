using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceAPI.Common
{
    /// <summary>
    /// Encrypt and decrypt helper for password.
    /// </summary>
    public static class PasswordHelper
    {
        //TODO: this should be moved to keyvault.
        private const string ENC_KEY = "a14cc5544a4e4133bbce2ea2315a1000";

        /// <summary>
        /// Encrypt and return the encrypted password
        /// </summary>
        /// <param name="plainText">The plain text password</param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ENC_KEY);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}
