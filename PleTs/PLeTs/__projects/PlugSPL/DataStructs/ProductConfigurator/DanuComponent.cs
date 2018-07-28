using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlugSpl.DataStructs.ComponentPoolManager;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace PlugSpl.DataStructs.ProductConfigurator
{
    /// <summary>
    /// Represents a single instance of a component.
    /// </summary>
    public class DanuComponent: IXmlSerializable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Interfaces made available by this component.
        /// </summary>
        private List<DanuInterfaceObject> interfaces;
        public List<DanuInterfaceObject> Interfaces
        {
            get { return interfaces; }
            set { interfaces = value; }
        }

        /// <summary>
        /// Sockets used by this component.
        /// </summary>
        private List<DanuSocket> sockets;
        public List<DanuSocket> Sockets
        {
            get { return sockets; }
            set { sockets = value; }
        }

        /// <summary>
        /// "Requires" constraints that part from this component or "Mutex" constraints that are linked to it.
        /// </summary>
        private List<DanuConstraint> relatedConstraints;
        public List<DanuConstraint> RelatedConstraints
        {
            get { return relatedConstraints; }
            set { relatedConstraints = value; }
        }

        /// <summary>
        /// The path of the text file containing the location of the Eshu specifications, if not standard.
        /// </summary>
        private string eshuPath;
        public string EshuPath
        {
            get { return eshuPath; }
            set { eshuPath = value; }
        }

        /// <summary>
        /// Eshu objects with the specifications of the Component.
        /// </summary>
        private EshuComponent specification;
        public EshuComponent Specification
        {
            get { return specification; }
            set { specification = value; }
        }

        /// <summary>
        /// Files that are required for compiling this component.
        /// </summary>
        private List<string> requiredFiles;
        public List<string> RequiredFiles
        {
            get { return requiredFiles; }
            set { requiredFiles = value; }
        }

        public DanuComponent(string name)
        {
            this.name = name;
            this.interfaces = new List<DanuInterfaceObject>();
            this.sockets = new List<DanuSocket>();
            this.relatedConstraints = new List<DanuConstraint>();
        }
        private DanuComponent() { }

        // Serialization

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            // Empty -- Meant to be read from control.
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            TextWriter fileWriter=null;
            try
            {
                writer.WriteAttributeString("Name", Name);
                writer.WriteAttributeString("FileLocation", "./Cache/" + Name + ".eshu");

                fileWriter = new StreamWriter("./Cache/" + Name + ".eshu");
                XmlSerializer eshuSerializer = new XmlSerializer(typeof(EshuComponent));

                eshuSerializer.Serialize(fileWriter, specification);

                writer.WriteStartElement("Implements");
                foreach (DanuSocket so in Sockets)
                {
                    writer.WriteElementString("Interface", so.InterfaceUsed.Name);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("Available_Interfaces");
                foreach (DanuInterfaceObject io in Interfaces)
                {
                    writer.WriteElementString("Interface", io.Name);
                }

                fileWriter.Close();
                writer.WriteEndElement();
            }
            catch (Exception ex)
            {
                if (fileWriter != null)
                    fileWriter.Close();

                throw ex;
            }
        }
    }
}