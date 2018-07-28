using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    /// <summary>
    /// This ridiculous class exists to encapsulate the possibility of tags being added to the model later on.
    /// </summary>
    public class Stereotype : ICloneable, IXmlSerializable
    {
        public Stereotype(string name)
        {
            this.name = name;
        }

        private Stereotype() { }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Clone()
        {
            Stereotype type = new Stereotype(name);
            return type;
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
        }
    }
}