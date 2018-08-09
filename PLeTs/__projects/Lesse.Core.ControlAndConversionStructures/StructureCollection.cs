using System.Collections.Generic;
using Lesse.Core.ControlStructure;

namespace Lesse.Core.ControlAndConversionStructures
{
    public class StructureCollection
    {
        public StructureType type { get; set; }
        public List<GeneralUseStructure> listGeneralStructure { get; set; }
        public StructureCollection()
        {
            listGeneralStructure = new List<GeneralUseStructure>();
        }
    }
}
