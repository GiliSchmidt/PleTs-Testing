//#define MTM
//#define OATS

using Lesse.Core.Interfaces;
#if PL_JUNIT
using Lesse.JUnit;
#endif

#if PL_MTM
using Lesse;
#endif

#if PL_OATS
using Lesse.OATS;
#endif


namespace Lesse.Factory.AbstractSequenceGenerator
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
