using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;

namespace KRTest
{
    class TestUtils
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
            bool isEqual = false;
            string [] fileLines = File.ReadAllLines(comparingFilePath);
            if (fileLines.Length == lst.Items.Count)
            {
                int i = 0; isEqual = true;
                string listItem = "";
                while (i < fileLines.Length && isEqual)
                {
                    if (lst.Items[i] is String)
                    {
                        listItem = (string)lst.Items[i];
                        if (listItem.Equals(fileLines[i]))
                        {
                            i++;
                        }
                        else
                        {
                            isEqual = false;
                        }
                    } else
                    {
                        listItem = "NOT STRING: " + lst.Items[i].ToString();
                        isEqual = false;
                    }
                } 
                if (!isEqual)
                {
                    lastMessage = String.Format("element {0} not equals between file line {1} and ListBox element {2}", i, fileLines[i], listItem);
                }
            } else
            {
                isEqual = false;
                lastMessage = String.Format("listBox elements count {0} not equals to file lines count {1}" , lst.Items.Count, fileLines.Length);
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
    }
}
