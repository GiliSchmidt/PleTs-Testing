//#define HSI
//#define DFS
//#define Wp
//#define OATS

using Lesse.Core.ControlStructure;
using Lesse.Core.Interfaces;

#if PL_HSI
using Coc.Data.HSI;
#endif

#if PL_DFS
using Coc.Data.DFS;
using Coc.Data.DFSforTCC;
#endif

#if PL_WP
using Coc.Data.Wpartial;
#endif

#if PL_OATS
#endif

namespace Lesse.Factory.AbstractSequenceGenerator
{
    public class SequenceGeneratorFactory
    {
        public static SequenceGenerator CreateSequenceGenerator(StructureType type)
        {
            switch (type)
            {
#if PL_HSI
                case StructureType.HSI:
                    return new HsiMethod();
#endif
#if PL_DFS
                case StructureType.DFS_TCC:
                    return new DepthFirstSearchForTCC();
                case StructureType.DFS:
                    return new DepthFirstSearch();
#endif
#if PL_WP
                case StructureType.Wp:
                    return new Wp();
#endif
#if PL_OATS
                case StructureType.OATS:
                    return new SequenceGenerator();
#endif

            }
            return null;
        }
    }
}
