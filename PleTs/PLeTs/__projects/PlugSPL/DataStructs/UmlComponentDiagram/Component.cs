using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class Component : IUmlObject, IXmlSerializable
    {
        public Component(string name)
        {
            this.Name = name;
            this.Interfaces = new List<InterfaceObject>();
            this.Sockets = new List<Socket>();
            this.Type = null;
            this.Attachments = new List<IAttachment>();
        }

        private Component() 
        {
            this.Attachments = new List<IAttachment>();
            this.Interfaces = new List<InterfaceObject>();
            this.Sockets = new List<Socket>();
        }

        private List<InterfaceObject> interfaces;

        public List<InterfaceObject> Interfaces
        {
            get { return interfaces; }
            set { interfaces = value; }
        }

        private List<Socket> sockets;

        public List<Socket> Sockets
        {
            get { return sockets; }
            set { sockets = value; }
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
            if (this.Type != null)
            {
                writer.WriteAttributeString("Stereotype", this.Type.ToString());
            }
        }
    }
}