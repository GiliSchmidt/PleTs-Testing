using System;
using System.Collections.Generic;
using Lesse.Core.ControlAndConversionStructures;

namespace Lesse.Core.Interfaces
{
    public class Validator
    {
        public virtual List<KeyValuePair<String, Int32>> Validate(List<GeneralUseStructure> ListModel, String fileName)
        {
            return new List<KeyValuePair<String, Int32>>();
        }
    }
}
