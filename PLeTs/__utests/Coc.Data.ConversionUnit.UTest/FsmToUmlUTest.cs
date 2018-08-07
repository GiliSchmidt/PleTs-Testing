using Coc.Data.ControlAndConversionStructures;
using Coc.Data.ControlStructure;
using Coc.Modeling.FiniteStateMachine;
using Coc.Modeling.Uml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UTestUtil;

namespace Coc.Data.ConversionUnit.UTest
{
    [TestClass()]
    public class FsmToUmlUTest
    {
        private FsmToUml converter;

        [TestInitialize()]
        public void Initializer()
        {
            converter = new FsmToUml();
        }

        [TestMethod()]
        public void ConverterTest()
        {
            List<GeneralUseStructure> listModel, methodReturn;
            StructureType type;

            listModel = new List<GeneralUseStructure>();
            listModel = StructureCollectionUtil.GetFSMUmlActOats();

            //this parameter is not used
            type = StructureType.OATS;

            methodReturn = converter.Converter(listModel, type);

            Assert.IsNotNull(methodReturn, "Method returned null");
            Assert.IsTrue(methodReturn.Count == 1, "Nothing was converted");
        }

        [TestMethod()]
        public void TransformToUmlTest()
        {
            FiniteStateMachine[] fsms;
            UmlModel methodReturn;

            fsms = StructureCollectionUtil.GetFSMUmlActOats().Cast<FiniteStateMachine>().ToArray();

            methodReturn = converter.TransformToUml(fsms);

            Assert.IsNotNull(methodReturn, "Method returned null");
            Assert.IsTrue(methodReturn.Diagrams.Count > 1, "Nothing was converted");
        }
    }
}