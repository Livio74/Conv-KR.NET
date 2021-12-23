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
            Assert.AreEqual(2 , intNumLV1);
            Assert.AreEqual("Crypt" , strListFD[0]);
            Assert.AreEqual("Varie.txt" , strListFD[1]);
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
    }
}
