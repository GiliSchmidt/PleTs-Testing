using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    public enum AtlasConnectionType
    {
        Mandatory = 0,
        Optional = 1, 
        Alternative = 2, 
        OrRelation = 3, 
        Excludes = 4, 
        Requires = 5, 
        Cardinality = 6, 
    }
}