using System;
using Lesse.Core.ControlAndConversionStructures;

namespace Lesse.Core.Interfaces
{
    public abstract class Parser
    {
        public abstract StructureCollection ParserMethod(String path, ref String name, Tuple<String, object>[] args);

        //ParserMethod(path, name, Tuple<object, object>[] args = new Tuple<String, object>[]() { (id1, data1), (id2, data2) } );
    }
}
