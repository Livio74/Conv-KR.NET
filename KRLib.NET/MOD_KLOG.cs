using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRLib.NET
{
    public class MOD_KLOG
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
            strGet = MOD_PRG_UTILS.getKey(strFileLog, strKey);
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            StreamReader streamFileLog = new StreamReader(strFileLog, iso88591, false);
            strCriptKey = streamFileLog.ReadLine();
            strCriptKey = STATICUTILS.EventuallyRemoveDoubleQuotes(strCriptKey);
            if ("".Equals(strCriptKey)) {
                streamFileLog.Close();
                return "";
            }
            else {
                if (!strGet.Equals(strCriptKey))
                {
                    streamFileLog.Close();
                    return "";
                }
            }
            while (streamFileLog.Peek() >= 0)
            {
                strListaDir[intNumDir] = streamFileLog.ReadLine();
                strListaDir[intNumDir] = STATICUTILS.EventuallyRemoveDoubleQuotes(strListaDir[intNumDir]);
                intNumDir++;
            }
            streamFileLog.Close();
            bolEsisteLog = true;
            for (i = 0; i < intNumDir; i++)
            {
                i1 = strListaDir[i].IndexOf(':');
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
            return strGet;
        }

        public static void SalvaLogFile(string strChiave , string strFileLog)
        {
            string strK;
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            MOD_PRG_UTILS.setKey(out strK, strChiave);
            StreamWriter streamFileLog = new StreamWriter(strFileLog, false, iso88591);
            streamFileLog.WriteLine("\"" + strK + "\"");
            for (long i = 0; i < intNumDir; i++)
            {
                streamFileLog.WriteLine("\"" + strListaDir[i] + "\"");
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
                    if (strListaDir[i].Substring(0 , strListaDir[i].Length - 3).ToUpper().Equals(strDir.ToUpper()) )
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
            if (bolEsisteLog)
            {
                if (strDir[strDir.Length - 1] == '\\') strDir = strDir.Substring(0, strDir.Length - 1);
                for (int i = 0; i < intNumDir; i++)
                {
                    if (strDir.Length <= strListaDir[i].Length - 3)
                    {
                        if (strListaDir[i].Substring(0, strDir.Length).Equals(strDir))
                        {
                            if (strListaDir[i][strListaDir[i].Length - 1] == 'E')
                            {
                                if (strListaDir[i][strListaDir[i].Length - 2] != 'K')
                                {
                                    strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 3) + ":KE";
                                }
                                else
                                {
                                    strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 3) + ":_E";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                strListaDir[intNumDir] = strDir + ":KE";
                intNumDir++;
            }
        }

        public static void RigeneraLog(string strChiave , string strDirRadice , string strLogFile, string Kr_dirRadice)
        {
            string[] strListLV1 = new string[5000]; int intNumLV1 = 0; //Lista directory livello i-1
            string[] strListLV2 = new string[5000]; int intNumLV2 = 0; //Lista directory livello i
            string[] strListFD = new string[5000]; int intNums = 0; //Lista dei files e delle dirs per una dir
            int intTipo = 0; //Tipo(File, Directory o altro) per strListFD(i)
            int i = 0, j = 0; string strDir = ""; string strS = "";
            int intNum1 = 0;
            //1. Salva Vecchio caricato
            if (strDirRadice[strDirRadice.Length - 1] == '\\') strDirRadice = strDirRadice.Substring(0, strDirRadice.Length - 1);
            for (i = 0; i < intNumDir; i++)
            {
                strListaDirOld[i] = strListaDir[i];
            }
            intNum1 = intNumDir;
            intNumDir = 0;
            //2. Rigenera Log e controlla nei vecchi
            if (strListaDirOld[0] == null)
            {
                strDir = "";
            } else if ("".Equals(strListaDirOld[0])) {
                strDir = "";
            } else if (Kr_dirRadice.Equals(strListaDirOld[0].Substring(0 , strListaDirOld[0].Length - 3)))
            {
                strDir = strDirRadice;
            } else
            {
                strDir = "";
            }
            strListLV1[0] = strDirRadice; intNumLV1 = 1;
            // 2.1 Per ogni directory di un certo livello vengono generate le sottodirectory
            // -.- e inserite nella lista di uscita lstListaDir ->lstFile
            // -.- poi viene riportata in lstListaDir per il loop successivo

            while (intNumLV1 > 0)
            {
                //2.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
                intNumLV2 = 0;
                for (i = 0; i < intNumLV1; i++)
                {
                    intNums = 0;
                    strListaDir[intNumDir] = strListLV1[i] + ":_E";
                    if (!"".Equals(strDir))
                    {
                        strS = strListaDirOld[0].Substring(0, strListaDirOld[0].Length - 3);
                        strS += strListLV1[i].Substring(strDir.Length);
                    }
                    for (j = 0; j < intNum1; j++)
                    {
                        if (strListLV1[i].Equals(strListaDirOld[j].Substring(0, strListaDirOld[j].Length - 3)))
                        {
                            strListaDir[intNumDir] = strListaDirOld[j];
                        }
                        else if (strS.Equals(strListaDirOld[j].Substring(0, strListaDirOld[j].Length - 3)))
                        {
                            strListaDir[intNumDir] = strListLV1[i] + strListaDirOld[j].Substring(strListaDirOld[j].Length - 2);
                        }
                    }
                    intNumDir++;
                    MOD_UTILS_SO.ListaFileEDirs(strListLV1[i], strListFD, out intNums);
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
                        }
                    }
                }
                //2.2 SUCC (Copia della lista di output come lista di input per il loop successivo
                for (i = 0; i < intNumLV2; i++)
                {
                    strListLV1[i] = strListLV2[i];
                }
                intNumLV1 = intNumLV2;
            }
            SalvaLogFile(strChiave, strLogFile);
            CaricaLogFile(strChiave, strLogFile);
        }

        public static void LoadIntoList(ListBox lst , string StatoE, string StatoK)
        {
            lst.Items.Clear();
            for (int i = 0; i < intNumDir; i++)
            {
                if ("".Equals(StatoK) || StatoK.Equals(strListaDir[i].Substring(strListaDir[i].Length - 2, 1)))
                {
                    if ("".Equals(StatoE) || StatoE.Equals(strListaDir[i].Substring(strListaDir[i].Length - 1, 1)))
                    {
                        lst.Items.Add(strListaDir[i]);
                    }
                }
            }
        }

        public static void CambiaStato(string strStatoE , string strStatoK , string strStatoNuovo, string strSubDir = "")
        {
            string strStato1=""; int i = 0;
            if ("".Equals(strSubDir))
            {
                for (i = 0; i < intNumDir; i ++)
                {
                    if ("".Equals(strStatoK) || strStatoK.Equals(strListaDir[i].Substring(strListaDir[i].Length - 2 , 1)))
                    {
                        if ("".Equals(strStatoE) || strStatoE.Equals(strListaDir[i].Substring(strListaDir[i].Length - 1 , 1)))
                        {
                            if ("".Equals(strStatoNuovo))
                            {
                                strStato1 = strListaDir[i].Substring(strListaDir[i].Length - 1, 1);
                                if ("D".Equals(strStato1)) strStato1 = "E"; else strStato1 = "D";
                            }
                            else
                            {
                                strStato1 = strStatoNuovo;
                            }
                            strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 1) + strStato1;
                        }
                    }
                }
            } else
            {
                for (i = 0; i < intNumDir; i++)
                {
                    if (strListaDir[i].IndexOf(strSubDir) >= 0)
                    {
                        if ("".Equals(strStatoNuovo))
                        {
                            strStato1 = strListaDir[i].Substring(strListaDir[i].Length - 1, 1);
                            if ("D".Equals(strStato1)) strStato1 = "E"; else strStato1 = "D";
                        }
                        else
                        {
                            strStato1 = strStatoNuovo;
                        }
                        strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 1) + strStato1;
                    }
                }
            }
        }
        public static void SetNewStato(string strDir , string strStatoK, string strStatoE)
        {
            for (int i=0; i < intNumDir; i++)
            {
                if (strDir.Length <= strListaDir[i].Length - 3)
                {
                    if (strListaDir[i].Substring(0, strListaDir[i].Length - 3).Equals(strDir))
                    {
                        if (!"".Equals(strStatoK))
                            strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 2) + strStatoK + strListaDir[i].Substring(strListaDir[i].Length - 1);
                        if (!"".Equals(strStatoE))
                            strListaDir[i] = strListaDir[i].Substring(0, strListaDir[i].Length - 1) + strStatoE;
                    }
                }
            }
        }
    }
}
