using System;
using System.Linq;
using Lesse.Core.ControlAndConversionStructures;
using Lesse.Modeling.Uml;
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

        /*
         * Test if OgmaJ doesn't crash if the script contains "web.textBox(...).setText("");"
         */
        [TestMethod]
        public void ParserMethodTextBoxSetTextEmptyTest()
        {
            String xmiPath, name;
            StructureCollection returnModel;

            xmiPath = "..\\..\\..\\TestFiles\\OATS\\textbox set text empy.java";
            name = "test";

            returnModel = parser.ParserMethod(xmiPath, ref name, null);
        
            Assert.IsNotNull(returnModel, "Method returned null");

            Assert.IsNotNull(returnModel.listGeneralStructure.OfType<UmlFinalState>()
                .FirstOrDefault(), "There's no final state in diagram!");
        }
    }
}
