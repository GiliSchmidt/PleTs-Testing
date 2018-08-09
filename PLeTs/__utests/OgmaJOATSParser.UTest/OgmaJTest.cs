using System;
using Lesse.Core.ControlAndConversionStructures;
using Lesse.OATS.OgmaJParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTestUtil;

namespace OgmaJOATSParser.UTest
{

    [TestClass]
    public class OgmaJTest
    {
        private OgmaJ parser;

        [TestInitialize()]
        public void Initializer()
        {
            parser = new OgmaJ();
        }


        [TestMethod]
        public void ParserMethodTest()
        {
            String xmiPath, name;
            StructureCollection returnModel;

            xmiPath = "..\\..\\..\\TestFiles\\java oats script.java";
            name = "test";

            returnModel = parser.ParserMethod(xmiPath, ref name, null);

            Assert.IsNotNull(returnModel, "Method returned null");
        }
    }
}
