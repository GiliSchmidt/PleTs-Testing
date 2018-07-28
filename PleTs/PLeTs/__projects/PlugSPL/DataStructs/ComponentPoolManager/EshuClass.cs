using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.ComponentPoolManager 
{
    /// <summary>
    /// Represents a class and its internal structure.
    /// </summary>
    public class EshuClass : IXmlSerializable
    {
        /// <summary>
        /// Class name. Must respect language definitions, such as spacing, case sensitivity, etc.
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private EshuComponent parent;
        public EshuComponent Parent
        {
            get { return parent; }
            set { parent = value; }
        }


        /// <summary>
        /// List of interfaces implemented by class.
        /// </summary>
        private List<EshuInterface> interfaces;
        public EshuInterface[] Interfaces
        {
            get { return interfaces.ToArray(); }
        }

        private List<EshuMethod> methods;
        public List<EshuMethod> Methods
        {
            get { return methods; }
            set { methods = value; }
        }

        public void AddInterface(EshuInterface io)
        {
            if (!Interfaces.Contains(io))
            {
                interfaces.Add(io);
                foreach (EshuMethod method in io.Signature)
                {
                    if (!Methods.Contains(method))
                    {
                        Methods.Add(method);
                    }
                }
            }
            io.ImplementingParents.Add(this);
        }
        public void RemoveInterface(EshuInterface io)
        {
            if (interfaces.Remove(io))
            {
                foreach (EshuMethod method in io.Signature)
                {
                    Methods.Remove(method);
                }
            }
            io.ImplementingParents.Remove(this);
        }

        public EshuClass(string name)
        {
            Name = name;
            methods = new List<EshuMethod>();
            interfaces = new List<EshuInterface>();
        }

        public EshuClass()
        {
            methods = new List<EshuMethod>();
            interfaces = new List<EshuInterface>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            //TODO Sblabs
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteStartElement("Interfaces");
            foreach (EshuInterface io in interfaces)
            {
                writer.WriteElementString("Interface", io.Name);
            }
            writer.WriteEndElement();
        }
    }
}