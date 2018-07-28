using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    public class Association : IUmlObject, IXmlSerializable
    {
        /// <summary>
        /// Constructor of Association objects.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public Association(IUmlObject source, IUmlObject target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException("Source and Target of an Association cannot be empty");
            }
            this.source = source;
            this.target = target;
            this.Name = null;
        }

        private Association() { }

        private IUmlObject source;
        public IUmlObject Source
        {
            get { return source; }
            set
            {
                if (!value.GetType().Equals(typeof(Association)))
                {
                    source = value;
                }
            }
        }

        private IUmlObject target;
        public IUmlObject Target
        {
            get { return target; }
            set
            {
                if (!value.GetType().Equals(typeof(Association)))
                {
                    target = value;
                }
            }
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
            writer.WriteAttributeString("Source", source.Name);
            writer.WriteAttributeString("Target", target.Name);
            writer.WriteAttributeString("Type", this.Type.Name);
        }
    }
}