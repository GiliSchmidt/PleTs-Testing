using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public abstract class IUmlObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<IAttachment> attachments;

        public List<IAttachment> Attachments
        {
            get { return attachments; }
            set { attachments = value; }
        }

        private Stereotype type;

        public Stereotype Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
