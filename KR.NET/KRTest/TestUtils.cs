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
    }
}
