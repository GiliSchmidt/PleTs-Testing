﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coc.Modeling.TestSuitStructure
{
    public class SaveParameter
    {
        #region Constructor
        public SaveParameter(string name)
        {
            this.name = name;
            Rules = new List<Rule>();
        }

        public SaveParameter()
        {
            Rules = new List<Rule>();
        }
        #endregion

        public List<Rule> Rules;

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string leftBoundary;

        public string LeftBoundary
        {
            get { return leftBoundary; }
            set { leftBoundary = value; }
        }

        private string rightBoundary;

        public string RightBoundary
        {
            get { return rightBoundary; }
            set { rightBoundary = value; }
        }

        private string prefix;

        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }

        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
    }
}
