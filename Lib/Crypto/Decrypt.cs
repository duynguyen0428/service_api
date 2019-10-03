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
    public class Decrypt:Crypto 
    {
        public Decrypt(string aesPassword): base(aesPassword)
        {
            
        }
         /// <summary>
        /// Returns decoded and decrypted and return the clear text form of the input
        /// crypt text
        /// </summary>
        /// <remarks>
        /// The input string is expected to be url encoded, base-64 encoded and
        /// AES-encrypted as implemented by the <see cref="URLEncodeAESEncryption"/>
        /// routine. When this instance of the crypto class is seeded with the
        /// same password that was used to encrypt the original clear text, then
        /// the original clear text will be returned.
        /// </remarks>
        public string URLDecodeAESEncryption(string clearText)
        {
            return AESDecrypt(HttpUtility.UrlDecode(clearText));
        }
        /// <summary>
        /// Attemps to decrypt and return the clear text form of the input crypt text.
        /// </summary>
        /// <remarks>
        /// The input string is expected to be base-64 encoded and AES-encrypted as
        /// implemented by the <see cref="AESEncrypt"/> routine. When this instance
        /// of the crypto class is seeded with the same password that was used to
        /// encrypt the original clear text, then the original clear text will be
        /// returned.
        /// </remarks>
        public string AESDecrypt(string cryptText)
        {
            // Seed and construct the transformation used for decrypting
            AESCryptoAlgorithm.GenerateIV();
            byte[] iv = AESCryptoAlgorithm.IV;
            int ivSize = iv.Length;
            ICryptoTransform decryptor = AESCryptoAlgorithm.CreateDecryptor(AESKey, iv);
            // The crypt text is expected to be encoded in base64 format, decode it...
            byte[] cryptBytes = Convert.FromBase64String(cryptText);
            // ...then decrypt it
            byte[] clearBytes = decryptor.TransformFinalBlock(cryptBytes, 0,
            cryptBytes.Length);
            // Lastly, strip away the leading iv which was pre-appended
            // when encrypted and decipher the string encoding
            return Encoding.ASCII.GetString(clearBytes, ivSize, clearBytes.Length - ivSize).
            TrimEnd('\0');
        }
    }
  
}