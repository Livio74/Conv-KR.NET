using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR.NET
{
    class MOD_KLOG
    {
        private static string[] strListaDir = new string[30000];
        private static string[] strListaDirOld = new string[30000];
        private static int intNumDir = 0;
        private static Boolean bolEsisteLog = false;
        private static Boolean bolErrLog = true;

        public static string CaricaLogFile(string strKey , string strFileLog)
        {
            long i; long j; long i1; string strGet;
            string strD; string strCriptKey = "";
            intNumDir = 0;
            try
            {
                strGet = MOD_PRG_UTILS.getKey(strFileLog, strKey);
                Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
                StreamReader streamFileLog = new StreamReader(strFileLog, iso88591, false);
                strCriptKey = streamFileLog.ReadLine();
                if ("".Equals(strCriptKey)) {
                    streamFileLog.Close();
                    return "";
                }
                else {
                    if (strGet.Equals(strCriptKey))
                    {
                        streamFileLog.Close();
                        return "";
                    }
                }
                while (streamFileLog.Peek() >= 0)
                {
                    strListaDir[intNumDir] = streamFileLog.ReadLine();
                    intNumDir++;
                }
                streamFileLog.Close();
                bolEsisteLog = true;
                for (i = 0; i < intNumDir; i++)
                {
                    i1 = strListaDir[intNumDir].IndexOf(':');
                    if (i1 < 0)
                    {
                        bolErrLog = true; return "";
                    }
                    //questa parte non me la ricordo , forse è per disabilitare le sottodirectory
                    if (strListaDir[i][strListaDir[i].Length - 1] == 'F')
                    {
                        strD = strListaDir[i].Substring(0, (int) i1);
                        for (j = 0; j < intNumDir; i++)
                        {
                            if (strD.Equals(strListaDir[j].Substring(0, (int)i1)))
                            {
                                strListaDir[j] = strListaDir[j].Substring(0, strListaDir[j].Length - 1) + 'D';
                            }
                        }
                    }
                }
                return strKey;
            } catch
            {
                bolEsisteLog = false;
                return "";
            }
        }

        public static void SalvaLogFile(string strChiave , string strFileLog)
        {
            string strK;
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            MOD_PRG_UTILS.setKey(out strK, strChiave);
            StreamWriter streamFileLog = new StreamWriter(strFileLog, false, iso88591);
            streamFileLog.WriteLine(strK);
            for (long i = 0; i < intNumDir; i++)
            {
                streamFileLog.WriteLine(strListaDir[i]);
            }
            streamFileLog.Close();
        }

        public static string IsStato(string strDir)
        {
            string strTemp = ""; int i = 0;
            if (bolEsisteLog)
            {
                if (strDir[strDir.Length - 1] == '\\') strDir = strDir.Substring(0, strDir.Length - 1);
                for (i = 0; i < intNumDir; i++)
                {
                    if (strListaDir[i].Substring(0 , strDir.Length - 4).ToUpper().Equals(strDir.ToUpper()) )
                    {
                        strTemp = strListaDir[i].Substring(strListaDir[i].Length - 1);
                    }
                }
            } else
            {
                strTemp = "E"; ;
            }
            return strTemp;
        }

        public static void SetStato(string strDir)
        {
            throw new NotImplementedException();
        }

        public static void RigeneraLog(string strChiave , string strDirRadice , string strLogFile)
        {
            throw new NotImplementedException();
        }

        public static void LoadIntoList(Object lst , string StatoE, string StatoK)
        {
            throw new NotImplementedException();
        }

        public static void CambiaStato(string strStatoE , string strStatoK , string strStatoNuovo, string strSubDir = "")
        {
            throw new NotImplementedException();
        }
        public static void SetNewStato(string strDir , string strStatoK, string strStatoE)
        {
            throw new NotImplementedException();
        }
    }
}
