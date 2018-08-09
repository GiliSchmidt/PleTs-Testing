using System;
using Lesse.Modeling.FiniteStateMachine;

namespace Coc.Data.Wp
{
    public class StatePair
    {
        #region Attributes
        public State Si { get; set; }
        public State Sj { get; set; }
        public String wi { get; set; }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Si.Name + " - " + wi + " - " + Sj.Name;
        }
        #endregion
    }
}
