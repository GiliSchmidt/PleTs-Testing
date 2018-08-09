using System.Collections.Generic;
using Lesse.Core.ControlAndConversionStructures;
using Lesse.Core.ControlStructure;

namespace Lesse.Core.ConversionUnit
{
    public interface ModelingStructureConverter
    {
        List<GeneralUseStructure> Converter(List<GeneralUseStructure> listModel, StructureType type);
    }
}
