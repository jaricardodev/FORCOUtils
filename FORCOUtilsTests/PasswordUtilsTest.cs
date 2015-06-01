using System;
using FORCOUtils.SecurityUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FORCOUtilsTests
{
    [TestClass]
    public class PasswordUtilsTest
    {
        [TestMethod]
        public void TestPasswordEncryptDecrypt()
        {
            const string _OriginalText = "admin@gmail.comadmin";
            PasswordUtils _Utils = new PasswordUtils();
            String _Ciphertext = _Utils.EncryptOneWay(_OriginalText);

            Assert.AreEqual(_Ciphertext, _Utils.EncryptOneWay(_OriginalText));
        }
    }
}
