//#define XMI

using Coc.Data.Xmi;
using Lesse.Core.Interfaces;
#if PL_XMI

#endif


namespace Lesse.Factory.AbstractParser
{
    public class ParsedStructureExporterFactory
    {
        public static ParsedStructureExporter CreateExporter()
        {
#if PL_XMI
            return new XmiExporter();
#endif
            return null;
        }
    }
}
