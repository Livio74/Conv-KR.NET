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
            byte[] Chiave = null; string strOut;
            char Ch; int X = 0;
            byte V; byte intLngChiave;
            string Chars; byte byt;
            Chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890èà";
            Chiave = Encoding.ASCII.GetBytes("0" + strChiave);
            intLngChiave = (byte) strChiave.Length; intLngChiave++;
            Chiave[0] = 0;
            strOut = "";
            for (int i = 0; i < strNomeFile.Length; i++)
            {
                Ch = strNomeFile[i];
                if (Chars.Contains(Ch))
                {
                    byt = (byte) Chars.IndexOf(Ch);
                    V = Krpt(byt, Chiave, intLngChiave, i, 63);
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


            for (int i = 0; i < strNomeFile.Length; i++)
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
            int first = dateX.Year;
            int second = dateX.Day * dateX.Month;
            strK = dateX.ToString("yyyyMMdd") + first.ToString("X") + dateX.ToString("ddyyyyMM") + second.ToString("X");
            strK = KritpStr(strK, strChiave);
            return strK;
        }

        public static void setKey(out string strK, string strChiave)
        {
            DateTime dateNow = DateTime.Now;
            int first = dateNow.Year;
            int second = dateNow.Day * dateNow.Month;
            strK = dateNow.ToString("yyyyMMdd") + first.ToString("X") + dateNow.ToString("ddyyyyMM") + second.ToString("X");
            strK = KritpStr(strK, strChiave);
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
            FileStream outputFile = null;
            FileStream inOutFile = null;
            try
            {
                int X = strNomeFile.LastIndexOf('\\');
                byte byt; int intPos; DateTime dtDataMod;
                if (X < 0) X = 1; else X++;
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
                }
                else
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
                        GA_Buffer[i] = Krpt(GA_Buffer[i], Chiave, intLngChiave, i + 1);
                    }
                    if (outputFile == null)
                    {
                        inOutFile.Seek(-bytesRead, SeekOrigin.Current);
                        inOutStream.Write(GA_Buffer, 0, bytesRead);
                    }
                    else
                    {
                        outputFileStream.Write(GA_Buffer, 0, bytesRead);
                    }
                }
                inOutFile.Close();
                inOutFile = null;
                if (outputFile == null)
                {
                    File.Move(strNomeFile, strNomeFileK);
                    MOD_UTILS_SO.SetFileDateTime(strNomeFileK, dtDataMod.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                else
                {
                    outputFile.Close();
                    outputFile = null;
                }
                //TODO: Gestione errore con variabile globale stringa errore
            } catch (Exception e)
            {
                MOD_MAIN.G_strErr = "<EXCEPTION ID = \"0\" IDREF=\"" + e.GetHashCode() + "\" DESCRIPTION=\"" + MOD_XML.ConvToXML(e.Message) + "\" SOURCE=\"";
                MOD_MAIN.G_strErr += e.Source + "\" DATETIME=\"" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\">";
                MOD_MAIN.G_strErr += "<DETAILS><FILE>" + MOD_XML.ConvToXML(strNomeFile, 1) + "</FILE></DETAILS></EXCEPTION>\r\n";
            } finally
            {
                if (outputFile != null)
                    outputFile.Close();
                if (inOutFile != null)
                    inOutFile.Close();
            }
        }
    }
}
