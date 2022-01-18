using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KR.NET;
using System.IO;

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
            strDirBase = (string)TestContext.Properties["WorkTestRoot"];
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = strDirBase + "\\CryptDir";
            dateKLog = (string)TestContext.Properties["klogDate"];
            testKey = (string)TestContext.Properties["testKey"];
            klogKey = (string)TestContext.Properties["klogKey"];
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
            string DirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest\\";
            string FileOut = DirBase + "CryptDecrypt_FileList.txt";
            string FileOutCfr = DirBase + "CryptDecrypt_FileList_Expected.txt";
            string Chiave = "LIVIO";
            MOD_UTILS_SO.SalvaListaFile(FileOut, DirBase + "CryptDecrypt", Chiave, "klog.txt");
            Assert.IsTrue(TestUtils.FilesAreEqual(FileOut, FileOutCfr), "File " + FileOutCfr + " are not equals " + FileOutCfr);
        }

        [TestMethod]
        public void TestMethodsSetFileDateTime()
        {
            string FileOut = strDirBase + "out.txt";
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
