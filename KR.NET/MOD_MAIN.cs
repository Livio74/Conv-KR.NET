using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KR.NET
{
    static class MOD_MAIN
    {

        public static string G_strErr;
        public static bool G_bolEsisteLog;
        public static bool G_bolErrLog;
        public static long G_lng_NumFiles;
        public static string G_strFileLog;
        public static string G_strDirRoot;
        public static string G_strFileList;
        public static string G_strChiave;

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            G_strDirRoot = GetParam(1, args);
            G_strChiave = GetParam(2, args);
            if ("".Equals(G_strDirRoot))
            {
                G_strFileLog = "klog.txt";
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new kr());
            } else
            {
                G_strFileLog = G_strDirRoot + "\\klog.txt";
                //caricare il secondo elemento dopo
                string message = "Ecco i parametri passati : cartella " + G_strDirRoot;
                if (!("".Equals(G_strChiave)))
                    message += " , chiave : " + G_strChiave;
                MessageBox.Show(message , "Parametri");
            }
        }

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
