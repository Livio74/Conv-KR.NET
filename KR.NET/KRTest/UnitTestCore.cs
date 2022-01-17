using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace KRTest
{
    [TestClass]
    public class UnitTestCore
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void First()
        {
            string strDirBase = (string)TestContext.Properties["WorkTestRoot"];
            string strProjectDir = TestUtils.ProjectDir();
            string strDirBaseCrypt = strDirBase + "\\CryptDir";
            string dateKLog = (string)TestContext.Properties["klogDate"];
            string testKey = (string)TestContext.Properties["testKey"];
            if (! Directory.Exists(strDirBaseCrypt)) {
                //Copia di tutto il progetto escluso file generati
                TestUtils.CopyDirectoryWithExclude(strProjectDir, strDirBaseCrypt + "\\KR.NET", ".vs,TestResults,packages");
                string strVB6ProjectDir = TestUtils.VB6ProjectDir();
                TestUtils.CopyDirectory(strVB6ProjectDir, strDirBaseCrypt + "\\Kripter", true);
                TestUtils.CopyDirectory(strDirBaseCrypt, strDirBase + "\\ClearDir", true);
                //Creazione dei file klog.txt
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt" , strDirBaseCrypt + "\\klog.txt", strDirBaseCrypt, dateKLog, "_E" , true);
                //Esecuzione progesso KR.exe
                File.Copy(strDirBase + "\\ClearDir\\Kripter\\kr.exe" , strDirBase + "\\kr.exe");
                Process krProcess = Process.Start(strDirBase + "\\kr.exe", strDirBaseCrypt + " " + testKey);
                krProcess.WaitForExit(600000);
            }
        }
    }
}
