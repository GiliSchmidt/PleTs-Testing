using System.Collections.Generic;
using System.Xml;
using Lesse.Core.ControlAndConversionStructures;

namespace Lesse.Core.Interfaces
{
    public interface ParsedStructureExporter
    {
        XmlDocument ToXmi(List<GeneralUseStructure> model);
    }
}
