﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using KRLib.NET;


namespace KRTest
{
    [TestClass]
    public class UnitTestModFileList
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestAll_LoadAllFilesFromKLog()
        {
            string strDirBase = TestUtils.getWorkTestRoot(TestContext);
            Assert.AreNotEqual("", strDirBase, "E' possibile che non sia stato associato il corretto test setting file");
            string strProjectDir = TestUtils.ProjectDir();
            string strDirBaseCrypt = TestUtils.getWorkTestCryptDir(TestContext);
            string dateKLog = TestUtils.getKlogDate(TestContext);
            string testKey = TestUtils.getTestKey(TestContext);
            string klogKey = TestUtils.getKlogKey(TestContext);
            string fileKey = MOD_KLOG.CaricaLogFile(testKey, strDirBaseCrypt + "\\klog.txt");
            Assert.AreNotEqual("", fileKey);
            File.Copy(strDirBaseCrypt + "\\klog.txt", strDirBase + "\\klog_SAVE.txt");
            MOD_KLOG.RigeneraLog(testKey, strDirBaseCrypt, strDirBaseCrypt + "\\klog.txt" , strDirBaseCrypt);
            string strErr = MOD_FILE_LIST.Genera(1, strDirBaseCrypt, strDirBaseCrypt + "\\klog.txt");
            long fileSize = MOD_FILE_LIST.GetSize();
            string[] fileList = new string[fileSize];
            for (long i = 0; i < fileSize; i++)
            {
                fileList[i] = MOD_FILE_LIST.GetFile(i);
            }
            File.Delete(strDirBaseCrypt + "\\klog.txt");
            File.Move(strDirBase + "\\klog_SAVE.txt", strDirBaseCrypt + "\\klog.txt");
            string FileOut = strDirBase + "\\CryptDir_FileList.txt";
            string FileOutCfr = strDirBase + "\\FileList_CryptDir.txt";
            TestUtils.copyFileListByCryptFileList (strDirBase + "\\ClearDir\\KR.NET\\KRTest\\Resources\\CryptDir_FileList.txt", FileOutCfr, strDirBase + "\\CryptDir", 1);
            bool listIsEquals = TestUtils.CheckStringArrayWithTextFile(fileList, strDirBase + "\\FileList_CryptDir.txt");
            if (!listIsEquals)
            {
                File.WriteAllLines(FileOut , fileList);
            }
            Assert.IsTrue(listIsEquals, TestUtils.LastMessage);
        }
    }
}
