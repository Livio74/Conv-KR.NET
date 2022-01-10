using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace KRTest
{
    [TestClass]
    public class UnitTestModKLog
    {
        [TestMethod]
        public void TestMethodsLoadFileAndListBox() {
            string key = "LIVIO";
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
            ListBox lst = new ListBox();
            string strkey = MOD_KLOG.CaricaLogFile(key, strDirBase + "\\CryptCrypt\\klog.txt");
            Assert.AreNotEqual("", strkey);
            MOD_KLOG.LoadIntoList(lst, "", "");
            Assert.AreEqual(3, lst.Items.Count);
            Assert.AreEqual(strDirBase + @"\Crypt:KE", lst.Items[0]);
            Assert.AreEqual(strDirBase + @"\Crypt\Dir1:KE", lst.Items[1]);
            Assert.AreEqual(strDirBase + @"\Crypt\Dir1\Dir2:KE", lst.Items[2]);
        }
    }
}
