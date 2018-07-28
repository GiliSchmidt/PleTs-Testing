using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.ComponentPoolManager
{
    public class EshuMethod : IXmlSerializable
    {
        private string returnType;
        public string ReturnType
        {
            get { return returnType; }
            set { returnType = value; }
        }

        /// <summary>
        /// Represents whether a method is public or private. True represents public.
        /// </summary>
        private bool accessModifier;
        public bool AccessModifier
        {
            get { return accessModifier; }
            set { accessModifier = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<EshuProperty> parameters;
        public List<EshuProperty> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public EshuMethod(string name, string returnType)
        {
            AccessModifier = false;
            Name = name;
            ReturnType = returnType;
            parameters = new List<EshuProperty>();
        }

        private EshuMethod()
        {
            parameters = new List<EshuProperty>();
            AccessModifier = false;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer parameterSerializer = new XmlSerializer(typeof(EshuProperty));
            System.Xml.XmlReader propertyReader;

            Name = reader["Name"];
            ReturnType = reader["ReturnType"];
            if(reader["AccessModifier"].Equals("True"))
                AccessModifier = true;

            propertyReader = reader.ReadSubtree();
            while (propertyReader.ReadToFollowing("EshuProperty"))
            {
                Parameters.Add((EshuProperty)parameterSerializer.Deserialize(propertyReader));
            }
            propertyReader.Close();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer parameterSerializer = new XmlSerializer(typeof(EshuProperty));
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("AccessModifier", AccessModifier.ToString());
            writer.WriteAttributeString("ReturnType", ReturnType);
            writer.WriteStartElement("Parameters");
            foreach (EshuProperty par in Parameters)
            {
                parameterSerializer.Serialize(writer, par);
            }
            writer.WriteEndElement();
        }
    }
}
