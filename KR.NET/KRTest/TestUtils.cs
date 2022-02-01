using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using KRLib.NET;
using System.Security.AccessControl;

namespace KRTest
{
    public class TestUtils
    {
        const int BYTES_TO_READ = sizeof(Int64);

        private static string lastMessage = "";

        public static string LastMessage { get => lastMessage; }

        public static bool FilesAreEqual(string firstFilePath, string secondFilePath)
        {
            lastMessage = "";
            FileInfo first = new FileInfo(firstFilePath);
            FileInfo second = new FileInfo(secondFilePath);
            if (first.Length != second.Length)
                return false;


            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }
            return true;
        }

        public static bool CheckListBoxWithTextFile(ListBox lst , string comparingFilePath)
        {
            string [] fileLines = File.ReadAllLines(comparingFilePath);
            string[] lstStringArray = lst.Items.OfType<string>().ToArray();
            string messageItemNotEqual = "element {0} not equals between ListBox element {1} and file line {2}";
            string messageCountNotEqual = "listBox elements count {0} not equals to file lines count {1}";
            return Check2StringArraysEquals(lstStringArray, fileLines, messageItemNotEqual, messageCountNotEqual);
        }

        public static bool CheckStringArrayWithTextFile(string[] stringArray , string comparingFilePath, string saveStringArrayFile, int length = -1)
        {
            string[] fileLines = File.ReadAllLines(comparingFilePath);
            string messageItemNotEqual = "element {0} not equals between array element {1} and file line {2}";
            string messageCountNotEqual = "Array elements count {0} not equals to file lines count {1}";
            bool isEqual = Check2StringArraysEquals(stringArray, fileLines, messageItemNotEqual, messageCountNotEqual, length);
            if (!isEqual)
            {
                saveStringArrayToFile(stringArray, saveStringArrayFile, length);
            }
            return isEqual;
        }

        private static void saveStringArrayToFile(string[] stringArray, string saveStringArrayFile, int length = -1)
        {
            if (length < 0)
                File.WriteAllLines(saveStringArrayFile, stringArray);
            else
            {
                string[] stringArray2 = new string[length];
                for (int i = 0; i < length; i++)
                    stringArray2[i] = stringArray[i];
                File.WriteAllLines(saveStringArrayFile, stringArray2);
            }
        }

        public static bool TextFilesAreEqual(string sourceTextFile, string destinationTextFile, string encodingSourceString = "", string encodingDestinationString = "")
        {
            string[] textFileLine1 = null;
            if ("".Equals(encodingSourceString))
                textFileLine1 = File.ReadAllLines(sourceTextFile);
            else
            {
                Encoding encodingSource = Encoding.GetEncoding(encodingSourceString);
                textFileLine1 = File.ReadAllLines(sourceTextFile, encodingSource);
            }
            string[] textFileLine2 = null;
            if ("".Equals(encodingDestinationString))
                textFileLine2 = File.ReadAllLines(destinationTextFile);
            else
            {
                Encoding encodingDestination = Encoding.GetEncoding(encodingDestinationString);
                textFileLine2 = File.ReadAllLines(sourceTextFile, encodingDestination);
            }
            string messageItemNotEqual = "element {0} not equals between text file line 1 {1} and text file line 2 {2}";
            string messageCountNotEqual = "text file 1 line count {0} not equals to text file 2 line count {1}";
            return Check2StringArraysEquals(textFileLine1, textFileLine2, messageItemNotEqual, messageCountNotEqual);
        }

        public static bool Check2StringArraysEquals(string[] stringArray1 , string[] stringArray2, string messageItemNotEqual, string messageCountNotEqual , int length1 = -1, int length2 = -1)
        {
            bool isEqual = false;
            if (length1 < 0) length1 = stringArray1.Length;
            if (length2 < 0) length2 = stringArray2.Length;
            if (length1 == length2)
            {
                int i = 0; isEqual = true;
                while (i < length2 && isEqual)
                {
                    if (stringArray1[i].Equals(stringArray2[i]))
                    {
                        i++;
                    }
                    else
                    {
                        isEqual = false;
                    }
                }
                if (!isEqual)
                {
                    lastMessage = String.Format(messageItemNotEqual, i, stringArray1[i], stringArray2[i]);
                }
            }
            else
            {
                isEqual = false;
                lastMessage = String.Format(messageCountNotEqual, length1, length2);
            }
            return isEqual;
        }

        public static bool CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
            return true;
        }

        public static bool CopyDirectoryWithExclude(string sourceDir, string destinationDir, string exceptionList)
        {
            return CopyDirectoryWithExclude(sourceDir, destinationDir, exceptionList, sourceDir);
        }
        
        private static bool CopyDirectoryWithExclude(string sourceDir, string destinationDir, string exceptionList, string sourceDirRoot)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                if (IsNotExcluded(file.FullName, exceptionList, sourceDirRoot)) file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                if (IsNotExcluded(subDir.FullName, exceptionList, sourceDirRoot))
                    CopyDirectoryWithExclude(subDir.FullName, newDestinationDir, exceptionList , sourceDirRoot);
            }
            return true;
        }

        private static bool IsNotExcluded(string fullpath, string exceptionList, string sourceDirRoot)
        {
            bool excluded = false;
            foreach (string exceptionString in exceptionList.Split(','))
            {
                if (fullpath.IndexOf(sourceDirRoot + "\\" + exceptionString) == 0) excluded = true;
            }
            return !excluded;
        }

        public static string ProjectDir()
        {
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT bin directory (ie ../bin/)
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            // This will get the current PROJECT directory
            projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            return projectDirectory;
        }

        public static string VB6ProjectDir()
        {
            return Directory.GetParent(ProjectDir()).FullName + "\\Kripter";
        }

        public static bool copyKLog(string klogResourceFile , string klogDestinationFile , string rootDir, string dateKLog, string status, string klogdKey = "", string status2 = "", string status3 = "", string status4 = "")
        {
            string[] klogResourcesList = File.ReadAllLines(klogResourceFile);
            Assert.AreNotEqual(0, klogResourcesList.Length);
            string[] klogFileList;
            if ("".Equals(klogdKey))
                klogFileList = new string[klogResourcesList.Length];
            else
            {
                klogFileList = new string[klogResourcesList.Length + 1];
                klogFileList[0] = "\"" + klogdKey + "\"";
            }
            for (int i = 0; i < klogResourcesList.Length; i++)
            {
                if ("".Equals(status2))
                {
                    if (i == 0)
                    {
                        if ("".Equals(klogdKey))
                            klogFileList[i] = rootDir + ":" + status;
                        else
                            klogFileList[i + 1] = "\"" + rootDir + ":" + status + "\"";
                    }
                    else
                    {
                        if ("".Equals(klogdKey))
                            klogFileList[i] = rootDir + klogResourcesList[i] + ":" + status;
                        else
                            klogFileList[i + 1] = "\"" + rootDir + klogResourcesList[i] + ":" + status + "\"";
                    }
                } else
                {
                    string newItem = klogResourcesList[i].Replace(":1", ":" + status);
                    newItem = newItem.Replace(":2", ":" + status2);
                    newItem = newItem.Replace(":3", ":" + status3);
                    newItem = newItem.Replace(":4", ":" + status4);
                    if (i == 0)
                    {
                        newItem = rootDir + newItem.Substring(1);
                    } else
                    {
                        newItem = rootDir + newItem;

                    }
                    if ("".Equals(klogdKey))
                        klogFileList[i] = newItem;
                    else
                        klogFileList[i + 1] = "\"" + newItem + "\"";
                }
            }
            File.WriteAllLines(klogDestinationFile, klogFileList);
            bool setit = true;
            if (!"".Equals(dateKLog))
                MOD_UTILS_SO.SetFileDateTime(klogDestinationFile, dateKLog);
            return setit;
        }
        public static bool copyFileListByCryptFileList(string cryptFileSource, string cryptFileDestination, string rootDir , int fileColumns)
        {
            return copyFileByCryptFileList(cryptFileSource, cryptFileDestination, rootDir, fileColumns);
        }

        public static bool copyCryptFileList(string cryptFileSource, string cryptFileDestination, string rootDir)
        {
            return copyFileByCryptFileList(cryptFileSource, cryptFileDestination, rootDir);
        }

        private static bool copyFileByCryptFileList(string cryptFileSource, string cryptFileDestination, string rootDir, int fileColumns = -1)
        {
            string[] cryptFileSourceList = File.ReadAllLines(cryptFileSource);
            string[] cryptFileDestinationList = new string[cryptFileSourceList.Length];
            Assert.AreNotEqual(0, cryptFileSourceList.Length);
            for (int i = 0; i < cryptFileSourceList.Length; i++)
            {
                if (fileColumns < 0 )
                {
                    if (cryptFileSourceList[i].IndexOf("\\\t") == 0)
                    {
                        cryptFileDestinationList[i] = rootDir + cryptFileSourceList[i].Substring(1);
                    }
                    else
                    {
                        cryptFileDestinationList[i] = rootDir + "\\" + cryptFileSourceList[i].Substring(1);
                    }
                } else
                {
                    string dir;
                    string file1;
                    string file2 = "";
                    int firstTab = cryptFileSourceList[i].IndexOf("\t");
                    if (firstTab >= 0)
                    {
                        dir = cryptFileSourceList[i].Substring(0, firstTab);
                        file1 = cryptFileSourceList[i].Substring(firstTab + 1);
                        int SecondTab = cryptFileSourceList[i].IndexOf("\t", firstTab + 1);
                        if (SecondTab >= 0)
                        {
                            file1 = cryptFileSourceList[i].Substring(firstTab + 1, SecondTab - firstTab - 1);
                            file2 = cryptFileSourceList[i].Substring(SecondTab + 1);
                        }
                    }
                    else
                    {
                        throw new Exception(" Row " + cryptFileSourceList[i] + " not properly formatted");
                    }
                    if ("\\".Equals(dir))
                    {
                        cryptFileDestinationList[i] = rootDir + dir.Substring(1) + "\\";
                    }
                    else
                    {
                        cryptFileDestinationList[i] = rootDir + "\\" + dir.Substring(1) + "\\";
                    }
                    if (fileColumns == 1)
                        cryptFileDestinationList[i] += file1;
                    else if (fileColumns == 2)
                        cryptFileDestinationList[i] += file2;
                }
            }
            File.WriteAllLines(cryptFileDestination, cryptFileDestinationList);
            return true;
        }

        public static string getWorkTestRoot(TestContext testContext)
        {
            string strDirBase = (string)testContext.Properties["WorkTestRoot"];
            if (!Directory.Exists(strDirBase))
                Directory.CreateDirectory(strDirBase);
            return strDirBase;
        }

        public static string getKlogDate(TestContext testContext)
        {
            return (string)testContext.Properties["klogDate"];
        }

        public static string getTestKey(TestContext testContext)
        {
            return (string)testContext.Properties["testKey"];
        }

        public static string getKlogKey(TestContext testContext)
        {
            return (string)testContext.Properties["klogKey"];
        }

        public static string getWorkTestCryptDir(TestContext testContext)
        {
            return getWorkTestRoot(testContext) + "\\CryptDir";
        }
    }
}
