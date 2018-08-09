using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Collections.Generic;
using Lesse.Core.ControlAndConversionStructures;
using UTestUtil;

namespace Coc.Data.Xmi.UTest
{
    [TestClass()]
    public class XmiExporterTest
    {
        private XmiExporter exporter;
        StructureCollectionUtil structureCollectionUtil;

        [TestInitialize()]
        public void Initializer()
        {
            exporter = new XmiExporter();
            structureCollectionUtil = new StructureCollectionUtil();
        }


        [TestMethod()]
        public void ToXmiTest()
        {
            List<GeneralUseStructure> generalUseStructure;
            XmlDocument resultDocument;

            generalUseStructure = StructureCollectionUtil.GetStructureCollectionActOats();

            resultDocument = exporter.ToXmi(generalUseStructure);

            Assert.IsNotNull(resultDocument, "Method returned null");

        }
    }
}
