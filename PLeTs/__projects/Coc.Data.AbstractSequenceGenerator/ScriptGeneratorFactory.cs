//#define MTM
//#define OATS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coc.Data.Interfaces;

#if PL_JUNIT
using Coc.Data.JUnit;
#endif

#if PL_MTM
using Coc.Data.Excel;
#endif

#if PL_OATS
using Coc.Data.OATS;
#endif


namespace Coc.Data.AbstractSequenceGenerator
{
    public class ScriptGeneratorFactory
    {
        public static ScriptGenerator CreateScriptGenerator()
        {
#if PL_JUNIT
            return new JUnitDriverGenerator();
#endif
#if PL_MTM
           
            return new MTMScriptGenerator();
#endif
#if PL_OATS
            return new ParserToExcelOATS();
#endif
            return null;
        }
    }
}
