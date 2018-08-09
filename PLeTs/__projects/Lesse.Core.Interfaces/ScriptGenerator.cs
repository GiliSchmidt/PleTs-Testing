using System;
using System.Collections.Generic;
using Lesse.Core.ControlAndConversionStructures;

namespace Lesse.Core.Interfaces
{
    public interface ScriptGenerator
    {
        void GenerateScript(List<GeneralUseStructure> listPlan, String path);//, List<object> arguments = null

       
    }
}
