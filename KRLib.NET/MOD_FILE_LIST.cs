using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRLib.NET
{
    public class MOD_FILE_LIST
    {
        const int MAX_PATH1 = 5000;
        const long MAX_PATH2 = 32267;

        //INIT_FL
        public static long m_ind = 0;
        public static long m_cnt = 0;

        public static int m_Type = 0;
        public static string m_Resource = "";
        public static string[] m_List = new string[MAX_PATH2];

        public static string Genera(int intType , string strResource , string strParam1)
        {
            string strOut = "";
            m_ind = 0; m_cnt = 0;
            if (intType == 1)
            {
                m_Type = 1; m_Resource = strResource;
                strOut = GeneraDaKLog(strResource, strParam1);
            }
            return strOut;
        }

        public static string GeneraDaKLog(string strDirRadice, string strFileLog)
        {
            string[] strListLV1 = new string[MAX_PATH1]; int intNumLV1 = 0; //Lista directory livello i-1
            string[] strListLV2 = new string[MAX_PATH1]; int intNumLV2 = 0; //Lista directory livello i
            string[] strListFD = new string[MAX_PATH1]; int intNums = 0; //Lista dei files e delle dirs per una dir
            string strErr = ""; string strS = ""; int i = 0; int j = 0;
            if (strDirRadice[strDirRadice.Length - 1] == '\\') strDirRadice = strDirRadice.Substring(0, strDirRadice.Length - 1);
            string strFileLogName = strFileLog.Substring(strDirRadice.Length + 1);
            strListLV1[0] = strDirRadice; intNumLV1 = 1;
            // 1 Per ogni directory di un certo livello vengono generate le sottodirectory
            // -.- e inserite nella lista di uscita lstListaDir ->lstFile
            // -.- poi viene riportata in lstListaDir per il loop successivo
            while (intNumLV1 > 0)
            {
                //1.1 Genera file e dirs per tutte le dir di un livello i (0,1,2,..)
                intNumLV2 = 0;
                for (i = 0; i < intNumLV1; i++)
                {
                    intNums = 0;
                    MOD_UTILS_SO.ListaFileEDirs(strListLV1[i], strListFD, out intNums);
                    if (! "A".Equals(MOD_KLOG.IsStato(strListLV1[i])))
                    {
                        MOD_KLOG.SetStato(strListLV1[i]);
                        for (j = 0; j < intNums; j++)
                        {
                            FileAttributes attr = File.GetAttributes(strListLV1[i] + "\\" + strListFD[j]);
                            if (attr.HasFlag(FileAttributes.Directory))
                            {
                                if (strListFD[j].IndexOf(".") != 0)
                                {
                                    strListLV2[intNumLV2] = strListLV1[i] + "\\" + strListFD[j];
                                    intNumLV2++;
                                }
                            } else
                            {
                                if ("E".Equals(MOD_KLOG.IsStato(strListLV1[i]))) {
                                    if (! strFileLogName.Equals(strListFD[j]))
                                    {
                                        if (m_cnt < MAX_PATH2) //Corretto BUG : era (m_ind < MAX_PATH2)
                                        {
                                            strS = strListLV1[i] + "\\" + strListFD[j];
                                            m_List[m_cnt] = "." + strS.Substring(strDirRadice.Length);
                                            m_cnt++;
                                        } else
                                        {
                                            strErr = "OVERFLOW";
                                        }
                                    } else if (! strDirRadice.Equals(strListLV1[i])) 
                                    {
                                        m_cnt = m_cnt; // NOOPERATION
                                        ///MsgBox "ATTENZIONE FILE LOG PRESENTE IN " & strListLV1(i), vbExclamation
                                        ///P.S. non so perchè serve questo if
                                    }
                                }
                            }
                        }
                    }
                }
                //1.2 SUCC (Copia della lista di output come lista di input per il loop successivo
                for (i = 0; i < intNumLV2; i++)
                {
                    strListLV1[i] = strListLV2[i];
                }
                intNumLV1 = intNumLV2;
            }
            return strErr;
        }

        public static long GetSize()
        {
            return m_cnt;
        }

        public static string GetFile(long lngInd) 
        {
            string strOut = "";
            if (m_ind < m_cnt)
            {
                strOut = m_List[lngInd];
            }
            if (strOut[0] == '.')
            {
                strOut = m_Resource + m_List[lngInd].Substring(1);
            }
            return strOut;
        }

        private static string MoveFirst()
        {
            throw new NotImplementedException();
        }

        private static string MoveNext()
        {
            throw new NotImplementedException();
        }

        private static void DESTROY_FL()
        {

        }
    }
}
