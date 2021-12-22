using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

// Conversione VB6 to C# di "D:\Root\Computername\Kudapc\e\DOCUMENTI\myPrograms\Visual Basic 6\Kripter\UTILS_SO.bas"
// Ad eccezione di 
//    strOut = strOut + strListLV1(i) & vbTab & strListFD(j) + vbTab + KritpStr2(strListFD(j), strChiave) + vbCrLf
// che per ora è 
//   strOut = strOut + strListLV1(i) & vbTab & strListFD(j) + vbTab + strListFD(j) + vbCrLf

namespace KR.NET
{
    public class MOD_UTILS_SO
    {

        const int MAX_PATH = 260;

        const int GENERIC_WRITE = 0x40000000;
        const int FILE_SHARE_READ = 1;
        const int FILE_SHARE_WRITE = 2;
        const uint OPEN_EXISTING = 2;

        struct FILETIME
        {
            public int dwLowDateTime;
            public int dwHighDateTime;
        }

        struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            public string cFileName; //mite need marshalling, TCHAR size = MAX_PATH???
            public string cAlternateFileName; //mite need marshalling, TCHAR size = 14
        }

        struct SYSTEMTIME
        {
            public int wYear;
            public int wMonth;
            public int wDayOfWeek;
            public int wDay;
            public int wHour;
            public int wMinute;
            public int wSecond;
            public int wMillisecs;
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr FindFirstFile(IntPtr lpfilename, ref WIN32_FIND_DATA findfiledata);

        [DllImport("kernel32.dll")]
        static extern IntPtr FindClose(IntPtr pff);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetLastError();

        [DllImport("kernel32.dll")]
        static extern IntPtr FindNextFile(IntPtr hFindFile, ref WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll")]
        static extern IntPtr SetFileTime(IntPtr hFile, IntPtr MullP, FILETIME lpLastWriteTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr SystemTimeToFileTime(SYSTEMTIME lpSystemTime, FILETIME lpLastWriteTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr LocalFileTimeToFileTime(FILETIME lpLocalFileTime, FILETIME lpFileTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateFileA(
             [MarshalAs(UnmanagedType.LPStr)] string filename,
             [MarshalAs(UnmanagedType.U4)] FileAccess access,
             [MarshalAs(UnmanagedType.U4)] FileShare share,
             IntPtr securityAttributes,
             [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
             [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
             IntPtr templateFile);

        public static void ListaFileEDirs(string strDir , string[] strLista , out int intNum)
        {
            WIN32_FIND_DATA lpDett = new WIN32_FIND_DATA(); IntPtr hSearch, hNext, lngLastErr;
            Boolean bolTrovatoFile = true; string strFile = "";
            intNum = 0;
            string lpFileNameString = @"\\?\" + strDir + @"\*";
            lpFileNameString = @"\\?\" + strDir;

            IntPtr lpFileName = Marshal.StringToHGlobalAuto(lpFileNameString);

            hSearch = FindFirstFile(lpFileName, ref lpDett);
            //Correzione BUG che non c'era
            Int32 hSearchValue = hSearch.ToInt32();
            if (hSearchValue < 0)
            {
                bolTrovatoFile = false;

            } else if (hSearch == IntPtr.Zero)
            {
                bolTrovatoFile = false;
            }
            //Fine correzione
            if (bolTrovatoFile)
            {
                while (bolTrovatoFile)
                {
                    strFile = lpDett.cFileName;
                    intNum = intNum + 1;
                    strLista[intNum] = strFile;
                    hNext = FindNextFile(hSearch, ref lpDett);
                    if (hNext == IntPtr.Zero)
                    {
                        bolTrovatoFile = false;
                    }
                }
                FindClose(hSearch);
            }
        }

        public static void ErrorLog(string strS)
        {
            string appPath = Assembly.GetExecutingAssembly().Location;
            File.WriteAllText(appPath + "\\ErrorLog.xml" , strS);
        }

        public static void WriteErrorLog(string strS)
        {
            string appPath = Assembly.GetExecutingAssembly().Location;
            File.AppendAllText(appPath + "\\ErrorLog.txt", strS);
        }

        public static void SalvaListaFile(string strFile , string strDir , string strChiave , string strFileLog)
        {
            string[] strListLV1 = new string[5000]; int intNumLV1 = 0;
            string[] strListLV2 = new string[5000]; int intNumLV2 = 0;
            string[] strListFD = new string[5000]; int intNums = 0;
            // 1. Inizializzazione
            string strOut = ""; int i = 0, j = 0;
            if (File.Exists(strFile)) {
                File.Delete(strFile);
            }
            strListLV1[0] = strDir; intNumLV1 = 1;
            // 2. Per ogni directory di un certo livello vengono generate le sottodirectory
            // -. e inserite nella lista di uscita lstListaDir ->lstFile
            // -. poi viene riportata in lstListaDir per il loop successivo
            while (intNumLV1 > 0)
            {
                //2.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
                intNumLV2 = 0;
                for (i = 0; i < intNumLV1; i++)
                {
                    intNums = 0;
                    ListaFileEDirs(strListLV1[i], strListFD, out intNums);
                    for (j = 0; j < intNums; j++)
                    {
                        FileAttributes attr = File.GetAttributes(strListLV1[i] + "\\" + strListFD[j]);
                        if (strListFD[j].IndexOf(".") != 0)
                        {
                            if (attr.HasFlag(FileAttributes.Directory))
                            {
                                strListLV2[intNumLV2] = strListLV1[i] + "\\" + strListFD[j];
                                intNumLV2++;
                            }
                            else
                            {
                                if (!(strListFD[j].Equals(strFileLog)))
                                {
                                    strOut = strOut + strListLV1[i] + "\t" + strListFD[j] + "\t" + strListFD[j] + "\r\n";
                                    //TODO: 
                                    //strOut = strOut + strListLV1[i] + "\t" + strListFD[j] + "\t" + KritpStr2(strListFD(j), strChiave) + "\r\n";
                                }
                            }
                        }
                    }
                }
                //2.2 Eventuale salvataggio
                if (strOut.Length > 30000)
                {
                    File.AppendAllText(strFile, strOut); strOut = "";
                }
                //2.3 SUCC (Copia della lista di output come lista di input per il loop successivo
                for(i=0; i < intNumLV2; i++)
                {
                    strListLV1[i] = strListLV2[i];
                }
                intNumLV1 = intNumLV2;
            }
            if (strOut.Length > 0)
            {
                File.AppendAllText(strFile, strOut); strOut = "";
            }
        }

        public static Boolean ExistsFile(string strFile , string strDir = "")
        {
            Boolean exists = false;
            if ("".Equals(strDir))
            {
                exists = File.Exists(strFile);
            } else
            {
                exists = File.Exists(strDir + "\\" + strFile);
            }
            return exists;
        }
        public static Boolean SetFileDateTime(string FileName , string TheDate)
        {
            Boolean setIt = false;
            IntPtr lFileHnd, lRet;
            if ("".Equals(FileName)) return false;
            if (!(IsDate(TheDate)))  return false;
            FILETIME typFileTime = new FILETIME();
            FILETIME typLocalTime = new FILETIME();
            SYSTEMTIME typSystemTime = new SYSTEMTIME();
            DateTime now = new DateTime();
            typSystemTime.wYear = now.Year;
            typSystemTime.wMonth = now.Month;
            typSystemTime.wDay = now.Day;
            DayOfWeek nowDayOfWeek = now.DayOfWeek;
            typSystemTime.wDayOfWeek = (int) nowDayOfWeek;
            typSystemTime.wHour = now.Hour;
            if (File.Exists(FileName))
            {
                lRet = SystemTimeToFileTime(typSystemTime, typLocalTime);
                if (!(lRet == IntPtr.Zero))
                {
                    lRet = LocalFileTimeToFileTime(typLocalTime, typFileTime);
                    if (!(lRet == IntPtr.Zero))
                    {
                        lFileHnd = CreateFileA(FileName, FileAccess.Write, FileShare.Read | FileShare.Write, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
                        if (!(lFileHnd == IntPtr.Zero))
                        {
                            lRet = SetFileTime(lFileHnd, IntPtr.Zero, typFileTime);
                            if (!(lRet == IntPtr.Zero))
                            {
                                CloseHandle(lFileHnd);
                                setIt = true;
                            }
                        }
                    }
                }
            }
            return setIt;
        }

        private static Boolean IsDate(string TheDate)
        {
            DateTime DateOut = new DateTime();
            return DateTime.TryParse(TheDate, out DateOut);
        }
    }

}
