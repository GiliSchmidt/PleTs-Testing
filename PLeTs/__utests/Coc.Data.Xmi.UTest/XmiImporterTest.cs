using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lesse.Core.ControlAndConversionStructures;
using UTestUtil;

namespace Coc.Data.Xmi.UTest
{
    [TestClass()]
    public class XmiImporterTest
    {
        private XmiImporter importer;

        [TestInitialize()]
        public void Initializer()
        {
            importer = new XmiImporter();
        }


        [TestMethod()]
        public void ParserMethodTest()
        {
            String path, name;
            StructureCollection returnModel;

            path = "..\\..\\..\\TestFiles\\ogmaj - oats - astah.xml";
            name = "test";

            returnModel = importer.ParserMethod(path, ref name, null);

            Assert.IsNotNull(returnModel, "Method returned null");
            Assert.IsTrue(returnModel.listGeneralStructure.Count > 0, "Nothing was parsed");
        }
    }
}
