using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KRLib.NET
{
    static public class MOD_MAIN
    {

        public static string G_strErr;
        public static bool G_bolEsisteLog;
        public static bool G_bolErrLog;
        public static long G_lng_NumFiles;
        public static string G_strFileLog;
        public static string G_strDirRoot;
        public static string G_strFileList;
        public static string G_strChiave;

        public static string getErrorMsg(string strErrCode)
        {
            string strErr = "";
            if ("OVERFLOW".Equals(strErrCode))
            {
                strErr = "Ci sono troppi file nella directory specificata";
            } else
            {
                strErr = "ERROR CODE : " + strErrCode;
            }
            return strErr;
        }

        private static string GetParam(int v, string[] args)
        {
            string param1 = ""; string param2 = "";
            if (args.Length > 0)
                param1 = args[0];
            if (args.Length > 1)
                param2 = args[1];
            if (! Directory.Exists(param1))
            {
                param1 = ""; param2 = "";
            }
            if (v == 1)
                return param1;
            else if (v == 2)
                return param2;
            else
                return "";
        }
    }
}
