using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text;


// Conversione VB6 to C# di "D:\Root\Computername\Kudapc\e\DOCUMENTI\myPrograms\Visual Basic 6\Kripter\UTILS_SO.bas"
// Ad eccezione di 
//    strOut = strOut + strListLV1(i) & vbTab & strListFD(j) + vbTab + KritpStr2(strListFD(j), strChiave) + vbCrLf
// che per ora è 
//   strOut = strOut + strListLV1(i) & vbTab & strListFD(j) + vbTab + strListFD(j) + vbCrLf

namespace KRLib.NET
{
    public class MOD_UTILS_SO
    {

        public static void ListaFileEDirs(string strDir , string[] strLista , out int intNum)
        {
            intNum = 0;
            string strDirWithSlash = strDir;
            if (strDirWithSlash[strDirWithSlash.Length - 1] != '\\')
            {
                strDirWithSlash += '\\';
            }
            IEnumerable<String> fileDirList = Directory.GetFileSystemEntries(strDir, "*.*", SearchOption.TopDirectoryOnly);
            foreach(String fileDirItem in fileDirList) {
                if (fileDirItem.IndexOf(strDirWithSlash) == 0)
                {
                    strLista[intNum] = fileDirItem.Substring(strDirWithSlash.Length);
                    intNum = intNum + 1;
                } else
                {
                    throw new Exception("File/Dir " + fileDirItem + " not contain base dir " + strDir);
                }
            }
        }

        public static void ErrorLog(string strS, string StartupPath)
        {
            File.WriteAllText(StartupPath + "\\ErrorLog.xml" , strS);
        }

        public static void WriteErrorLog(string strS, string StartupPath)
        {
            File.AppendAllText(StartupPath + "\\ErrorLog.txt", strS);
        }

        public static string SalvaListaFile(string strFile , string strDir , string strChiave , string strFileLog)
        {
            string[] strListLV1 = new string[5000]; int intNumLV1 = 0;
            string[] strListLV2 = new string[5000]; int intNumLV2 = 0;
            string[] strListFD = new string[5000]; int intNums = 0;
            byte[] strOutUnicodeBytes = new byte[6000];
            byte[] strOutAsciiBytes = new byte[3000];
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            // 1. Inizializzazione
            string strOut = ""; int i = 0, j = 0;
            if (File.Exists(strFile)) {
                File.Delete(strFile);
            }
            strListLV1[0] = strDir; intNumLV1 = 1;
            // 2. Per ogni directory di un certo livello vengono generate le sottodirectory
            // -. e inserite nella lista di uscita lstListaDir ->lstFile
            // -. poi viene riportata in lstListaDir per il loop successivo
            FileStream inOutFile = File.Open(strFile, FileMode.Create, FileAccess.Write);
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
                                    if (strListLV1[i].Equals(strDir))
                                    {
                                        strOut = strOut + strListLV1[i] + "\\\t" + strListFD[j] + "\t" + MOD_PRG_UTILS.KritpStr2(strListFD[j], strChiave) + "\r\n";
                                    }
                                    else
                                    {
                                        strOut = strOut + strListLV1[i] + "\t" + strListFD[j] + "\t" + MOD_PRG_UTILS.KritpStr2(strListFD[j], strChiave) + "\r\n";
                                    }
                                }
                            }
                        }
                    }
                }
                //2.2 Eventuale salvataggio
                if (strOut.Length > 30000)
                {
                    strOutUnicodeBytes = Encoding.UTF8.GetBytes(strOut);
                    strOutAsciiBytes = Encoding.Convert(Encoding.UTF8, iso88591, strOutUnicodeBytes);
                    inOutFile.Write(strOutAsciiBytes, 0 , 3000);
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
                strOutUnicodeBytes = Encoding.UTF8.GetBytes(strOut);
                strOutAsciiBytes = Encoding.Convert(Encoding.UTF8, iso88591, strOutUnicodeBytes);
                inOutFile.Write(strOutAsciiBytes, 0, strOut.Length);
            }
            inOutFile.Close();
            return "";
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
        //TODO : si potrebbe trasformare string FileName , Datetime TheDate
        public static Boolean SetFileDateTime(string FileName , string TheDate)
        {
            Boolean setIt = false;
            DateTime DateOut = new DateTime();
            setIt = DateTime.TryParse(TheDate, out DateOut);
            if (setIt)
            {
                try
                {
                    File.SetLastWriteTime(FileName, DateOut);
                    setIt = true;
                }
                catch
                {
                    setIt = false;
                }
            }
            return setIt;
        }
    }

}
