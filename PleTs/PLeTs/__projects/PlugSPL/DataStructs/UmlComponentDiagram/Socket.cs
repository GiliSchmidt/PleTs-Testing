using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class Socket : IUmlObject, IXmlSerializable
    {
        public Socket(Component parent, InterfaceObject attachedInterface, string name)
        {
            Name = name;
            this.parent = parent;
            this.attachedInterface = attachedInterface;
            attachedInterface.Sockets.Add(this);
            this.Attachments = new List<IAttachment>();
            this.Type = null;
        }

        private Socket() 
        {
            this.Attachments = new List<IAttachment>();
        }

        private InterfaceObject attachedInterface;

        public InterfaceObject AttachedInterface
        {
            get { return attachedInterface; }
            set { attachedInterface = value; }
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
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Parent", parent.Name);
            if (attachedInterface != null)
            {
                writer.WriteAttributeString("AttachedInterface", attachedInterface.Name);
            }
            if (this.Type != null)
            {
                writer.WriteAttributeString("Stereotype", this.Type.Name);
            }
        }
    }
}