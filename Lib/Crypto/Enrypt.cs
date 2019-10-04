using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace Lib.Crypto
{
    public class Encrypt: Crypto
    {   

        public Encrypt(string aesPassword): base(aesPassword)
        {
            
        }
        /// <summary>
        /// Returns the encrypted and base-64 encoded form of the input clear text.
        /// </summary>
        /// <remarks>
        /// The result string can be passed to the <see cref="AESDecrypt"/> routine
        /// which will return the original clear text when seeded with the same
        /// <see cref="AESPassword"/>.
        /// </remarks>
        public string AESEncrypt(string clearText)
        {
            // Seed and construct the transformation used for encrypting
            AESCryptoAlgorithm.GenerateIV();
            byte[] iv = AESCryptoAlgorithm.IV;
            int ivSize = iv.Length;
            ICryptoTransform encryptor = AESCryptoAlgorithm.CreateEncryptor(AESKey, iv);
            int blockSize = encryptor.InputBlockSize;
            byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);
            byte[] cryptBytes = encryptor.TransformFinalBlock(clearBytes, 0,
            clearBytes.Length);
            int cryptSize = cryptBytes.Length;
            // Combine the IV and the encrypted data
            byte[] totalBytes = new byte[ivSize + cryptSize];
            Array.Copy(iv, 0, totalBytes, 0, ivSize);
            Array.Copy(cryptBytes, 0, totalBytes, ivSize, cryptSize);
            // Lastly, Base64 encode the whole thing
            return Convert.ToBase64String(totalBytes);
        }
        /// <summary>
        /// Returns a URL Encoded version of the encrypted and base-64 encoded form of the
        ///input clear text.
        /// </summary>
        /// <remarks>
        /// The result string can be passed to the <see cref="URLDecodeAESDecrypt"/>
        ///routine
        /// which will return the original clear text when seeded with the same
        /// </remarks>
        public string URLEncodeAESEncryption(string clearText)
        {
            return HttpUtility.UrlEncode(AESEncrypt(clearText));
        }

    /// <summary>
        /// Returns a URL Encoded version of the encrypted and base-64 encoded form of the
        ///input clear text.
        /// </summary>
        /// <remarks>
        /// The result string can be passed to the <see cref="URLDecodeAESDecrypt"/>
        ///routine
        /// which will return the original clear text when seeded with the same
        /// </remarks>
        public string ComputeSha256Hash(string rawData) 
        {
                        // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }
    }
  
}