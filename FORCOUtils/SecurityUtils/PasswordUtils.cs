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

        public string Encrypt(string aString)
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

        public string Decrypt(string aString)
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
    }
}
