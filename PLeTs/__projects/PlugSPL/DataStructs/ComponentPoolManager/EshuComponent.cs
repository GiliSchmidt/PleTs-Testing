using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

/* V2: First version of Eshu. It is fully implemented in what refers to the requirements of V2.
 * 
 * V3 (prediction): Eshu's style of serialization will serve as the cornerstone of the new serialization methods of the
 * other structures. It is important to improve it to allow the definition of multiple classes within a single component,
 * which is currently not fully implemented (though the groundwork for this is present).
 **/

namespace PlugSpl.DataStructs.ComponentPoolManager 
{
    // Eshu: African deity of the crossroads, responsible for the connection between the mortal and divine.
    public class EshuComponent : IXmlSerializable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<EshuClass> classes;
        public List<EshuClass> Classes
        {
            get { return classes; }
            set { classes = value; }
        }

        private string guid;
        public string GUID
        {
            get { return guid; }
        }

        private List<EshuInterface> interfaces;
        public List<EshuInterface> Interfaces
        {
            get { return interfaces; }
            set { interfaces = value; }
        }

        public EshuClass GetClass(string name)
        {
            foreach (EshuClass cls in classes)
            {
                if (cls.Name.Equals(name)) return cls;
            }
            return null;
        }

        public EshuInterface GetInterface(string name)
        {
            foreach (EshuInterface io in interfaces)
            {
                if (io.Name.Equals(name)) return io;
            }
            return null;
        }

        public EshuComponent(string name)
        {
            Name = name;
            classes = new List<EshuClass>();
            interfaces = new List<EshuInterface>();
            guid = Guid.NewGuid().ToString();
        }

        public EshuComponent()
        {
            classes = new List<EshuClass>();
            interfaces = new List<EshuInterface>();
            guid = Guid.NewGuid().ToString();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            //string currentExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            //FileInfo f = new FileInfo(currentExecutingAssemblyLocation);
            //System.Environment.CurrentDirectory = f.Directory.FullName;

            XmlSerializer interfaceSerializer = new XmlSerializer(typeof(EshuInterface));
            XmlSerializer classSerializer = new XmlSerializer(typeof(EshuClass));

            Name = reader["Name"];

            reader.ReadToFollowing("Interfaces");
            System.Xml.XmlReader interfaceReader = reader.ReadSubtree();

            interfaceReader.Read();
            while (interfaceReader.ReadToFollowing("EshuInterface"))
            {
                interfaces.Add((EshuInterface)interfaceSerializer.Deserialize(interfaceReader));
            }
            interfaceReader.Close();

            reader.ReadToFollowing("Classes");
            System.Xml.XmlReader classReader = reader.ReadSubtree();

            classReader.Read();
            while (classReader.ReadToFollowing("EshuClass"))
            {
                if (classReader.Name.Equals("EshuClass"))
                {
                    EshuClass newClass = new EshuClass(reader["Name"]);
                    while (classReader.ReadToFollowing("Interface"))
                    {
                        newClass.AddInterface(GetInterface(reader.ReadElementString()));
                    }
                    foreach (EshuInterface io in newClass.Interfaces)
                    {
                        io.Parent = newClass;
                    }
                    newClass.Parent = this;
                    classes.Add(newClass);
                }
            }
            classReader.Close();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            //string currentExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            //FileInfo f = new FileInfo(currentExecutingAssemblyLocation);
            //System.Environment.CurrentDirectory = f.Directory.FullName;

            XmlSerializer interfaceSerializer = new XmlSerializer(typeof(EshuInterface));
            XmlSerializer classSerializer = new XmlSerializer(typeof(EshuClass));

            writer.WriteAttributeString("Name", Name);
            
            writer.WriteStartElement("Interfaces");

            foreach (EshuInterface io in Interfaces)
            {
                interfaceSerializer.Serialize(writer, io);
            }

            writer.WriteEndElement();
            writer.WriteStartElement("Classes");

            foreach (EshuClass cls in Classes)
            {
                classSerializer.Serialize(writer, cls);
            }

            writer.WriteEndElement();
        }
    }
}