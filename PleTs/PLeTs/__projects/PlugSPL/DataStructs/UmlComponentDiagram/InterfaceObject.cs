using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class InterfaceObject : IUmlObject, IXmlSerializable
    {
        public InterfaceObject(string name, Component parent)
        {
            this.Name = name;
            this.parent = parent;
            this.Attachments = new List<IAttachment>();
            this.sockets = new List<Socket>();
            this.Type = null;
        }

        private InterfaceObject() 
        {
            this.Attachments = new List<IAttachment>();
            this.sockets = new List<Socket>();
        }

        private List<Socket> sockets;

        public List<Socket> Sockets
        {
            get { return sockets; }
            set { sockets = value; }
        }

        private Component parent;

        public Component Parent
        {
            get { return parent; }
            set { parent = value; }
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
            writer.WriteAttributeString("Name", this.Name);
            writer.WriteAttributeString("Parent", this.parent.Name);
            if (this.Type != null)
            {
                writer.WriteAttributeString("Stereotype", this.Type.ToString());
            }
        }
    }
}