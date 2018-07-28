using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    public struct AtlasFeatureTuple : IComparable
    {
        private AtlasFeature parent;
        private AtlasFeature child;

        internal AtlasFeatureTuple(AtlasFeature parent, AtlasFeature child)
        {
            this.parent = parent;
            this.child = child;
        }

        public int CompareTo(object obj)
        {
            AtlasFeatureTuple toCompare = (AtlasFeatureTuple)obj;
            if (toCompare.child.Equals(this.child) && toCompare.parent.Equals(this.parent)) return 0;
            return -1;
        }

        public AtlasFeature ParentFeature {
            get{
                return this.parent;
            }
        }

        
        public AtlasFeature ChildFeature {
            get{
                return this.child;
            }
        }
    }
}
