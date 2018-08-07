using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.DataStructs.ProductConfigurator
{
    public class DanuConstraint
    {
        private DanuComponent point1;
        public DanuComponent Point1
        {
            get { return point1; }
            set { point1 = value; }
        }

        private DanuComponent point2;
        public DanuComponent Point2
        {
            get { return point2; }
            set { point2 = value; }
        }

        private DanuConstraintTypes constraintType;
        public DanuConstraintTypes ConstraintType
        {
            get { return constraintType; }
            set { constraintType = value; }
        }

        public DanuConstraint(DanuComponent point1, DanuComponent point2, DanuConstraintTypes constraintType)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.constraintType = constraintType;
        }

        private DanuConstraint() { }
    }
}