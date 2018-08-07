using Coc.Data.ControlAndConversionStructures;
using Coc.Data.ControlStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UTestUtil;

namespace Coc.Data.ConversionUnit.UTest
{
    [TestClass()]
    public class UmlToFsmTest
    {
        private UmlToFsm converter;

        [TestInitialize()]
        public void Initializer()
        {
            converter = new UmlToFsm();
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