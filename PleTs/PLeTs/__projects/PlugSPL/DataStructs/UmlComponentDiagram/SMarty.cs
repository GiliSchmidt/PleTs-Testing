using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class SMarty : IAttachment, IXmlSerializable
    {
        public SMarty(string name, SMartyBindingTimeTypes bindingTime, IUmlObject parent)
        {
            this.Name = name;
            this.bindingTime = bindingTime;
            this.Parent = parent;
            this.minSelection = 0;
            this.maxSelection = int.MaxValue;
        }

        private SMarty() { }

        private int minSelection;

        public int MinSelection
        {
            get { return minSelection; }
            set { minSelection = value; }
        }

        private int maxSelection;

        public int MaxSelection
        {
            get { return maxSelection; }
            set { maxSelection = value; } 
        }

        private SMartyBindingTimeTypes bindingTime;

        public SMartyBindingTimeTypes BindingTime
        {
            get { return bindingTime; }
            set { bindingTime = value; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            //Empty
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("MinSelection", minSelection.ToString());
            writer.WriteAttributeString("MaxSelection", maxSelection.ToString());
            writer.WriteAttributeString("BindingTime", bindingTime.ToString());
            writer.WriteAttributeString("Parent", Parent.Name);
        }
    }
}