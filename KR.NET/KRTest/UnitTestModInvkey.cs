﻿using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KRTest
{
    [TestClass]
    public class UnitTestModInvkey
    {
        public TestContext TestContext { get; set; }

        string strDirBase = null;
        string strProjectDir = null;
        string strDirBaseCrypt = null;
        string dateKLog = null;
        string testKey = null;
        string klogKey = null;

        [TestInitialize]
        public void TestInitialize()
        {
            strDirBase = (string)TestContext.Properties["WorkTestRoot"];
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = strDirBase + "\\CryptDir";
            dateKLog = (string)TestContext.Properties["klogDate"];
            testKey = (string)TestContext.Properties["testKey"];
            klogKey = (string)TestContext.Properties["klogKey"];
        }

        [TestMethod]
        public void TestMethodReverseKey()
        {
            string outReverse = MOD_INVKEY.reverseKey(strDirBase + "\\klog.txt", true);
            Assert.AreEqual("[64]" + testKey , outReverse.Substring(0, testKey.Length + 4));
        }
    }
}
