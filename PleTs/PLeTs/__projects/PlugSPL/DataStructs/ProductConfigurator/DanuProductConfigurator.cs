using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlugSpl.DataStructs.UmlComponentDiagram;
using System.Xml.Schema;
using System.Xml.Serialization;
using PlugSpl.DataStructs.ComponentPoolManager;
using System.IO;
using System.Reflection;

/* V2: First version of Danu. Danu's general structure is fully operational for V2's necessities. Pending issues below.
 *      
 * V3 (prediction): In V2, Danu was to be used in both the Component Pool and Product Configurator stages of Plug. In V3,
 * a new structure will be created solely for the Product Configurator phase, and compatibility between Bragi and Danu 
 * will be improved. The serialization methods should be improved as well. An important feature to implement is the 
 * proper use of constraints and the acceptance of Bragi objects that make use of cardinality. It is also important to
 * better define the role of interfaces as a separate entity from components, as currently they are regarded as part of it.
 * 
 * V4 (prediction): Danu should be able to accept structures directly from Coeus, without requiring the use of Atlas or
 * Bragi.
 **/

namespace PlugSpl.DataStructs.ProductConfigurator
{
    // Danu: Celtic mother-goddess and ancestral figure.
    public class DanuProductConfigurator: IXmlSerializable
    {
        private Dictionary<string, DanuComponent> components;
        private Dictionary<string, DanuConstraint> constraints;
        private Dictionary<string, DanuInterfaceObject> interfaces;
        private DanuComponent root;

        public DanuComponent[] Components
        {
            get { return this.components.Values.ToArray(); }
        }
        public DanuConstraint[] Constraints
        {
            get { return this.constraints.Values.ToArray(); }
        }
        public DanuInterfaceObject[] Interfaces
        {
            get { return this.interfaces.Values.ToArray(); }
        }
        public DanuComponent Root
        {
            get
            {
                if (root == null)
                {
                    foreach (DanuComponent component in components.Values)
                    {
                        if (component.Sockets.Count == 0)
                        {
                            root = component;
                            return component;
                        }
                    }

                    throw new MissingFieldException("Component Diagram has no root");
                }
                else
                {
                    return root;
                }
            }
        }

        public DanuProductConfigurator(ComponentDiagramBragi bragi)
        {
            components = new Dictionary<string, DanuComponent>();
            constraints = new Dictionary<string, DanuConstraint>();
            interfaces = new Dictionary<string, DanuInterfaceObject>();

            foreach (Component component in bragi.Components)
            {
                AddComponent(component);
            }

            foreach (Association assoc in bragi.Associations)
            {
                AddConstraint(assoc);
            }

            foreach (Component component in bragi.Components)
            {
                foreach (Socket so in component.Sockets)
                {
                    AddSocket(so);
                }
            }
        }
        private DanuProductConfigurator() 
        {
            components = new Dictionary<string, DanuComponent>();
            constraints = new Dictionary<string, DanuConstraint>();
            interfaces = new Dictionary<string, DanuInterfaceObject>();
        }

        /// <summary>
        /// Adds Component to Danu. Adds its name and all its interfaces if they are not present.
        /// </summary>
        public void AddComponent(Component component)
        {
            DanuComponent newComponent = new DanuComponent(component.Name);
            components.Add(newComponent.Name, newComponent);

            foreach (InterfaceObject io in component.Interfaces)
            {
                if (!interfaces.ContainsKey(io.Name))
                {
                    AddInterface(io);
                }

                newComponent.Interfaces.Add(interfaces[io.Name]);
            }

            EshuClass mainClass = new EshuClass(component.Name);
            EshuComponent comp = new EshuComponent(component.Name);

            foreach (DanuInterfaceObject io in newComponent.Interfaces)
            {
                if (io.Eshu == null)
                {
                    EshuInterface newIo = new EshuInterface(io.Name);
                    io.Eshu = newIo;
                }
                mainClass.AddInterface(io.Eshu);
                mainClass.Parent = comp;
                comp.Interfaces.Add(io.Eshu);
                io.Eshu.Parent = mainClass;
            }

            comp.Classes.Add(mainClass);
            newComponent.Specification = comp;
        }
        
        /// <summary>
        /// Adds Interface to Danu. Adds its name, minVar, maxVar and bindingTime.
        /// </summary>
        public void AddInterface(InterfaceObject io)
        {
            SMartyBindingTimeTypes bindingTime = default(SMartyBindingTimeTypes);
            DanuBindingTime newBindingTime = default(DanuBindingTime);
            SMarty attach = null;

            foreach(IAttachment attachment in io.Attachments)
            {
                if (attachment.GetType().Equals(typeof(SMarty)))
                    attach = (SMarty)attachment;
            }

            int minVar = attach.MinSelection;
            int maxVar = attach.MaxSelection;

            foreach (IAttachment attachment in io.Attachments)
            {
                if (attachment.GetType().Equals(typeof(SMarty)))
                {
                    SMarty smarty = (SMarty)attachment;
                    bindingTime = smarty.BindingTime;
                    minVar = smarty.MinSelection;
                    maxVar = smarty.MaxSelection;
                    break;
                }
            }

            if (!bindingTime.Equals(default(SMartyBindingTimeTypes)))
            {
                switch (bindingTime)
                {
                    case SMartyBindingTimeTypes.CompileTime:
                        newBindingTime = DanuBindingTime.CompileTime;
                        break;

                    case SMartyBindingTimeTypes.LinkingTime:
                        newBindingTime = DanuBindingTime.LinkingTime;
                        break;

                    case SMartyBindingTimeTypes.Runtime:
                        newBindingTime = DanuBindingTime.Runtime;
                        break;

                    case SMartyBindingTimeTypes.UpdateTime:
                        newBindingTime = DanuBindingTime.UpdateTime;
                        break;
                }
            }

            DanuInterfaceObject newInterface = new DanuInterfaceObject(io.Name, newBindingTime, minVar, maxVar);
            interfaces.Add(newInterface.Name, newInterface);
        }

        /// <summary>
        /// Adds Socket to a Danu Component. Adds all its attributes, and adds it to 
        /// its parent component and related interface.
        /// </summary>
        public void AddSocket(Socket so)
        {
            DanuSocket newSocket = new DanuSocket(components[so.Parent.Name], interfaces[so.AttachedInterface.Name]);
            AddSocket(newSocket);
        }
        public void AddSocket(DanuSocket so)
        {
            components[so.Parent.Name].Sockets.Add(so);
            interfaces[so.InterfaceUsed.Name].PossibleSockets.Add(so.Parent);
        }

        public void AddConstraint(Association assoc)
        {
            if (assoc.Type.Name.Equals("Mutex"))
            {
                DanuConstraint newConstraint = new DanuConstraint(components[assoc.Source.Name],
                    components[assoc.Target.Name], DanuConstraintTypes.Mutex);
                components[assoc.Source.Name].RelatedConstraints.Add(newConstraint);
                components[assoc.Target.Name].RelatedConstraints.Add(newConstraint);
                constraints.Add(assoc.Source.Name + assoc.Target.Name, newConstraint);
            }
            else
            {
                DanuConstraint newConstraint = new DanuConstraint(components[assoc.Source.Name],
                    components[assoc.Target.Name], DanuConstraintTypes.Requires);
                components[assoc.Source.Name].RelatedConstraints.Add(newConstraint);
                constraints.Add(assoc.Source.Name + assoc.Target.Name, newConstraint);
            }
        }

        public DanuComponent GetComponent(string name)
        {
            return components[name];
        }
        public DanuConstraint GetConstraint(DanuComponent source, DanuComponent target)
        {
            return constraints[source.Name + target.Name];
        }
        public DanuInterfaceObject GetInterface(string name)
        {
            return interfaces[name];
        }

        // Serialization

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            //string currentExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            //FileInfo f = new FileInfo(currentExecutingAssemblyLocation);
            //System.Environment.CurrentDirectory = f.Directory.FullName;

            reader.ReadToFollowing("Interfaces");
            ReadXmlInterfaces(reader.ReadSubtree());

            reader.ReadToFollowing("Components");
            ReadXmlComponents(reader.ReadSubtree());
        }

        public void ReadXmlInterfaces(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    DanuBindingTime btype = default(DanuBindingTime);

                    switch (reader["BindingTime"])
                    {
                        case "Runtime":
                            btype = DanuBindingTime.Runtime;
                            break;

                        case "LinkingTime":
                            btype = DanuBindingTime.LinkingTime;
                            break;

                        case "CompileTime":
                            btype = DanuBindingTime.CompileTime;
                            break;

                        case "UpdateTime":
                            btype = DanuBindingTime.UpdateTime;
                            break;
                    }

                    DanuInterfaceObject io = new DanuInterfaceObject(reader["Name"], btype, 
                        int.Parse(reader["MinVar"]), int.Parse(reader["MaxVar"]));

                    this.interfaces.Add(io.Name, io);
                }
            }

            reader.Close();
        }

        public void ReadXmlComponents(System.Xml.XmlReader reader)
        {
            reader.Read();
            try
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        System.Xml.XmlReader implementsReader;
                        System.Xml.XmlReader availableInterfaceReader;

                        DanuComponent comp = new DanuComponent(reader["Name"]);
                        comp.EshuPath = reader["FileLocation"];

                        XmlSerializer eshuSerializer = new XmlSerializer(typeof(EshuComponent));

                        //removed comp.EshuPath -> impossible to store location :( dããã...

                        TextReader eshuReader = new StreamReader(Environment.CurrentDirectory + "/Cache/" + reader["Name"] + ".eshu");
                        comp.Specification = (EshuComponent)eshuSerializer.Deserialize(eshuReader);

                        this.components.Add(comp.Name, comp);

                        reader.Read();
                        implementsReader = reader.ReadSubtree();
                        ReadXmlComponentImplements(implementsReader, comp);
                        implementsReader.Close();

                        reader.Read();
                        availableInterfaceReader = reader.ReadSubtree();
                        ReadXmlComponentAvailableInterfaces(availableInterfaceReader, comp);
                        availableInterfaceReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                reader.Close();

                throw ex;
            }
            reader.Close();
        }

        public void ReadXmlComponentImplements(System.Xml.XmlReader reader, DanuComponent comp)
        {
            reader.Read();

            while (reader.Read())
            {
                if (reader.Name.Equals("Interface"))
                {
                    DanuInterfaceObject io = GetInterface(reader.ReadElementString());
                    DanuSocket so = new DanuSocket(comp, io);
                    AddSocket(so);
                    foreach (DanuComponent checkComp in Components)
                    {
                        if (checkComp.Interfaces.Contains(io))
                        {
                            io.Eshu.Parent = checkComp.Specification.Classes.FirstOrDefault();
                            io.Eshu.ImplementingParents.Add(checkComp.Specification.Classes.FirstOrDefault());
                        }
                    }
                }
            }

            reader.Close();
        }

        public void ReadXmlComponentAvailableInterfaces(System.Xml.XmlReader reader, DanuComponent comp)
        {
            reader.Read();

            while (reader.Read())
            {
                if (!reader.Name.Equals("Interface") && !reader.Name.Equals("Available_Interfaces"))
                {
                    DanuInterfaceObject io = GetInterface(reader.ReadContentAsString());
                    foreach (EshuInterface ioEshu in comp.Specification.Interfaces)
                    {
                        if(ioEshu.Name.Equals(io.Name))
                        {
                            io.Eshu = ioEshu;
                        }
                    }
                    comp.Interfaces.Add(io);
                }
            }

            reader.Close();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            //string currentExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            //FileInfo f = new FileInfo(currentExecutingAssemblyLocation);
            //System.Environment.CurrentDirectory = f.Directory.FullName;

            XmlSerializer componentSerializer = new XmlSerializer(typeof(DanuComponent));
            XmlSerializer interfaceSerializer = new XmlSerializer(typeof(DanuInterfaceObject));

            writer.WriteAttributeString("Directory", "./Cache/" + Root.Name);
            writer.WriteStartElement("Interfaces");

            foreach (DanuInterfaceObject io in Interfaces)
            {
                interfaceSerializer.Serialize(writer, io);
            }

            writer.WriteEndElement();
            writer.WriteStartElement("Components");

            foreach (DanuComponent comp in Components)
            {
                componentSerializer.Serialize(writer, comp);
            }

            writer.WriteEndElement();
        }
    }
}