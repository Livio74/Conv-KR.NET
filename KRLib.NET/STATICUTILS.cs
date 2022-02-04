using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRLib.NET
{
    public class STATICUTILS
    {

        public static string EventuallyRemoveDoubleQuotes(string inString)
        {
            string outstring = inString;
            if (inString.Length > 1)
            {
                if (inString[0] == '\"')
                {
                    if (inString[inString.Length - 1] == '\"')
                    {
                        outstring = inString.Substring(1, inString.Length - 2);
                    }
                }
            }
            return outstring;
        }

        public static Boolean CheckSystemOrCriticalFolder(string folder)
        {
            bool isSystemOrCritical = false;
            string windowFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string programFolder = "C:\\Progra";
            string programFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programFilesFolderX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string commonProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            string commonProgramFilesX86Folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);
            string commonProgramsFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
            string userRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!"".Equals(userRootFolder))
                userRootFolder = Directory.GetParent(userRootFolder).FullName;
            string folderUpper = folder.ToUpper();
            if ("".Equals(windowFolder))
                throw new Exception("Empty Windows folder");
            if ("".Equals(programFolder))
                throw new Exception("Empty Program folder");
            if ("".Equals(commonProgramFilesFolder))
                throw new Exception("Empty common Program Files Folder");
            if ("".Equals(commonProgramFilesX86Folder))
                throw new Exception("Empty common Program Files X86 Folder");
            if ("".Equals(commonProgramsFolder))
                throw new Exception("Empty common Program Folder");
            if ("".Equals(userRootFolder))
                throw new Exception("Empty Users Folder");

            if (folder.Length == 2 && folder[1] == ':')
                isSystemOrCritical = true;
            if (folder.Length == 3 && folder[1] == ':'  && folder[2] =='\\')
                isSystemOrCritical = true;
            if ("C:\\".Equals(folder))
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(windowFolder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(programFolder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(programFilesFolder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(programFilesFolderX86.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(commonProgramFilesFolder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(commonProgramFilesX86Folder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.IndexOf(commonProgramsFolder.ToUpper()) == 0)
                isSystemOrCritical = true;
            else if (folderUpper.Equals(userRootFolder.ToUpper()))
                isSystemOrCritical = true;

            return isSystemOrCritical;
        }
    }
}
