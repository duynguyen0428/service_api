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
    public abstract class Crypto
    {
        //
        // -- Constants ------------------------------------------------------------------
        /// <summary>
        /// The expected key size.
        /// </summary>
        public const int AES_KEY_SIZE = 16;
        //
        // -- Fields ---------------------------------------------------------------------
        // Cached copy of the MD5 algorithm used in computing the key.
        private MD5 _AESKeyHashingAlgorithm;
        // Cached copy of the crypto algorithm used in transforming clear/crypt text.
        private Rijndael _AESCryptoAlogrithm;
        // Cached copy of the computed key.
        private byte[] _AESKey;
        /// <summary>
        /// Constructs an instance of this crypto class seeded with a specific AES-password
        /// which will be used to derive the encryption/decryption key.
        /// </summary>
        /// <param name="aesPassword"></param>
        public Crypto(string aesPassword)
        {
            AESPassword = aesPassword;
        }


    
        public static string HexEncode(byte[] data)
        {
            StringBuilder buff = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                buff.Append(b.ToString("x2"));
            return buff.ToString();
        }


        /// <summary>
        /// Converts a string into a byte array by interpreting every 2 characters
        /// as a hexadecimal representation of the corresponding byte value.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexDecode(string hex)
        {
            int len = hex.Length;
            byte[] bytes = new byte[len / 2];
            for (int i = 0; i < len; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        //
        // -- Properties -----------------------------------------------------------------
        /// <summary>
        /// The password which was used to seed this instance of this crypto class
        /// and from which the AES key is derived from.
        /// </summary>
        public string AESPassword
        { get; protected set; }
        /// <summary>
        /// Returns a hashing algorithm that's fully constructed and configured to be used
        /// for hashing the key used for the CSI-AES-Crypto protocol.
        /// </summary>
        protected MD5 AESKeyHashingAlgorithm
        {
            get
            {
                if (_AESKeyHashingAlgorithm == null)
                    _AESKeyHashingAlgorithm = MD5.Create();
                return _AESKeyHashingAlgorithm;
            }
        }
        /// <summary>
        /// Returns a crypto algorithm that's fully constructed and configured to be used
        /// for the encryption/decryption of data for the CSI-AES-Crypto protocol.
        /// </summary>
        protected Rijndael AESCryptoAlgorithm
        {
            get
            {
                if (_AESCryptoAlogrithm == null)
                {
                    _AESCryptoAlogrithm = Rijndael.Create();
                    _AESCryptoAlogrithm.BlockSize = 128;
                    _AESCryptoAlogrithm.Mode = CipherMode.CBC;
                    _AESCryptoAlogrithm.Padding = PaddingMode.Zeros;
                }
                return _AESCryptoAlogrithm;
            }
        }
        /// <summary>
        /// Returns the key derived from the <see cref="AESPassword">password</see> to be
        ///used
        /// for encryption/decryption of data for the CSI-AES-Crypto protocol.
        /// </summary>
        /// <remarks>
        /// The mechanism for deriving the key from the password is one of the
        /// aspects unique to the CSI-AES-Crypto protocol, as it specifies
        /// a unique way of transforming the password value into a key.
        /// </remarks>
        protected byte[] AESKey
        {
            get
            {
                if (_AESKey == null)
                {
                    // To compute the actual key used in encrypting/decrypting:
                    // 1. First, we compute the MD5 hash of the inputted password
                    byte[] keyHash = AESKeyHashingAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(AESPassword));
                    // 2. Then, we convert the result to a hex string representation of
                    ///digest
                    string keyHashText = HexEncode(keyHash);
                    // 3. Finally, we take a substring of that hex representation equal
                    // to the key size and convert it into a usable key byte array
                    _AESKey = Encoding.ASCII.GetBytes(keyHashText.Substring(0, AES_KEY_SIZE));
                }
                return _AESKey;
            }
        }


       
    }
  
}