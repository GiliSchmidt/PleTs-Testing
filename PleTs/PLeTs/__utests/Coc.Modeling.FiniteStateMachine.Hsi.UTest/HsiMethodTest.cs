using Coc.Modeling.FiniteStateMachine.Hsi;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Coc.Modeling.FiniteStateMachine;
using System.Collections.Generic;
using Coc.Data.HSI;

namespace Coc.Data.Modeling.FiniteStateMachine.Hsi.UTest
{
    
    
    /// <summary>
    ///This is a test class for HsiMethodTest and is intended
    ///to contain all HsiMethodTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HsiMethodTest
    {
        /// <summary>
        /// Generate a Finite State Machine for use on method validation
        /// </summary>
        public Coc.Modeling.FiniteStateMachine.FiniteStateMachine GenerateTestMachine1()
        {
            //Initialize Test FSM
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = new Coc.Modeling.FiniteStateMachine.FiniteStateMachine();

            State a = new State("A");
            State b = new State("B");
            State c = new State("C");
            State d = new State("D");
            State e = new State("E");
            State f = new State("F");

            fsm.AddState(a);
            fsm.AddState(b);
            fsm.AddState(c);
            fsm.AddState(d);
            fsm.AddState(e);
            fsm.AddState(f);

            fsm.AddTransition(new Transition(a, b, "a", "1"));
            fsm.AddTransition(new Transition(a, e, "x", "8"));
            fsm.AddTransition(new Transition(b, b, "y", "11"));
            fsm.AddTransition(new Transition(b, c, "b", "2"));
            fsm.AddTransition(new Transition(b, d, "c", "3"));
            fsm.AddTransition(new Transition(c, f, "s", "9"));
            fsm.AddTransition(new Transition(d, e, "z", "12"));
            fsm.AddTransition(new Transition(d, f, "d", "4"));
            fsm.AddTransition(new Transition(e, f, "f", "0"));

            fsm.InitialState = a;
            return fsm;
        }

        /// <summary>
        /// Generate a Finite State Machine for use on method validation
        /// </summary>
        public Coc.Modeling.FiniteStateMachine.FiniteStateMachine GenerateTestMachine2()
        {
            //Initialize Test FSM
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = new Coc.Modeling.FiniteStateMachine.FiniteStateMachine();

            State _1 = new State("1");
            State _2 = new State("2");
            State _3 = new State("3");
            State _4 = new State("4");

            fsm.AddState(_1);
            fsm.AddState(_2);
            fsm.AddState(_3);
            fsm.AddState(_4);

            fsm.AddTransition(new Transition(_1, _2, "a", "1"));
            fsm.AddTransition(new Transition(_1, _3, "b", "0"));
            fsm.AddTransition(new Transition(_2, _3, "a", "1"));
            
            fsm.AddTransition(new Transition(_2, _4, "b", "0"));
            fsm.AddTransition(new Transition(_3, _4, "a", "0"));
            fsm.AddTransition(new Transition(_4, _1, "b", "0"));

            fsm.InitialState = _1;
            return fsm;
        }


        /// <summary>
        ///A test for GetPreamble
        ///</summary>
        [TestMethod()]
        public void GetPreambleTest()
        {
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            HsiMethod target = new HsiMethod(fsm);

            string[] actual;
            actual = target.GetPreamble(fsm.GetStateByName("D"));
            
            Assert.IsTrue(actual != null, "O retorno do método não deve ser nulo, nunca.");
            Assert.IsTrue(actual.Length == 2, "O tamanho do preâmbulo do estado D é 2.");
            Assert.IsTrue(actual[0] == "a" && actual[1] == "c", "A menor sequencia de entradas para se alcançar o estado E é composta somente da entrada {x}");
        }

        /// <summary>
        ///A test for GetAllowedInputs
        ///</summary>
        [TestMethod()]
        public void GetAllowedInputsTest()
        {
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            HsiMethod target = new HsiMethod(fsm);

            string[] actual;
            actual = target.GetAllowedInputs(fsm.GetStateByName("D"));

            Assert.IsTrue(actual != null, "O retorno do método não deve ser nulo, nunca.");
            Assert.IsTrue(actual.Length == 2, "O número de entradas aceitas pelo estado D é 2.");
            Assert.IsTrue(actual[0] == "z" && actual[1] == "d", "As entradas aceitas pelo estado D são {z} e {d}.");
        }

        /// <summary>
        ///A test for GetTransitionCover
        ///</summary>
        [TestMethod()]
        public void GetTransitionCoverTest()
        {
            //Initialize Test FSM
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            
            string[][] actual;

            HsiMethod target = new HsiMethod(fsm);
            actual = target.GetTransitionCover(fsm.GetStateByName("D"));

            Assert.IsTrue(actual != null, "O retorno do método não deve ser nulo, nunca.");
            //Assert.IsTrue(actual.Length == 2, "O número de entradas aceitas pelo estado D é 2.");
            //Assert.IsTrue(actual[0] == "z" && actual[1] == "d", "As entradas aceitas pelo estado D são {z} e {d}.");
        }

        /// <summary>
        ///A test for GetStatePairGroup
        ///</summary>
        [TestMethod()]
        public void GetStatePairGroupTest()
        {
            //Initialize Test FSM
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();

            HsiMethod target = new HsiMethod(fsm); // TODO: Initialize to an appropriate value

            HsiMethod.StatePair[] actual;
            actual = target.GetStatePairGroup();
            
            //Assert.AreEqual(actual, );
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetFailnessTable
        ///</summary>
        [TestMethod()]
        public void GetFailnessTableTest()
        {
            //Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine2();

            HsiMethod target = new HsiMethod(fsm); 
            
            List<HsiMethod.FailnessRecord> actual = target.GetFailnessTable();
            //Assert.AreEqual(true, actual);
        }

        /// <summary>
        ///A test for GetHiSet
        ///</summary>
        [TestMethod()]
        public void GetHiSetTest()
        {
            //Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine2();

            HsiMethod target = new HsiMethod(fsm); 
            String[][] result = target.GetHiSet();

            Assert.AreEqual(true, true);
        }

        /// <summary>
        ///A test for GetHsiSet
        ///</summary>
        [TestMethod()]
        public void GetHsiSetTest()
        {
            //Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine2();

            HsiMethod target = new HsiMethod(fsm); // TODO: Initialize to an appropriate value

            string[][][] actual;
            actual = target.GetHsiSet();
        }

        /// <summary>
        ///A test for GetIdentifierState
        ///</summary>
        [TestMethod()]
        public void GetIdentifierStateTest()
        {
            //Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine2();

            HsiMethod target = new HsiMethod(fsm); // TODO: Initialize to an appropriate value
            string[] transitionCover = new String[] { "a", "a", "a" };
            
            State actual = target.GetIdentifierState(transitionCover);
            Assert.AreEqual(actual.Name, "4");
        }

        /// <summary>
        ///A test for GenerateTestCases
        ///</summary>
        [TestMethod()]
        public void GenerateTestCasesTest()
        {
            //Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine1();
            Coc.Modeling.FiniteStateMachine.FiniteStateMachine fsm = this.GenerateTestMachine2();

            HsiMethod target = new HsiMethod(fsm); // TODO: Initialize to an appropriate value
            target.GenerateTestCases();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
