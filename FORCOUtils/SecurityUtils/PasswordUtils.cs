using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FORCOUtils.SecurityUtils
{
    public class PasswordUtils
    {
        private readonly byte[] fKey = Encoding.ASCII.GetBytes("Bl1ndEncr4pti0n");
        private readonly byte[] fIv = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        /// <author>Marlon Gonzalez Gongora-nickoo1987@gmail.com</author>
        /// <summary>
        /// Encrypts a string using the Rijndael algorithm
        /// </summary>
        /// <param name="aString">String to encrypt</param>
        /// <returns>Encrypted text</returns>
        public string EncryptTwoWay(string aString)
        {

            byte[] _InputBytes = Encoding.ASCII.GetBytes(aString);
            byte[] _Encripted;
            RijndaelManaged _Cripto = new RijndaelManaged();
            using (MemoryStream _Ms = new MemoryStream(_InputBytes.Length))
            {
                using (CryptoStream _ObjCryptoStream = new CryptoStream(_Ms, _Cripto.CreateEncryptor(fKey, fIv), CryptoStreamMode.Write))
                {
                    _ObjCryptoStream.Write(_InputBytes, 0, _InputBytes.Length);
                    _ObjCryptoStream.FlushFinalBlock();
                    _ObjCryptoStream.Close();
                }
                _Encripted = _Ms.ToArray();
            }
            return Convert.ToBase64String(_Encripted);
        }

        /// <author>Marlon Gonzalez Gongora-nickoo1987@gmail.com</author>
        /// <summary>
        /// Decrypt a string using the Rijndael algorithm
        /// </summary>
        /// <param name="aString">String to decrypt</param>
        /// <returns>original text</returns>
        public string DecryptTwoWay(string aString)
        {
            byte[] _InputBytes = Convert.FromBase64String(aString);
            string _CleanText;
            RijndaelManaged _Cripto = new RijndaelManaged();
            using (MemoryStream _Ms = new MemoryStream(_InputBytes))
            {
                using (CryptoStream _ObjCryptoStream = new CryptoStream(_Ms, _Cripto.CreateDecryptor(fKey, fIv), CryptoStreamMode.Read))
                {
                    using (StreamReader _Reader = new StreamReader(_ObjCryptoStream, true))
                    {
                        _CleanText = _Reader.ReadToEnd();
                    }
                }
            }
            return _CleanText;
        }

        /// <author>Marlon Gonzalez Gongora-nickoo1987@gmail.com</author>
        /// <summary>
        /// Encrypt a string using SHA1CryptoServiceProvider
        /// </summary>
        /// <param name="aString">String to encrypt</param>
        /// <returns>original text</returns>
        public string EncryptOneWay(string aString)
        {
            const string _Salt = "Bl1ndEncr4pti0n";
            SHA1CryptoServiceProvider _Crypto = new SHA1CryptoServiceProvider();
            byte[] _TextWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(aString, _Salt));
            byte[] _HashedBytes = _Crypto.ComputeHash(_TextWithSaltBytes);
            _Crypto.Clear();
            return Convert.ToBase64String(_HashedBytes);
        }
    }
}
