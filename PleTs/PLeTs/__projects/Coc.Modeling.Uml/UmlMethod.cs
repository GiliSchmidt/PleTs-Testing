﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coc.Modeling.Uml
{
    public class UmlMethod : UmlBase
    {
        #region Constructor
        public UmlMethod()
        {
            Params = new List<UmlMethodParam>();
        }
        #endregion

        #region Attributes
        public String Return { get; set; }

        public List<UmlMethodParam> Params { get; set; }

        public Boolean Abstract { get; set; }

        public String Visibility { get; set; }
        #endregion

        public override string ToString()
        {
            return this.Name;
        }
    }
}
