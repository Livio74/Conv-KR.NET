using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR.NET
{
    public class MOD_PRG_UTILS
    {
        public static string KritpStr(string strNomeFile , string strChiave)
        {
            byte[] Chiave = new byte[256]; string strOut;
            char Ch; int X = 0;
            byte V; byte intLngChiave;
            string Chars; byte byt;
            Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            Chiave = Encoding.ASCII.GetBytes(strChiave);
            intLngChiave = (byte) strChiave.Length; intLngChiave++;
            strOut = "";
            for (int i=0; i < strChiave.Length; i++)
            {
                Ch = strNomeFile[i];
                if (Chars.Contains(Ch))
                {
                    byt = (byte) Chars.IndexOf(Ch);
                    V = Krpt(byt, Chiave, intLngChiave, i - 1, 63);
                    strOut += Chars[V];
                } else
                {
                    strOut += Ch;
                }
            }
            return strOut;
        }

        public static string KritpStr2(string strNomeFile, string strChiave)
        {
            byte[] Chiave = new byte[256]; string strOut;
            char Ch;
            byte V; byte intLngChiave = 0;
            string Chars; byte byt;
            Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            Chiave = Encoding.ASCII.GetBytes(strChiave);
            intLngChiave = (byte)strChiave.Length;
            strOut = "";
            for (int i = 0; i < strChiave.Length; i++)
            {
                Ch = strNomeFile[i];
                if (Chars.Contains(Ch))
                {
                    byt = (byte)Chars.IndexOf(Ch);
                    V = Krpt(byt, Chiave, intLngChiave, i, 63);
                    strOut += Chars[V];
                }
                else
                {
                    strOut += Ch;
                }
            }
            return strOut;
        }

        public static string InvKript(string strS1, string strS2)
        {
            byte[] Chiave = new byte[256]; string strOut;
            char Ch;
            byte V; byte intLngChiave = 0;
            string Chars; byte byt; 
            string strChiave = strS2; int X = 1;
            Chiave = Encoding.ASCII.GetBytes(strChiave);
            intLngChiave = (byte) strChiave.Length;
            strOut = "";
            Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            for (int i = 0; i < strChiave.Length; i++)
            {
                Ch = strS1[i];
                if (Chars.Contains(Ch))
                {
                    byt = (byte) Chars.IndexOf(Ch);
                    V = Krpt(byt, Chiave, intLngChiave, i - 1, 63);
                    strOut += Chars[V];
                }
                else
                {
                    strOut += Ch;
                }
            }
            return strOut;
        }

        public static byte Krpt(byte bytV, byte[] Chiave , byte byLngChiave , long lngPos, byte just = 255)
        {
            int intOut = bytV ^ ((Chiave[lngPos % byLngChiave] & just));
            byte bytOut = (byte)intOut;
            return bytOut;
        }

        public static string getKey(string strFIleLog , string strChiave)
        {
            string strK; DateTime dateX;
            dateX = File.GetLastWriteTime(strFIleLog);
            int first = dateX.Year + Convert.ToInt32(dateX.ToString("ddyyyymm"));
            int second = dateX.Day * dateX.Month;
            strK = dateX.ToString("yyyymmdd") + first.ToString("X") + second.ToString("X");
            strK = KritpStr(strK, strChiave);
            return strK;
        }

        public static void Kriptp(string strNomeFile , string strChiave , string strOut = "")
        {
            byte[] Chiave = new byte[256];
            string strNomeFileK = "";
            byte intLngChiave = 0;
            char Ch;
            long j; byte V; long lngLngFile;
            byte[] GA_Buffer = new byte[3000];
            string Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            int X = strNomeFile.LastIndexOf('\\');
            byte byt; int intPos; DateTime dtDataMod;
            FileStream inOutFile = null;
            FileStream outputFile = null;
            if (X < 0) X = 0;
            // 1. Conversione TXT->BIN e calcolo della lunghezza
            Chiave = Encoding.ASCII.GetBytes(strChiave);
            intLngChiave = (byte)strChiave.Length;
            // 2. Kritp nome file
            strNomeFileK = strNomeFile.Substring(0, X);
            for (int i = X; i < strNomeFile.Length; i++)
            {
                Ch = strNomeFile[i];
                if (Chars.Contains(Ch))
                {
                    byt = (byte)Chars.IndexOf(Ch);
                    V = Krpt(byt, Chiave, intLngChiave, i - X, 63);
                    strNomeFileK += Chars[V];
                }
                else
                {
                    strNomeFileK += Ch;
                }
            }
            intPos = 2;
            dtDataMod = File.GetLastWriteTime(strNomeFile);
            Stream outputFileStream = null;
            if (strOut.Length == 0)
            {
                inOutFile = File.Open(strNomeFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            } else
            {
                inOutFile = File.Open(strNomeFile, FileMode.Open, FileAccess.Read);
                outputFile = File.Open(strOut, FileMode.Create, FileAccess.Write);
                outputFileStream = outputFile;
            }
            Stream inOutStream = (Stream)inOutFile;
            int bytesRead = 0;
            while ((bytesRead = inOutStream.Read(GA_Buffer, 0, GA_Buffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    GA_Buffer[i] = Krpt(GA_Buffer[i] , Chiave , intLngChiave , i);
                }
                if (outputFile == null)
                {
                    inOutStream.Write(GA_Buffer, 0, bytesRead);
                } else
                {
                    outputFileStream.Write(GA_Buffer, 0, bytesRead);
                }
            }
            inOutFile.Close();
            if (outputFile == null)
            {
                File.Move(strNomeFile, strNomeFileK);
                MOD_UTILS_SO.SetFileDateTime(strNomeFileK, dtDataMod.ToString("dd/MM/yyyy HH:mm:ss"));
            } else
            {
                outputFile.Close();
            }
            //TODO: Gestione errore con variabile globale stringa errore
        }
    }
}
