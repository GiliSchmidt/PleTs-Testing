using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace PlugSpl.Atlas
{
    public class AtlasFeature : IConstraintMember, IXmlSerializable
    {
        private string name;
        private AtlasIsAbstract isAbstract;
        private int minimum;
        private int maximum;
        private int multipleParentFlag;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public AtlasIsAbstract IsAbstract
        {
            get { return isAbstract; }
            set { isAbstract = value; }
        }
        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }
        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }
        internal int MultipleParentFlag
        {
            get { return multipleParentFlag; }
            set { multipleParentFlag = value; }
        }

        internal AtlasFeature() 
        {
            ResetFeature();
        }
        public AtlasFeature(string name)
        {
            Name = name;
            ResetFeature();
        }
        private void ResetFeature()
        {
            Minimum = 0;
            Maximum = int.MaxValue;
            MultipleParentFlag = 0;
            IsAbstract = false;
        }

        #region Serialization and Display
            public XmlSchema GetSchema()
            {
                return null;
            }
            public void ReadXml(XmlReader reader)
            {
                Name = reader["Name"];
                IsAbstract = reader["IsAbstract"];
                reader.Read();
                reader.Read();
                Minimum = reader.ReadElementContentAsInt();
                Maximum = reader.ReadElementContentAsInt();
            }
            public void WriteXml(XmlWriter writer)
            {
                writer.WriteAttributeString("Name", Name);
                writer.WriteAttributeString("IsAbstract", IsAbstract);
                
                writer.WriteStartElement("Cardinality");
                writer.WriteElementString("Minimum", Minimum.ToString());
                writer.WriteElementString("Maximum", Maximum.ToString());
                writer.WriteEndElement();
            }
            public override string ToString()
            {
                return IsAbstract + " Feature " + Name;
            }
        #endregion

        public string GetMemberName() {
            return this.Name;
        }
    }
}