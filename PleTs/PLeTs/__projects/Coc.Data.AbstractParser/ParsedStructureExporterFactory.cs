//#define XMI

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coc.Data.Interfaces;

#if PL_XMI
using Coc.Data.Xmi;
#endif


namespace Coc.Data.AbstractParser
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
