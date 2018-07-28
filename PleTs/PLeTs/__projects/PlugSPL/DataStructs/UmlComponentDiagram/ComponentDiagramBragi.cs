using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

/* V2: First version of Bragi. It is fully implemented for the purposes of V2.
 * 
 * V3 (prediction): Bragi needs a serious overhaul. The serialization method needs to be greatly improved on, the 
 * constraints and constraint validation methods MUST be implemented. Encapsulation needs to be reviewed. The 
 * implementation of SMarty and the connection with the graphical editor need to be reviewed. Furthermore, exportion of
 * Bragi objects into Danu objects must be reviewed, as it may be transferred to this side of the operation. A lower
 * priority is the use of Clafer, with Coeus' lexical analyser, to represent Component Diagrams.
 **/

namespace PlugSpl.DataStructs.UmlComponentDiagram
{
    // Bragi: Norse god of eloquence and poetry.
    public class ComponentDiagramBragi : IXmlSerializable
    {
        private Dictionary<string, IUmlObject> umlObjects   = new Dictionary<string, IUmlObject>();
        private Dictionary<string, IAttachment> attachments = new Dictionary<string, IAttachment>();
        private Dictionary<string, Stereotype> types        = new Dictionary<string, Stereotype>();

        public Component[] Components
        {
            get 
            {
                List<Component> components = new List<Component>();

                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject.GetType().Equals(typeof(Component)))
                    {
                        components.Add((Component)umlObject);
                    }
                }

                return components.AsReadOnly().ToArray();
            }
        }
        public InterfaceObject[] Interfaces
        {
            get
            {
                List<InterfaceObject> interfaces = new List<InterfaceObject>();

                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject.GetType().Equals(typeof(InterfaceObject)))
                    {
                        interfaces.Add((InterfaceObject)umlObject);
                    }
                }

                return interfaces.AsReadOnly().ToArray();
            }
        }
        public Socket[] Sockets
        {
            get
            {
                List<Socket> sockets = new List<Socket>();

                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject.GetType().Equals(typeof(Socket)))
                    {
                        sockets.Add((Socket)umlObject);
                    }
                }

                return sockets.AsReadOnly().ToArray();
            }
        }
        public Association[] Associations
        {
            get
            {
                List<Association> associations = new List<Association>();

                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject.GetType().Equals(typeof(Association)))
                    {
                        associations.Add((Association)umlObject);
                    }
                }

                return associations.AsReadOnly().ToArray();
            }
        }
        public Comment[] Comments
        {
            get
            {
                List<Comment> comments = new List<Comment>();

                foreach (IAttachment attachment in attachments.Values)
                {
                    if (attachment.GetType().Equals(typeof(Comment)))
                    {
                        comments.Add((Comment)attachment);
                    }
                }

                return comments.AsReadOnly().ToArray();
            }
        }
        public SMarty[] SMartyList
        {
            get
            {
                List<SMarty> smartylist = new List<SMarty>();

                foreach (IAttachment attachment in attachments.Values)
                {
                    if (attachment.GetType().Equals(typeof(SMarty)))
                    {
                        smartylist.Add((SMarty)attachment);
                    }
                }

                return smartylist.AsReadOnly().ToArray();
            }
        }

        #region UmlObject-related Methods
            public void AddComponent(Component component)
            {
                umlObjects.Add(component.Name, component);
            }

            public void RemoveComponent(Component component)
            {
                foreach (InterfaceObject inter in component.Interfaces)
                {
                    RemoveInterface(inter);
                }
                foreach (Socket sock in component.Sockets)
                {
                    RemoveSocket(sock);
                }
                umlObjects.Remove(component.Name);
            }

            public void AddInterface(InterfaceObject inter)
            {
                inter.Parent.Interfaces.Add(inter);
                umlObjects.Add(inter.Name, inter);
            }

            public void RemoveInterface(InterfaceObject inter)
            {
                foreach (Socket socket in inter.Sockets)
                {
                    socket.AttachedInterface = null;
                }
                foreach (IAttachment attachment in inter.Attachments)
                {
                    attachments.Remove(attachment.Name);
                }
                umlObjects.Remove(inter.Name);

                inter.Parent.Interfaces.Remove(inter);
            }

            public void AddSocket(Socket sock)
            {
                sock.Parent.Sockets.Add(sock);
                umlObjects.Add(sock.Name, sock);
            }

            public void RemoveSocket(Socket sock)
            {
                sock.AttachedInterface.Sockets.Remove(sock);

                foreach (IAttachment attachment in sock.Attachments)
                {
                    attachments.Remove(attachment.Name);
                }
                umlObjects.Remove(sock.Name);

                sock.Parent.Sockets.Remove(sock);
            }

            public void AddAssociation(Association assoc)
            {
                try
                {
                    umlObjects.Add(assoc.Source.Name + assoc.Target.Name + assoc.Type, assoc);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Association already exists");
                }
            }

            public void RemoveAssociation(Association assoc)
            {
                umlObjects.Remove(assoc.Source.Name + assoc.Target.Name + assoc.Type);
            }

            public IUmlObject GetUmlObject(string name)
            {
                return umlObjects[name];
            }

            public bool HasRoot()
            {
                return RootList().Count == 1;
            }

            public string RootName
            {
                get
                {
                    if (HasRoot())
                    {
                        return RootList().First();
                    }
                    throw new Exception("The component diagram must have exactly one root component.");
                }
            }

            private List<string> RootList()
            {
                List<Component> components = new List<Component>();
                List<Component> parents = new List<Component>();
                List<Socket> sockets = new List<Socket>();
                List<string> roots = new List<string>();
                List<InterfaceObject> interfaceObjects = new List<InterfaceObject>();
                
                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject is Component)
                    {
                        components.Add((Component)umlObject);
                    }
                    if (umlObject.GetType().Equals(typeof(InterfaceObject)))
                    {
                        interfaceObjects.Add((InterfaceObject)umlObject);
                    }
                    if (umlObject.GetType().Equals(typeof(Socket)))
                    {
                        sockets.Add((Socket)umlObject);
                    }
                    //if (umlObject is Association)
                    //{
                    //    associations.Add((Association)umlObject);
                    //}
                }

                foreach (InterfaceObject io in interfaceObjects)
                {
                    if (!parents.Contains(io.Parent))
                    {
                        parents.Add(io.Parent);
                    }
                }

                foreach (Socket so in sockets)
                {
                    if (parents.Contains(so.Parent))
                    {
                        parents.Remove(so.Parent);
                    }
                }
                foreach (Component c in parents)
                {
                    roots.Add(c.Name);
                }
                return roots;
            }
        #endregion

        #region Attachment-related Methods
            public void AddAttachment(IAttachment attachment)
            {
                attachment.Parent.Attachments.Add(attachment);
                attachments.Add(attachment.Name, attachment);
            }

            public void RemoveAttachment(IAttachment attachment)
            {
                attachment.Parent.Attachments.Remove(attachment);
                attachments.Remove(attachment.Name);
            }

            public IAttachment GetAttachment(string name)
            {
                return attachments[name];
            }
        #endregion

        #region Stereotype-related Methods
            public void AddStereotype(Stereotype type)
            {
                types.Add(type.Name, type);
            }

            public void RemoveStereotype(Stereotype type)
            {
                types.Remove(type.Name);
                foreach (IUmlObject umlObject in umlObjects.Values)
                {
                    if (umlObject.Type.Equals(type.Name))
                    {
                        umlObject.Type = null;
                    }
                }
            }

            /// <summary>
            /// This method returns a clone of the stereotype list of this diagram. Alterations to the returned list
            /// will not incur in alterations to the original list. Use for iteration purposes.
            /// </summary>
            public List<Stereotype> CopyStereotypes()
            {
                List<Stereotype> cloneTypes = new List<Stereotype>();
                foreach (Stereotype type in types.Values)
                {
                    cloneTypes.Add((Stereotype)type.Clone());
                }
                return cloneTypes;
            }

            public Stereotype GetStereotype(string name)
            {
                return types[name];
            }
        #endregion

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Stereotype":
                            Stereotype type = new Stereotype(reader["Name"]);
                            AddStereotype(type);
                            break;
                        case "Socket":
                            Socket so = new Socket((Component)GetUmlObject(reader["Parent"]),
                                (InterfaceObject)GetUmlObject(reader["AttachedInterface"]), reader["Name"]);

                            if (reader["Stereotype"] != null)
                            {
                                so.Type = types[reader["Stereotype"]];
                            }
                            AddSocket(so);
                            break;

                        case "SMarty":
                            SMartyBindingTimeTypes btype = default(SMartyBindingTimeTypes);

                            switch (reader["BindingTime"])
                            {
                                case "Runtime":
                                    btype = SMartyBindingTimeTypes.Runtime;
                                    break;

                                case "LinkingTime":
                                    btype = SMartyBindingTimeTypes.LinkingTime;
                                    break;

                                case "CompileTime":
                                    btype = SMartyBindingTimeTypes.CompileTime;
                                    break;

                                case "UpdateTime":
                                    btype = SMartyBindingTimeTypes.UpdateTime;
                                    break;
                            }

                            SMarty smar = new SMarty(reader["Name"], btype, GetUmlObject(reader["Parent"]));
                            smar.MinSelection = int.Parse(reader["MinSelection"]);
                            smar.MaxSelection = int.Parse(reader["MaxSelection"]);
                            AddAttachment(smar);
                            break;

                        case "InterfaceObject":
                            InterfaceObject io = new InterfaceObject(reader["Name"], 
                                (Component)umlObjects[reader["Parent"]]);
                            if (reader["Stereotype"] != null)
                            {
                                io.Type = types[reader["Stereotype"]];
                            }
                            AddInterface(io);
                            break;

                        case "Component":
                            Component comp = new Component(reader["Name"]);
                            if (reader["Stereotype"] != null)
                            {
                                comp.Type = types[reader["Stereotype"]];
                            }
                            AddComponent(comp);
                            break;

                        case "Comment":
                            Comment com = new Comment(reader["Name"], GetUmlObject(reader["Parent"]));
                            reader.Read();
                            com.Content = reader.Value;
                            AddAttachment(com);
                            break;

                        case "Association":
                            Association link = new Association(GetUmlObject(reader["Source"]), 
                                GetUmlObject(reader["Target"]));
                            if (reader["Stereotype"] != null)
                            {
                                link.Type = types[reader["Stereotype"]];
                            }
                            if (reader["Type"] != null)
                            {
                                link.Type = types[reader["Type"]];
                            }
                            AddAssociation(link);
                            break;
                    }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer stereotypeSerializer = new XmlSerializer(typeof(Stereotype));
            XmlSerializer socketSerializer = new XmlSerializer(typeof(Socket));
            XmlSerializer smartySerializer = new XmlSerializer(typeof(SMarty));
            XmlSerializer interfaceObjectSerializer = new XmlSerializer(typeof(InterfaceObject));
            XmlSerializer componentSerializer = new XmlSerializer(typeof(Component));
            XmlSerializer commentSerializer = new XmlSerializer(typeof(Comment));
            XmlSerializer associationSerializer = new XmlSerializer(typeof(Association));

            foreach (Stereotype type in types.Values)
            {
                stereotypeSerializer.Serialize(writer, type);
            }

            List<Component> components = new List<Component>();
            List<InterfaceObject> interfaceObjects = new List<InterfaceObject>();
            List<Socket> sockets = new List<Socket>();
            List<Association> associations = new List<Association>();

            foreach (IUmlObject umlObject in umlObjects.Values)
            {
                if (umlObject.GetType().Equals(typeof(Component)))
                {
                    components.Add((Component)umlObject);
                }
                if (umlObject.GetType().Equals(typeof(InterfaceObject)))
                {
                    interfaceObjects.Add((InterfaceObject)umlObject);
                }
                if (umlObject.GetType().Equals(typeof(Socket)))
                {
                    sockets.Add((Socket)umlObject);
                }
                if (umlObject.GetType().Equals(typeof(Association)))
                {
                    associations.Add((Association)umlObject);
                }
            }

            foreach (Component component in components)
            {
                componentSerializer.Serialize(writer, component);
            }

            foreach (InterfaceObject io in interfaceObjects)
            {
                interfaceObjectSerializer.Serialize(writer, io);
            }

            foreach (Socket so in sockets)
            {
                socketSerializer.Serialize(writer, so);
            }

            foreach (Association associ in associations)
            {
                associationSerializer.Serialize(writer, associ);
            }

            foreach (IAttachment attachment in attachments.Values)
            {
                if (attachment.GetType().Equals(typeof(Comment)))
                {
                    commentSerializer.Serialize(writer, (Comment)attachment);
                }
                if (attachment.GetType().Equals(typeof(SMarty)))
                {
                    smartySerializer.Serialize(writer, (SMarty)attachment);
                }
            }
        }
    }
}