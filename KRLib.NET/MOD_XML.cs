using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRLib.NET
{
    public class MOD_XML
    {
        public static string ConvToXML(string strS , int intForce = 0)
        {
            string strS2 = ""; int i; char Chr;
            bool bBlank = true;
            for (i = 0; i < strS.Length; i++)
            {
                Chr = strS[i];
                int intChr = (int)Chr;
                if (intChr <= 31)
                {
                    strS2 += "&#" + intChr.ToString() + ";";
                    if (intChr != 9 && intChr != 13 && intChr != 10) bBlank = false;
                } else if (intChr >= 128)
                {
                    strS2 += "&#" + intChr.ToString() + ";";
                } else
                {
                    switch (intChr)
                    {
                        case 32: 
                            strS2 += "&#32;";
                            break;
                        case 38:
                            strS2 += "&amp;";
                            bBlank = false;
                            break;
                        case 39:
                            strS2 += "'";
                            bBlank = false;
                            break;
                        case 60:
                            strS2 += "&lt;";
                            bBlank = false;
                            break;
                        case 62:
                            strS2 += "&gt;";
                            bBlank = false;
                            break;
                        default:
                            strS2 += Chr;
                            bBlank = false;
                            break;
                    }
                }
            }
            if (bBlank && intForce == 0) strS2 = strS;
            return strS2;
        }
    }
}
