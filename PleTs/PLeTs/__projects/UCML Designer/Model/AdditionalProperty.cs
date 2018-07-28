using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeConnectors
{
    public class AdditionalProperty
    {
        public string name { get; set; }
        public string value { get; set; }

        public AdditionalProperty()
        {
            this.name = null;
            this.value = null;
        }
    }
}
