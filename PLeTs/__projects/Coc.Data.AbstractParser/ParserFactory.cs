//#define XMI
//#define OATS
//#define LR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coc.Data.Interfaces;

#if PL_XMI
using Coc.Data.Xmi;
#endif

#if PL_OATS
using OgmaJOATSParser;
#endif

#if PL_LR
using Coc.Data.ReadLR;
#endif

namespace Coc.Data.AbstractParser
{
    public class ParserFactory
    {
        public static Parser CreateParser(String parserType)
        {
            switch (parserType)
            {
#if PL_OATS
                case "Script JAVA":
                    return new OgmaJ();
#endif
#if PL_XMI
                case "Astah SeqDiag XML":
                    return new SequenceDiagramImporter();
                case "Astah XML":
                    return new SequenceDiagramImporter();
                case "Argo XML":
                    return new XmlArgoUml();
#endif
#if PL_LR
                case "LoadRunnerToXMI":
                    return new LoadRunnerToXMI();
#endif
            }
            return null;
        }
    }
}