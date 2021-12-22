using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KR.NET;

namespace KRTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
            strDirBase = "D:";
            string[] strListFD = new string[5000]; int intNumLV1 = 0;
            MOD_UTILS_SO.ListaFileEDirs(strDirBase, strListFD, out intNumLV1);
            Console.Out.WriteLine("Inizio Dir List : "+ strDirBase);
            for (int i = 0; i < intNumLV1; i++)
            {
                Console.Out.WriteLine(strListFD[i]);
            }
            Console.Out.WriteLine("Fine Dir List : " + strDirBase);
            ///MOD_UTILS_SO.SalvaListaFile(strDirBase + "out.txt", strDirBase + "Crypt\\", "", "klog.txt");

        }
    }
}
