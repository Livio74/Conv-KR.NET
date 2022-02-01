using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using KRLib.NET;

namespace KRTest
{
    [TestClass]
    public class UnitTestModPrgUtils
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
        public void TestMethodKriptp()
        {
            string strDirBase = (string) TestContext.Properties["WorkTestRoot"];
            string clearFilePathToElabOrig = strDirBase + "\\ClearDir\\KR.NET\\KR.NET.csproj";
            string clearFilePathToElab = strDirBase + "\\KR.NET.csproj";
            string clearFilePathExpected = clearFilePathToElabOrig;
            string cryptedFilePathToElab = strDirBase + "\\Fx.8zl.reuhwu";
            string cryptedFilePathExpected = strDirBase + "\\CryptDir\\KR.NET\\Fx.8zl.reuhwu";
            string cryptedFilePathOut = clearFilePathToElab + ".krp";

            File.Copy(clearFilePathToElabOrig, clearFilePathToElab);
            MOD_PRG_UTILS.Kriptp(clearFilePathToElab, testKey);
            Assert.IsTrue(TestUtils.FilesAreEqual(cryptedFilePathToElab, cryptedFilePathExpected), "File " + cryptedFilePathToElab + " are not equals " + cryptedFilePathExpected);
            MOD_PRG_UTILS.Kriptp(cryptedFilePathToElab, testKey);
            Assert.IsTrue(TestUtils.FilesAreEqual(clearFilePathToElab, clearFilePathExpected), "File " + clearFilePathToElab + " are not equals " + clearFilePathExpected);
            MOD_PRG_UTILS.Kriptp(clearFilePathToElab, testKey, cryptedFilePathOut);
            Assert.IsTrue(TestUtils.FilesAreEqual(cryptedFilePathOut, cryptedFilePathExpected), "File " + cryptedFilePathOut + " are not equals " + cryptedFilePathExpected);
            File.Delete(cryptedFilePathOut);
            File.Delete(clearFilePathToElab);
        }

        [TestMethod]
        public void TestMethodKritpStr2()
        {
            string fileNameEncryptedExpected = "Fx.8zl.reuhwu";
            string fileNameEncryptedActual = MOD_PRG_UTILS.KritpStr2("KR.NET.csproj", "LIVIO");
            Assert.AreEqual(fileNameEncryptedExpected, fileNameEncryptedActual);
        }

        [TestMethod]
        public void TestMethodgetKey()
        {
            string workKLog = strDirBase + "\\klog.txt";
            string strGet = MOD_PRG_UTILS.getKey(workKLog, this.testKey);
            Assert.AreEqual(klogKey, strGet);
        }
    }
}
