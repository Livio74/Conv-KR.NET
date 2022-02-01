using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using KRLib.NET;

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
            strDirBase = TestUtils.getWorkTestRoot(TestContext);
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            dateKLog = TestUtils.getKlogDate(TestContext);
            testKey = TestUtils.getTestKey(TestContext);
            klogKey = TestUtils.getKlogKey(TestContext); 
        }

        [TestMethod]
        public void TestMethodReverseKey()
        {
            string outReverse = MOD_INVKEY.reverseKey(strDirBase + "\\klog.txt", true);
            Assert.AreEqual("[64]" + testKey , outReverse.Substring(0, testKey.Length + 4));
        }

        [TestMethod]
        public void TestMethodInvKript()
        {
            string fileClearName = "UnitTestModUtilsSo.cs";
            string fileCryptname = "Lkkfkgel7iqHlgRiLT.Qi";
            string outInvKript = MOD_INVKEY.InvKript(fileClearName, fileCryptname, true);
            Assert.AreEqual(testKey, outInvKript.Substring(0, testKey.Length));
        }
    }
}
