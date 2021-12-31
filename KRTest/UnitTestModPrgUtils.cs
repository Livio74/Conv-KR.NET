using KR.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KRTest
{
    [TestClass]
    public class UnitTestModPrgUtils
    {
        [TestMethod]
        public void TestMethodKriptp()
        {
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
            MOD_PRG_UTILS.Kriptp(strDirBase + "\\Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf" , "LIVIO");
        }

        [TestMethod]
        public void TestMethod()
        {
            string fileNameEncryptedExpected = "lrhqixs DZ9GV0M 9K98 CTEpcsgfi_eEkoorcgi_Xs8P9GLèPBN6B27l_9N60KN7V0U158è.qrw";
            string fileNameEncryptedActual = MOD_PRG_UTILS.KritpStr2("Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf" , "LIVIO");
            Assert.AreEqual(fileNameEncryptedExpected, fileNameEncryptedActual);
        }

        
    }
}
