using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KR.NET;
using System.IO;

namespace KRTest
{
    [TestClass]
    public class UnitTestModUtilsSo
    {
        [TestMethod]
        public void TestMethodListaFileEDirs()
        {
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest\\CryptDecrypt";
            string[] strListFD = new string[5000]; int intNumLV1 = 0;
            MOD_UTILS_SO.ListaFileEDirs(strDirBase, strListFD, out intNumLV1);
            Console.Out.WriteLine("Inizio Dir List : " + strDirBase);
            Assert.AreEqual(4 , intNumLV1);
            Assert.AreEqual("Dir1", strListFD[0]);
            Assert.AreEqual("klog.txt", strListFD[1]);
            Assert.AreEqual("ListaMovimenti (1).xlsx", strListFD[2]);
            Assert.AreEqual("Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf", strListFD[3]);
            for (int i = 0; i < intNumLV1; i++)
            {
                Console.Out.WriteLine(strListFD[i]);
            }
            Console.Out.WriteLine("Fine Dir List : " + strDirBase);

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
            string DirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest\\";
            string FileOut = DirBase + "out.txt";
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
    }
}
