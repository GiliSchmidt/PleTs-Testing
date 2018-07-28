using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class Comment : IAttachment, IXmlSerializable
    {
        public Comment(string name, IUmlObject parent)
        {
            this.Name = name;
            this.Parent = parent;
            this.content = "";
        }

        private Comment() { }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
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
            writer.WriteAttributeString("Parent", Parent.Name);
            writer.WriteElementString("Content", content);
        }
    }
}