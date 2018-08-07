using Coc.Data.Xmi.FuntionalValidator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Coc.Modeling.Uml;
using System.Collections.Generic;
using Coc.Data.Xmi.AbstractValidator;

namespace Coc.Data.Xmi.FuntionalValidator.UTest
{


    /// <summary>
    ///This is a test class for FuntionalValidatorTest and is intended
    ///to contain all FuntionalValidatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FuntionalValidatorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ValidateFunctional
        ///</summary>
        [TestMethod()]
        public void ValidateFunctionalTest()
        {
            UmlModel model = null; // TODO: Initialize to an appropriate value
            model = new UmlModel("Model X");
            UmlActivityDiagram diagram = new UmlActivityDiagram("");
            //UmlUseCaseDiagram case1 = new UmlUseCaseDiagram();
            UmlInitialState initial = new UmlInitialState();
            UmlFinalState final = new UmlFinalState();
            UmlActionState action = new UmlActionState();
            UmlActionState action1 = new UmlActionState();
            UmlTransition transition = new UmlTransition();
            UmlTransition transition1 = new UmlTransition();
            UmlTransition transition2 = new UmlTransition();
            initial.Name = "initial0";
            final.Name = "final0";
            diagram.UmlObjects.Add(initial);
            transition.Source = initial;
            transition.Target = action;
            action.SetTaggedValue("jude.hyperlink", "teste");
            transition1.Source = action;
            transition1.Target = action1;
            transition1.SetTaggedValue("FTaction", "");
            transition1.SetTaggedValue("FTexpectedResult", "Use parameters in the shared step below.");
            transition2.Source = action1;
            transition2.Target = final;
            diagram.UmlObjects.Add(action);
            diagram.UmlObjects.Add(action1);
            diagram.UmlObjects.Add(transition);
            diagram.UmlObjects.Add(transition1);
            diagram.UmlObjects.Add(transition2);
            diagram.UmlObjects.Add(transition2);




            List<KeyValuePair<string, int>> expected = new List<KeyValuePair<string, int>>(); // TODO: Initialize to an appropriate value
            List<KeyValuePair<string, int>> actual = ValidatorFactory.CreateValidator().Validate(model,"");


            Assert.AreEqual(expected.Count, actual.Count);

            IEnumerator<KeyValuePair<string, int>> e1 = expected.GetEnumerator();
            IEnumerator<KeyValuePair<string, int>> e2 = actual.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }
    }
}
