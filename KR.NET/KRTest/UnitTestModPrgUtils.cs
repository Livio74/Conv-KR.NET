using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KRTest
{
    [TestClass]
    public class UnitTestModPrgUtils
    {

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethodKriptp()
        {
            string key = "LIVIO";
            string strDirBase = (string) TestContext.Properties["WorkTestRoot"];
            string clearFilePathToElabOrig = strDirBase + "\\Resources\\Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf";
            string clearFilePathToElab = strDirBase + "\\Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf";
            string clearFilePathExpected = strDirBase + "\\CryptDecrypt\\Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf";
            string cryptedFilePathToElab = strDirBase + "\\lrhqixs DZ9GV0M 9K98 CTEpcsgfi_eEkoorcgi_Xs8P9GLèPBN6B27l_9N60KN7V0U158è.qrw";
            string cryptedFilePathExpected = strDirBase + "\\CryptCrypt\\lrhqixs DZ9GV0M 9K98 CTEpcsgfi_eEkoorcgi_Xs8P9GLèPBN6B27l_9N60KN7V0U158è.qrw";
            string cryptedFilePathOut = clearFilePathToElab + ".krp";

            File.Copy(clearFilePathToElabOrig, clearFilePathToElab);
            MOD_PRG_UTILS.Kriptp(clearFilePathToElab, key);
            Assert.IsTrue(TestUtils.FilesAreEqual(cryptedFilePathToElab, cryptedFilePathExpected), "File " + cryptedFilePathToElab + " are not equals " + cryptedFilePathExpected);
            MOD_PRG_UTILS.Kriptp(cryptedFilePathToElab, key);
            Assert.IsTrue(TestUtils.FilesAreEqual(clearFilePathToElab, clearFilePathExpected), "File " + clearFilePathToElab + " are not equals " + clearFilePathExpected);
            MOD_PRG_UTILS.Kriptp(clearFilePathToElab, key, cryptedFilePathOut);
            Assert.IsTrue(TestUtils.FilesAreEqual(cryptedFilePathOut, cryptedFilePathExpected), "File " + cryptedFilePathOut + " are not equals " + cryptedFilePathExpected);
            File.Delete(cryptedFilePathOut);
        }

        [TestMethod]
        public void TestMethodKritpStr2()
        {
            string fileNameEncryptedExpected = "lrhqixs DZ9GV0M 9K98 CTEpcsgfi_eEkoorcgi_Xs8P9GLèPBN6B27l_9N60KN7V0U158è.qrw";
            string fileNameEncryptedActual = MOD_PRG_UTILS.KritpStr2("Tampone COVID19 2021 Documento_sanitario_FRNLVI74L08C573W_20210829130444.pdf" , "LIVIO");
            Assert.AreEqual(fileNameEncryptedExpected, fileNameEncryptedActual);
        }

        [TestMethod]
        public void TestMethodgetKey()
        {
            string strKey = "LIVIO";
            string strDirBase = "D:\\Root\\Working\\Kudalpt2019\\KRTest";
            string strGet = MOD_PRG_UTILS.getKey(strDirBase + "\\CryptCrypt\\klog.txt", strKey);
            Assert.AreEqual("2B9O0725MaB7161P082KD", strGet);
        }
    }
}
