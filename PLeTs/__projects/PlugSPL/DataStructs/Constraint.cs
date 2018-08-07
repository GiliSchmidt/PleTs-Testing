using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.DataStructs
{
    public struct Constraint
    {
        private List<ConstraintMember> constraints;

        public List<ConstraintMember> Constraints
        {
            get { return constraints; }
            set { constraints = value; }
        }

        /// <summary>
        /// If false, next item must be operator. If true, next item must be feature.
        /// </summary>
        private bool operatorOrFeature;

        public bool OperatorOrFeature
        {
            get { return operatorOrFeature; }
            set { operatorOrFeature = value; }
        }

    }
}