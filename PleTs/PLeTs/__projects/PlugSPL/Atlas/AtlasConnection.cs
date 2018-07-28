using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.Atlas
{
    public class AtlasConnection : IComparable, IXmlSerializable
    {
        private AtlasFeature parent;
        private AtlasFeature child;
        private AtlasConnectionType type;

        public AtlasFeature Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public AtlasFeature Child
        {
            get { return child; }
            set { child = value; }
        }
        public AtlasConnectionType Type
        {
            get { return type; }
            set { type = value; }
        }

        internal AtlasConnection() { }
        public AtlasConnection(AtlasFeature parent, AtlasFeature child, AtlasConnectionType type)
        {
            this.parent = parent;
            this.child = child;
            this.type = type;
        }

        public static implicit operator AtlasConnectionType(AtlasConnection item)
        {
            return item.Type;
        }

        public int CompareTo(object obj)
        {
            AtlasConnection con = (AtlasConnection)obj;
            if (this.parent.Equals(con.parent) && this.child.Equals(con.child)) return 0;
            return -1;
        }

        #region Serialization
            public XmlSchema GetSchema()
            {
                return null;
            }
            public void ReadXml(XmlReader reader)
            {
                throw new NotImplementedException("A Connection cannot be initialized directly from itself because of "
                    + "dependencies with Features in the model.");
            }
            public void WriteXml(XmlWriter writer)
            {
                writer.WriteAttributeString("Parent", Parent.Name);
                writer.WriteAttributeString("Child", Child.Name);
                writer.WriteAttributeString("Type", ((int)Type).ToString());
            }
        #endregion
    }
}