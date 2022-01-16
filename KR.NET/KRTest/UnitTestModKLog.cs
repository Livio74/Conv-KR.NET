using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace KRTest
{
    [TestClass]
    public class UnitTestModKLog
    {
        string key = "LIVIO";
        string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
        ListBox lst = new ListBox();

        [TestInitialize]
        public void TestInitialize()
        {
            string strkey = MOD_KLOG.CaricaLogFile(key, strDirBase + "\\CryptCrypt\\klog.txt");
            Assert.AreNotEqual("", strkey);
        }

        [TestMethod]
        public void TestMethodsLoadFileAndListBox()
        {
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.IsTrue(TestUtils.CheckListBoxWithTextFile(lst, strDirBase + "\\Resources\\CryptCrypt_klog.txt") , TestUtils.LastMessage);
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
