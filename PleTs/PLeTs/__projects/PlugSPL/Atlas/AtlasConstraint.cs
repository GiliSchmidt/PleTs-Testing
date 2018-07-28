using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace PlugSpl.Atlas
{
    public class AtlasConstraint : IXmlSerializable
    {
        private List<IConstraintMember> constraints;

        public List<IConstraintMember> Constraints
        {
            get { return constraints; }
            set { constraints = value; }
        }

        public AtlasConstraint()
        {
            constraints = new List<IConstraintMember>();
        }

        #region Serialization and Display
            public XmlSchema GetSchema()
            {
                return null;
            }
            public void ReadXml(XmlReader reader)
            {
                throw new NotImplementedException("A Constraint cannot be initialized directly from itself because of "
                        + "dependencies with Features in the model.");
            }
            public void WriteXml(XmlWriter writer)
            {
                string content = "";
                foreach (IConstraintMember member in Constraints)
                {
                    if (!member.Equals(Constraints.First()))
                        content += " ";

                    if (member.GetType().Equals(typeof(AtlasFeature)))
                    {
                        AtlasFeature feature = (AtlasFeature)member;
                        content += feature.Name;
                    }
                    else
                    {
                        AtlasConstraintOperator oper = (AtlasConstraintOperator)member;
                        content += ((int)oper.Type).ToString();
                    }
                }
                writer.WriteAttributeString("Content", content);
            }
        #endregion

    }
}