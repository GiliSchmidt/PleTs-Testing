using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UTestUtil;
using Coc.Data.ControlAndConversionStructures;
using Coc.Data.ControlStructure;

namespace Coc.Data.ConversionUnit.UTest
{
    [TestClass()]
    public class UmlToGraphTests
    {
        private UmlToGraph converter;

        [TestInitialize()]
        public void Initializer()
        {
            converter = new UmlToGraph();
        }

        [TestMethod()]
        public void ConverterTest()
        {
            List<GeneralUseStructure> listModel, methodReturn;
            StructureType type;

            listModel = new List<GeneralUseStructure>();
            listModel = StructureCollectionUtil.GetStructureCollectionActOats();

            //this parameter is not used
            type = StructureType.OATS;

            methodReturn = converter.Converter(listModel, type);

            Assert.IsNotNull(methodReturn, "Method returned null");
            Assert.IsTrue(methodReturn.Count == 1, "Nothing was converted");
        }
    }
}