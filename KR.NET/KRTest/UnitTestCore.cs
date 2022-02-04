using KRLib.NET;
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

        string strDirBase = null;
        string strProjectDir = null;
        string strDirBaseCrypt = null;
        string dateKLog = null;
        string testKey = null;
        string klogKey = null;

        [TestInitialize]
        public void TestInitialize()
        {
            strDirBase = TestUtils.getWorkTestRoot(TestContext);
            strProjectDir = TestUtils.ProjectDir();
            strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            dateKLog = TestUtils.getKlogDate(TestContext);
            testKey = TestUtils.getTestKey(TestContext);
            klogKey = TestUtils.getKlogKey(TestContext);
        }

        [TestMethod]
        public void First()
        {
            Assert.AreNotEqual("", strDirBase, "E' possibile che non sia stato associato il corretto test setting file");
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

        [TestMethod]
        public void testMethodCheckSystemOrCriticalFolder()
        {
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("C:\\"), "cartella C:\\ non bloccata");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("D:\\"), "cartella D:\\ non bloccata");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("E:\\"), "cartella E:\\ non bloccata");
            string windowFolder = Environment.GetEnvironmentVariable("SystemRoot");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(windowFolder), "cartella Windows non bloccata");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("C:\\Windows"), "cartella Windows non bloccata");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("C:\\programmi"), "cartella Programmi non bloccata");
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder("C:\\programm files"), "cartella Programmi non bloccata");
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(programFilesFolder), "cartella Programmi X 86 non bloccata");
            string programFilesFolderX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(programFilesFolderX86), "cartella Programmi X 86 non bloccata");
            string commonProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(commonProgramFilesFolder), "cartella File programmi comune non bloccata");
            string commonProgramFilesX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(commonProgramFilesX86Folder), "cartella File programmi comune X86 non bloccata");
            string commonProgramsFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(commonProgramsFolder), "cartella Comune Programmi non bloccata");
            string systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(systemFolder), "cartella System non bloccata");
            string systemX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(systemX86Folder), "cartella System X86 non bloccata");
            string usersFolder = "C:\\Users";
            Assert.IsTrue(STATICUTILS.CheckSystemOrCriticalFolder(usersFolder), "cartella utenti non bloccata");
            string DocumentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Assert.IsFalse(STATICUTILS.CheckSystemOrCriticalFolder(DocumentFolder), "cartella Documenti bloccata");
            string DesktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Assert.IsFalse(STATICUTILS.CheckSystemOrCriticalFolder(DesktopFolder), "cartella Desktop bloccata");
        }
    }
}
