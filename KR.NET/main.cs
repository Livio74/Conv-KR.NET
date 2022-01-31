using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KRLib.NET;

namespace KR.NET
{
    static class main
    {

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            MOD_MAIN.G_strDirRoot = GetParam(1, args);
            MOD_MAIN.G_strChiave = GetParam(2, args);
            if ("".Equals(MOD_MAIN.G_strDirRoot))
            {
                MOD_MAIN.G_strFileLog = "klog.txt";
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new kr());
            } else
            {
                MOD_MAIN.G_strFileLog = MOD_MAIN.G_strDirRoot + "\\klog.txt";
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new minKR());
            }
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
