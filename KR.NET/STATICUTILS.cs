using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR.NET
{
    public class STATICUTILS
    {

        public static string EventuallyRemoveDoubleQuotes(string inString)
        {
            string outstring = inString;
            if (inString.Length > 1)
            {
                if (inString[0] == '\"')
                {
                    if (inString[inString.Length - 1] == '\"')
                    {
                        outstring = inString.Substring(1, inString.Length - 2);
                    }
                }
            }
            return outstring;
        }
    }
}
