using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRLib.NET;

namespace KR.NET
{
    public class MOD_INVKEY
    {
        public static string reverseKey(string strFIleLog, bool bAdd64)
        {
            string strK; DateTime dateX; string strCriptKey;
            dateX = File.GetLastWriteTime(strFIleLog);
            int first = dateX.Year;
            int second = dateX.Day * dateX.Month;
            strK = dateX.ToString("yyyyMMdd") + first.ToString("X") + dateX.ToString("ddyyyyMM") + second.ToString("X");
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            StreamReader streamFileLog = new StreamReader(strFIleLog, iso88591, false);
            strCriptKey = streamFileLog.ReadLine();
            streamFileLog.Close();
            strCriptKey = STATICUTILS.EventuallyRemoveDoubleQuotes(strCriptKey);
            strK = InvKript(strCriptKey, strK, bAdd64);
            return strK;
        }

        //// public static string InvKript_OLD(string strS1 , string strS2) NON USATA

        public static string InvKript(string strS1, string strS2, bool bAdd64)
        {
            byte[] Chiave = new byte[256]; string strOut;
            char Ch; int i = 0;
            byte V, V2, intLngChiave = 0;
            string Chars; int intChar;
            string strChiave = strS2; int X = 1;
            Chiave = Encoding.ASCII.GetBytes(strChiave);
            intLngChiave = (byte)strChiave.Length;
            strOut = "";
            Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            for (i = 0; i < strChiave.Length; i++)
            {
                intChar = Chars.IndexOf(strChiave[i]);
                if (intChar >= 0)
                    Chiave[i] = (byte)intChar;
            }
            for (i = 0; i < strS1.Length; i++)
            {
                Ch = strS1[i];
                intChar = Chars.IndexOf(Ch);
                if (intChar >= 0)
                {
                    V = MOD_PRG_UTILS.Krpt((byte)intChar, Chiave, intLngChiave, i, 63);
                    if (bAdd64) V2 = (byte) (V + 64); else V2 = V;
                    char VChar = (Char) (int) V2;
                    if (('a' <= VChar) && (VChar <= 'z'))
                       strOut +=  VChar;
                    else if (('A' <= VChar) && (VChar <= 'Z'))
                        strOut += VChar;
                    else if (('0' <= VChar) && (VChar <= '9'))
                        strOut += VChar;
                    else
                        strOut += "[" + V2.ToString() + "]";
                }
                else
                {
                    strOut += Ch;
                }
            }
            return strOut;
        }
    }
}
