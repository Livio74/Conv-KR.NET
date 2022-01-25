using KR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KRTest
{
    [TestClass]
    public class UnitTestModXml
    {
        [TestMethod]
        public void TestMethodConvXML()
        {
            string input = "<prova>";
            string outConvXML = MOD_XML.ConvToXML(input);
            string outConvXmlExpected = "&lt;prova&gt;";
            Assert.AreEqual(outConvXmlExpected, outConvXML);

            input = "'provag'";
            outConvXML = MOD_XML.ConvToXML(input);
            outConvXmlExpected = input;
            Assert.AreEqual(outConvXmlExpected, outConvXML);

            input = "prova prova&prova";
            outConvXML = MOD_XML.ConvToXML(input);
            outConvXmlExpected = input.Replace("&", "&amp;").Replace(" ", "&#32;");
            Assert.AreEqual(outConvXmlExpected, outConvXML);

            input = "prova\r\n";
            outConvXML = MOD_XML.ConvToXML(input);
            outConvXmlExpected = input.Replace("\r", "&#13;").Replace("\n", "&#10;");
            Assert.AreEqual(outConvXmlExpected, outConvXML);
        }
    }
}
