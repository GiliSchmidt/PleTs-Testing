using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    public enum AtlasLogicalOperator
    {
        And = 0,
        Or = 1,
        Implies = 2,
        Iff = 3,
        LeftBracket = 4,
        RightBracket = 5,
        Not = 6,
    }
}