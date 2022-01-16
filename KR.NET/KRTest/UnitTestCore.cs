using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            if (! Directory.Exists(strDirBaseCrypt)) {
                TestUtils.CopyDirectoryWithExclude(strProjectDir, strDirBaseCrypt + "\\KR.NET", ".vs,TestResults,packages");
                string strVB6ProjectDir = TestUtils.VB6ProjectDir();
                TestUtils.CopyDirectory(strVB6ProjectDir, strDirBaseCrypt + "\\Kripter", true);
            }
        }
    }
}
