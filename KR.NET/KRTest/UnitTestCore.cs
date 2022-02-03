﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string strDirBase = TestUtils.getWorkTestRoot(TestContext);
            Assert.AreNotEqual("", strDirBase, "E' possibile che non sia stato associato il corretto test setting file");
            string strProjectDir = TestUtils.ProjectDir();
            string strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            string dateKLog = TestUtils.getKlogDate(TestContext);
            string testKey = TestUtils.getTestKey(TestContext);
            string klogKey = TestUtils.getKlogKey(TestContext);
            if (! Directory.Exists(strDirBaseCrypt)) {
                //Copia di tutto il progetto escluso file generati
                TestUtils.CopyDirectoryWithExclude(strProjectDir, strDirBaseCrypt + "\\KR.NET", ".vs,TestResults,packages,bin,obj,KRTest\\bin,KRTest\\obj");
                string dirRoot = Directory.GetParent(strProjectDir).FullName;
                TestUtils.CopyDirectoryWithExclude(dirRoot + "\\KRLib.NET", strDirBaseCrypt + "\\KRLib.NET", ".vs,TestResults,packages,bin,obj,KRTest\\bin,KRTest\\obj");
                TestUtils.CopyDirectoryWithExclude(dirRoot + "\\KR_UTILS", strDirBaseCrypt + "\\KR_UTILS", ".vs,TestResults,packages,bin,obj,KRTest\\bin,KRTest\\obj");
                TestUtils.CopyDirectoryWithExclude(dirRoot + "\\KRDE", strDirBaseCrypt + "\\KRDE", ".vs,TestResults,packages,bin,obj,KRTest\\bin,KRTest\\obj");
                string strVB6ProjectDir = TestUtils.VB6ProjectDir();
                TestUtils.CopyDirectory(strVB6ProjectDir, strDirBaseCrypt + "\\Kripter", true);
                TestUtils.CopyDirectory(strDirBaseCrypt, strDirBase + "\\ClearDir", true);
                //Creazione dei file klog.txt
                TestUtils.copyKLog(strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\klog.txt" , strDirBaseCrypt + "\\klog.txt", strDirBaseCrypt, dateKLog, "_E" , klogKey);
                File.Copy(strDirBaseCrypt + "\\klog.txt", strDirBase + "\\klog.txt");
                //Esecuzione progesso KR.exe
                File.Copy(strDirBase + "\\ClearDir\\Kripter\\kr.exe" , strDirBase + "\\kr.exe");
                Process krProcess = Process.Start(strDirBase + "\\kr.exe", strDirBaseCrypt + " " + testKey);
                krProcess.WaitForExit(600000);
            }
        }
    }
}
