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

            string strkey = MOD_KLOG.CaricaLogFile(testKey, strDirBase + "\\klog.txt");
            Assert.AreNotEqual("", strkey);
        }

        [TestMethod]
        public void TestMethodsLoadFileAndListBox()
        {
            MOD_KLOG.LoadIntoList(lst, "", "");
            string strKLogAllE = strDirBase +"\\klog_ALLE.txt";
            if (!File.Exists(strKLogAllE))
            {
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt", strKLogAllE, strDirBaseCrypt, dateKLog, "_E");
            }
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strKLogAllE) , TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodCambiaStato()
        {
            MOD_KLOG.CambiaStato("" , "" , "D");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_ALLD_klog.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_klog.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "K", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_klog.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("D", "_", "E");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_klog.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "D" , @"D:\Root\Working\Kudalpt2019\KRTest\Crypt\Dir1");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_Dir1_klog.txt"), TestUtils.LastMessage);
            MOD_KLOG.CambiaStato("", "", "");
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_Dir1_cambio_klog.txt"), TestUtils.LastMessage);
        }

        [TestMethod]
        public void TestMethodIsStato()
        {
            MOD_KLOG.LoadIntoList(lst, "", "");
            string stato = MOD_KLOG.IsStato(@"D:\Root\Working\Kudalpt2019\KRTest\Crypt\Dir1");
            Assert.AreEqual("E" ,stato);
        }
    }
}
