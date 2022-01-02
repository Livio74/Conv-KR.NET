using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR.NET
{
    class MOD_FILE_LIST
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
            throw new NotImplementedException();
        }

        public static long GetSize()
        {
            return m_cnt;
        }

        public static string GetFile(long lngInd) 
        {
            throw new NotImplementedException();
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
