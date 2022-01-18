using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Windows.Forms;

namespace KRTest
{
    [TestClass]
    public class UnitTestModKLog
    {
        public TestContext TestContext { get; set; }

        string strDirBase = null;
        string strProjectDir = null;
        string strDirBaseCrypt = null;
        string dateKLog = null;
        string testKey = null;
        string klogKey = null;
        ListBox lst = new ListBox();

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
        public void TestMethodsLoadFileAndListBox()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBase + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALL_E.txt", "_E");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALL_E.txt") , TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodCambiaStato()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.CambiaStato("" , "" , "D");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALLKD.txt", "KD");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKD.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALLKE.txt", "KE");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "_", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "D" , strDirBaseCrypt + "\\KR.NET\\KRTest");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt" , strDirBase + "\\klog_dir1.txt", "KE", "KD");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_dir1.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt", strDirBase + "\\klog_dir1_cambio.txt", "KD", "KE");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_dir1_cambio.txt"), TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodIsStato()
        {
            MOD_KLOG.LoadIntoList(lst, "", "");
            string stato = MOD_KLOG.IsStato(strDirBaseCrypt + "\\KR.NET\\KRTest");
            Assert.AreEqual("E" ,stato);
        }

        [TestMethod]
        public void TestMethodSetStato()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.SetStato(strDirBaseCrypt + "\\KR.NET\\KRTest");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt", strDirBase + "\\klog_SetStato.txt", "_E", "KE");
            TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_SetStato.txt");
        }

        private void createFileWithFSList(string klogOut , string status)
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt", klogOut, strDirBaseCrypt, dateKLog, status);
            }
        }

        private void createFileWithFSList_status(string klogName , string klogOut, string status, string status2, string status3 = "" , string status4 = "")
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\" + klogName, klogOut, strDirBaseCrypt, dateKLog, status, "" , status2, status3, status4);
            }
        }
    }
}
