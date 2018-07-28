using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.ComponentPoolManager
{
    public class EshuProperty : IXmlSerializable
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public EshuProperty(string name, string type)
        {
            Name = name;
            Type = type;
        }

        private EshuProperty() { }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            Name = reader["Name"];
            Type = reader["Type"];
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Type", Type);
        }
    }
}
