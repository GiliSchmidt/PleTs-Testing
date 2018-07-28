using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.Atlas
{
    public struct AtlasIsAbstract
    {
        private bool isAbstract;

        internal AtlasIsAbstract(bool value) 
        {
            isAbstract = value;
        }
        internal AtlasIsAbstract(string value)
        {
            if(value.Equals("Abstract") || value.Equals("true"))
                isAbstract = true;
            else
                isAbstract = false;
        }

        public static implicit operator bool(AtlasIsAbstract item)
        {
            return item.isAbstract;
        }
        public static implicit operator string(AtlasIsAbstract item)
        {
            if (item.isAbstract)
                return "Abstract";
            return "NotAbstract";
        }

        public static implicit operator AtlasIsAbstract(bool item)
        {
            return new AtlasIsAbstract(item);
        }
        public static implicit operator AtlasIsAbstract(string item)
        {
            return new AtlasIsAbstract(item);
        }
    }
}
