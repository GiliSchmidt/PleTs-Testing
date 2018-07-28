using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    public class AtlasConstraintOperator : IConstraintMember
    {
        private AtlasLogicalOperator type;

        public AtlasLogicalOperator Type
        {
            get { return type; }
            set { type = value; }
        }

        public AtlasConstraintOperator(AtlasLogicalOperator type)
        {
            this.type = type;
        }

        public string GetMemberName() {

            switch (this.type)   {
                case AtlasLogicalOperator.And:
                    return "∧";
                case AtlasLogicalOperator.Or:
                    return "∨";
                case AtlasLogicalOperator.Implies:
                    return "⇒";
                case AtlasLogicalOperator.Iff:
                    return "Iff";
                case AtlasLogicalOperator.LeftBracket:
                    return "(";
                case AtlasLogicalOperator.RightBracket:
                    return ")";
                case AtlasLogicalOperator.Not:
                    return "¬";
                default:
                    return "?";
            }

        }
    }
}