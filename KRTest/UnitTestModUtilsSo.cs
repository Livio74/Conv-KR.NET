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
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
            string[] strListFD = new string[5000]; int intNumLV1 = 0;
            MOD_UTILS_SO.ListaFileEDirs(strDirBase, strListFD, out intNumLV1);
            Console.Out.WriteLine("Inizio Dir List : " + strDirBase);
            Assert.AreEqual(5 , intNumLV1);
            Assert.AreEqual("Crypt", strListFD[0]);
            Assert.AreEqual("out.txt", strListFD[1]);
            Assert.AreEqual("outCfr.txt", strListFD[2]);
            Assert.AreEqual("UnitTestModUtilsSo.cs", strListFD[3]);
            Assert.AreEqual("Varie.txt", strListFD[4]);
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
            string FileOut = DirBase + "out.txt";
            string FileOutCfr = DirBase + "outCfr.txt";
            MOD_UTILS_SO.SalvaListaFile(FileOut, DirBase + "Crypt\\", "", "klog.txt");
            string FileOutString = File.ReadAllText(FileOut);
            string FileOutCfrString = File.ReadAllText(FileOutCfr);
            Assert.AreEqual(FileOutCfrString, FileOutString);
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
