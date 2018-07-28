using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.ComponentPoolManager
{
    public class EshuInterface : IXmlSerializable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<EshuClass> implementingParents;
        public List<EshuClass> ImplementingParents
        {
            get { return implementingParents; }
            set { implementingParents = value; }
        }


        private List<EshuMethod> signature;
        public EshuMethod[] Signature
        {
            get { return signature.ToArray(); }
        }

        public EshuInterface(string name)
        {
            Name = name;
            signature = new List<EshuMethod>();
            implementingParents = new List<EshuClass>();
        }

        public EshuInterface()
        {
            signature = new List<EshuMethod>();
            implementingParents = new List<EshuClass>();
        }

        public void AddMethod(EshuMethod method)
        {
            signature.Add(method);
            foreach (EshuClass ip in ImplementingParents)
            {
                ip.Methods.Add(method);
            }
        }

        public void RemoveMethod(EshuMethod method)
        {
            signature.Remove(method);
            foreach (EshuClass ip in ImplementingParents)
            {
                ip.Methods.Remove(method);
            }
        }

        public void ClearMethods()
        {
            EshuMethod[] toRemove = signature.ToArray();

            foreach (EshuMethod method in toRemove)
            {
                RemoveMethod(method);
            }
        }

        private EshuClass parent;
        public EshuClass Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer methodSerializer = new XmlSerializer(typeof(EshuMethod));
            System.Xml.XmlReader methodReader;

            Name = reader["Name"];
            reader.Read();
            methodReader = reader.ReadSubtree();
            while (methodReader.ReadToFollowing("EshuMethod"))
            {
                signature.Add((EshuMethod)methodSerializer.Deserialize(methodReader));
            }
            methodReader.Close();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer methodSerializer = new XmlSerializer(typeof(EshuMethod));

            writer.WriteAttributeString("Name", Name);

            writer.WriteStartElement("Signature");
            foreach (EshuMethod method in signature)
            {
                methodSerializer.Serialize(writer, method);
            }
            writer.WriteEndElement();
        }
    }
}
