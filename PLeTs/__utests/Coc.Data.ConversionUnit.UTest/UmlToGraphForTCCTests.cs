using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Coc.Data.ControlAndConversionStructures;
using UTestUtil;
using Coc.Data.ControlStructure;

namespace Coc.Data.ConversionUnit.UTest
{
    [TestClass()]
    public class UmlToGraphForTCCTests
    {
        private UmlToGraphForTCC converter;

        [TestInitialize()]
        public void Initializer()
        {
            converter = new UmlToGraphForTCC();
        }

        [TestMethod()]
        public void ConverterTest()
        {
            List<GeneralUseStructure> listModel, methodReturn;
            StructureType type;

            listModel = new List<GeneralUseStructure>();
            listModel = StructureCollectionUtil.GetStructureCollectionSeqJUnit();

            //this parameter is not used
            type = StructureType.OATS;

            methodReturn = converter.Converter(listModel, type);

            Assert.IsNotNull(methodReturn, "Method returned null");
            Assert.IsTrue(methodReturn.Count == 1, "Nothing was converted");
        }
    }
}