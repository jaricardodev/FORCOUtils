﻿using System;
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
            const string _OriginalText = "testo de prueba";
            PasswordUtils _Utils = new PasswordUtils();
            String _Ciphertext = _Utils.Encrypt(_OriginalText);
           
            Assert.AreEqual(_OriginalText, _Utils.Decrypt(_Ciphertext));
        }
    }
}