using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public abstract class IAttachment
    {
        private IUmlObject parent;

        public IUmlObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}