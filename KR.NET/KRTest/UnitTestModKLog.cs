using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Windows.Forms;
using KRLib.NET;


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
            strDirBase = TestUtils.getWorkTestRoot(TestContext);
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            dateKLog = TestUtils.getKlogDate(TestContext);
            testKey = TestUtils.getTestKey(TestContext);
            klogKey = TestUtils.getKlogKey(TestContext);
        }

        [TestMethod]
        public void TestMethodsLoadFileAndListBox()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBase + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALL_E.txt", "_E");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALL_E.txt", strDirBase + "\\klog_ALL_E_Attuale.txt") , TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodCambiaStato()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.CambiaStato("" , "" , "D");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALLKD.txt", "KD");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKD.txt", strDirBase + "\\klog_ALL_D_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALLKE.txt", "KE");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt", strDirBase + "\\klog_ALL_E_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt", strDirBase + "\\klog_ALL_E_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "_", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALLKE.txt", strDirBase + "\\klog_ALL_E_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "D" , strDirBaseCrypt + "\\KR.NET\\KRTest");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt" , strDirBase + "\\klog_dir1.txt", "", "KE", "KD");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_dir1.txt", strDirBase + "\\klog_dir1_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt", strDirBase + "\\klog_dir1_cambio.txt", "", "KD", "KE");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_dir1_cambio.txt", strDirBase + "\\klog_dir1_cambio_Attuale.txt"), TestUtils.LastMessage);
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
            MOD_KLOG.SetStato(strDirBaseCrypt + "\\KR.NET\\KRTest\\Properties");
            MOD_KLOG.SetStato(strDirBaseCrypt + "\\KR.NET\\KRTest\\Resources");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt", strDirBase + "\\klog_SetStato.txt", "", "KE", "_E");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_SetStato.txt" , strDirBase + "\\klog_SetStato_Attuale.txt"), TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodSetNewStato()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBase + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.SetNewStato(strDirBaseCrypt + "\\KR.NET\\KRTest", "K" , "D");
            MOD_KLOG.SetNewStato(strDirBaseCrypt + "\\KR.NET\\KRTest\\Properties", "K", "D");
            MOD_KLOG.SetNewStato(strDirBaseCrypt + "\\KR.NET\\KRTest\\Resources", "K", "D");
            MOD_KLOG.SetNewStato(strDirBaseCrypt + "\\KR.NET\\KRTest\\Resources", "K", "D");
            MOD_KLOG.SetNewStato(strDirBaseCrypt + "\\Kripter", "", "");
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_dir1.txt", strDirBase + "\\klog_SetNewStato.txt", "", "_E", "KD");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_SetNewStato.txt", strDirBase + "\\klog_SetNewStatoAttuale.txt"), TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodRigeneraLog()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            File.Copy(strDirBaseCrypt + "\\klog.txt", strDirBase + "\\klog_SAVE.txt");
            string dirProva = strDirBaseCrypt + "\\Prova";
            Directory.CreateDirectory(dirProva);
            File.WriteAllText(dirProva + "\\Prova.txt", "File di Prova");
            string newDir = strDirBaseCrypt + "_new";
            if (!Directory.Exists(newDir))
            {
                TestUtils.CopyDirectory(strDirBaseCrypt, newDir, true);
            } else
            {
                File.Delete(newDir + "\\klog.txt");
                File.Copy(strDirBaseCrypt + "\\klog.txt", newDir + "\\klog.txt");
            }
            MOD_KLOG.RigeneraLog(testKey, strDirBaseCrypt, strDirBaseCrypt + "\\klog.txt", strDirBaseCrypt);
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList_status("klog_ConProva.txt", strDirBase + "\\klog_ConProva.txt", "", "KE", "_E");
            bool isEqualCheckProva = TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ConProva.txt", strDirBase + "\\klog_ConProva_Attuale.txt");
            string CheckProvaMessage = TestUtils.LastMessage;
            strkey = MOD_KLOG.CaricaLogFile(testKey, newDir + "\\klog.txt");
            MOD_KLOG.RigeneraLog(testKey, newDir, newDir + "\\klog.txt", newDir);
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSListRoot_status(newDir, "klog_ConProva.txt", strDirBase + "\\klog_ConProvaDir2.txt", "", "KE", "_E");
            bool isEqualCheckProvaDir2 = TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ConProvaDir2.txt", strDirBase + "\\klog_ConProvaDir2_Attuale.txt");
            string CheckProvaMessageDir2 = TestUtils.LastMessage;
            File.Delete(strDirBaseCrypt + "\\klog.txt");
            File.Move(strDirBase + "\\klog_SAVE.txt" , strDirBaseCrypt + "\\klog.txt");
            File.Delete(dirProva + "\\Prova.txt");
            Directory.Delete(dirProva);
            Assert.IsTrue(isEqualCheckProva, CheckProvaMessage);
            Assert.IsTrue(isEqualCheckProvaDir2, CheckProvaMessageDir2);
        }

        [TestMethod]
        public void TestMethodsMixed1()
        {
            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBase + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.LoadIntoList(lst, "", "");
            createFileWithFSList(strDirBase + "\\klog_ALL_E.txt", "_E");
            MOD_KLOG.LoadIntoList(lst, "D", "");
            Assert.AreEqual(0, lst.Items.Count);
            MOD_KLOG.LoadIntoList(lst, "E", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALL_E.txt", strDirBase + "\\klog_ALL_E_Attuale.txt"), TestUtils.LastMessage);
            MOD_KLOG.LoadIntoList(lst, "", "K");
            Assert.AreEqual(0, lst.Items.Count);
            MOD_KLOG.LoadIntoList(lst, "", "_");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\klog_ALL_E.txt", strDirBase + "\\klog_ALL_E_Attuale.txt"), TestUtils.LastMessage);
        }

        private void createFileWithFSList(string klogOut , string status)
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt", klogOut, strDirBaseCrypt, dateKLog, status);
            }
        }

        private void createFileWithFSList_status(string klogName , string klogOut, string dateKLogToSet, string status, string status2, string status3 = "" , string status4 = "")
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\" + klogName, klogOut, strDirBaseCrypt, dateKLogToSet, status, "" , status2, status3, status4);
            }
        }

        private void createFileWithFSListRoot_status(string dirRoot , string klogName, string klogOut, string dateKLogToSet, string status, string status2, string status3 = "", string status4 = "")
        {
            if (!File.Exists(klogOut))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\" + klogName, klogOut, dirRoot, dateKLogToSet, status, "", status2, status3, status4);
            }
        }
    }
}

