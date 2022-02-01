using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using KRLib.NET;


namespace KRTest
{
    [TestClass]
    public class UnitTestModUtilsSo
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
        public void TestMethodListaFileEDirs()
        {
            string dirToList = strDirBase + "\\ClearDir\\KR.NET";
            string[] strListFD = new string[5000]; int intNumLV1 = 0;
            MOD_UTILS_SO.ListaFileEDirs(dirToList, strListFD, out intNumLV1);
            bool isEqual = TestUtils.CheckStringArrayWithTextFile(strListFD, strDirBase + @"\\clearDir\KR.NET\KRTest\Resources\CleanDir_KR.NET.txt", intNumLV1);
            Assert.IsTrue(isEqual, TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodSalvaListaFile()
        {
            string FileOut = strDirBase + "\\CryptDir_FileList.txt";
            string FileOutCfr = strDirBase + "\\CryptDir_FileList_Expected.txt";
            TestUtils.copyCryptFileList(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\CryptDir_FileList.txt", FileOutCfr, strDirBase + "\\CryptDir");
            MOD_UTILS_SO.SalvaListaFile(FileOut, strDirBase + "\\CryptDir", testKey, "klog.txt");
            Assert.IsTrue(TestUtils.TextFilesAreEqual(FileOut, FileOutCfr, "ISO-8859-1"), TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodsSetFileDateTime()
        {
            string FileOut = strDirBase + "\\out.txt";
            createFileWithFSList(FileOut, "KE");
            DateTime dateFileOutBefore = File.GetLastWriteTime(FileOut);
            DateTime now = DateTime.Now;
            String nowString = now.ToLongDateString() + " " + now.ToLongTimeString();
            Boolean setit = MOD_UTILS_SO.SetFileDateTime(FileOut, nowString);
            Assert.IsTrue(setit, "Data file non modificata");
            DateTime dateFileOutAfter = File.GetLastWriteTime(FileOut);
            Assert.IsTrue(dateFileOutBefore < dateFileOutAfter , "Date not modified , same date");
            TimeSpan dateDiff = dateFileOutAfter.Subtract(now);
            Assert.IsTrue(dateDiff.TotalSeconds < 1 ,"Date not modified : diff to high");
       }

        private void createFileWithFSList(string klogOut, string status)
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt", klogOut, strDirBaseCrypt, dateKLog, status);
            }
        }
    }
}
