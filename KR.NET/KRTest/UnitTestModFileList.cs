using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using KRLib.NET;
using System.Windows.Forms;

namespace KRTest
{
    [TestClass]
    public class UnitTestModFileList
    {
        public TestContext TestContext { get; set; }

        string strDirBase = null;
        string strProjectDir = null;
        string strDirBaseCrypt = null;
        string dateKLog = null;
        string testKey = null;
        string klogKey = null;
        ListBox lst = null;

        [TestInitialize]
        public void TestInitialize()
        {
            strDirBase = TestUtils.getWorkTestRoot(TestContext);
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            dateKLog = TestUtils.getKlogDate(TestContext);
            testKey = TestUtils.getTestKey(TestContext);
            klogKey = TestUtils.getKlogKey(TestContext);
            lst = new ListBox();
        }

        [TestMethod]
        public void TestAll_LoadAllFilesFromKLog()
        {
            Assert.AreNotEqual("", strDirBase, "E' possibile che non sia stato associato il corretto test setting file");
            string fileKey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            Assert.AreNotEqual("", fileKey);
            File.Copy(strDirBaseCrypt + "\\klog.txt", strDirBase + "\\klog_SAVE.txt");
            MOD_KLOG.RigeneraLog(testKey, strDirBaseCrypt, strDirBaseCrypt + "\\klog.txt" , strDirBaseCrypt);
            File.Delete(strDirBaseCrypt + "\\klog.txt");
            File.Move(strDirBase + "\\klog_SAVE.txt", strDirBaseCrypt + "\\klog.txt");
            createFileWithFSList(strDirBase + "\\klog_ALLKE_Genera.txt", "KE");
            MOD_KLOG.LoadIntoList(lst, "", "");
            bool klogIsEquals = TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE_Genera.txt", strDirBase + "\\klog_ALLKE_Genera_Attuale.txt");
            Assert.IsTrue(klogIsEquals, TestUtils.LastMessage);
            string strErr = MOD_FILE_LIST.Genera(1, strDirBaseCrypt, strDirBaseCrypt + "\\klog.txt");
            long fileSize = MOD_FILE_LIST.GetSize();
            string[] fileList = new string[fileSize];
            for (long i = 0; i < fileSize; i++)
            {
                fileList[i] = MOD_FILE_LIST.GetFile(i);
            }
            string FileOut = strDirBase + "\\CryptDir_FileList_Attuale.txt";
            string FileOutCfr = strDirBase + "\\CryptDir_FileList_Expected.txt";
            TestUtils.copyFileListByCryptFileList (strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\CryptDir_FileList.txt", FileOutCfr, strDirBase + "\\CryptDir", 1);
            bool listIsEquals = TestUtils.CheckStringArrayWithTextFile(fileList, strDirBase + "\\CryptDir_FileList_Expected.txt", FileOut);
            Assert.IsTrue(listIsEquals, TestUtils.LastMessage);
            createFileWithFSList(strDirBase + "\\klog_ALL_E_Genera.txt", "_E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            klogIsEquals = TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALL_E_Genera.txt", strDirBase + "\\klog_ALL_E_Genera_Attuale.txt");
            Assert.IsTrue(klogIsEquals, TestUtils.LastMessage);

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
