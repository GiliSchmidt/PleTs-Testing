//#define XMI
//#define OATS
//#define LR

#if PL_XMI
#endif
using System;
using Coc.Data.Xmi;
using Lesse.Core.Interfaces;
using Lesse.OATS.OATSScriptGenerator;


#if PL_OATS
using Lesse.OATS.OgmaJParser;
#endif

#if PL_LR
using Coc.Data.ReadLR;
#endif

namespace Lesse.Factory.AbstractParser
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