using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlugSpl.DataStructs.ComponentPoolManager;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;

namespace PlugSpl.DataStructs.ProductConfigurator
{
    /// <summary>
    /// Object that represents all instances of a type of interface in Danu.
    /// </summary>
    public class DanuInterfaceObject: IXmlSerializable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// List of components that have sockets of this type.
        /// </summary>
        private List<DanuComponent> possibleSockets;
        public List<DanuComponent> PossibleSockets
        {
            get { return possibleSockets; }
            set { possibleSockets = value; }
        }

        /// <summary>
        /// Dictionary relating components through this interface.
        /// </summary>
        private Dictionary<DanuComponent, DanuSocket> relatedSockets;
        public bool ContainsRelationship()
        {
            if (relatedSockets.Count > 0)
                return true;
            return false;
        }

        private EshuInterface eshu;
        public EshuInterface Eshu
        {
            get { return eshu; }
            set { eshu = value; }
        }

        private int minVar;
        public int MinVar
        {
            get { return minVar; }
            set { minVar = value; }
        }

        private int maxVar;
        public int MaxVar
        {
            get { return maxVar; }
            set { maxVar = value; }
        }

        private DanuBindingTime bindingTime;
        public DanuBindingTime BindingTime
        {
            get { return bindingTime; }
            set { bindingTime = value; }
        }

        public DanuInterfaceObject(string name, DanuBindingTime bindingTime, int minVar, int maxVar)
        {
            this.name = name;
            this.bindingTime = bindingTime;
            this.minVar = minVar;
            this.maxVar = maxVar;
            relatedSockets = new Dictionary<DanuComponent, DanuSocket>();
            possibleSockets = new List<DanuComponent>();
        }
        private DanuInterfaceObject() { }

        public void AddRelationship(DanuComponent parent, DanuSocket socket)
        {
            relatedSockets.Add(parent, socket);
        }
        public void RemoveRelationship(DanuComponent parent)
        {
            relatedSockets.Remove(parent);
        }

        // Serialization

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            // Empty -- Meant to be read from control class
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("MinVar", MinVar.ToString());
            writer.WriteAttributeString("MaxVar", MaxVar.ToString());
            writer.WriteAttributeString("BindingTime", BindingTime.ToString());
        }
    }
}