using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

/* V2: First version of Atlas. It is finished for the purposes of V2. It is already capable of receiving cardinality and 
 * constraint information, but this is not yet fully implemented in connection with the graphic editors.
 * 
 * V2.1: Atlas has been fully revamped in what relates to code presentation design and accessibility. The serialization
 * has been revamped accordingly with the precedent set in Eshu V2. It has been changed to allow the persistence of
 * unfinished Feature Models, as well as better integration with a GUI. Constraints system has been completely modified
 * and is now implemented with an appropriate state machine. Serialization has been tested and works perfectly.
 **/

namespace PlugSpl.Atlas
{
    // Atlas: Greek titan that holds the earth on his shoulders.
    public class AtlasFeatureModel : IXmlSerializable
    {
        private AtlasFeature rootFeature;
        private Dictionary<string, AtlasFeature> features;
        private Dictionary<AtlasFeatureTuple, AtlasConnection> connections;
        private List<AtlasConstraint> constraints;

        public AtlasConnection[] Connections 
        {
            get { return this.connections.Values.ToArray(); }
        }
        public AtlasFeature[] Features 
        {
            get { return this.features.Values.ToArray(); }
        }
        public AtlasConstraint[] Constraints
        {
            get { return this.constraints.AsReadOnly().ToArray(); }
        }
        public bool IsValid
        {
            get
            {
                foreach (AtlasFeature feature in features.Values)
                {
                    if (feature.MultipleParentFlag == 0)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the root feature name
        /// </summary>
        public string RootFeatureName { get { return rootFeature.Name; } }

        #region Model-related Operations
            public AtlasFeatureModel(AtlasFeature rootFeature)
            {
                ClearModel();

                CreateFeatureModel(rootFeature);
            }
            public AtlasFeatureModel() 
            {
                ClearModel();
            }
            public void ClearModel()
            {
                features = new Dictionary<string, AtlasFeature>();
                connections = new Dictionary<AtlasFeatureTuple, AtlasConnection>();
                constraints = new List<AtlasConstraint>();
                rootFeature = null;
            }
            public void CreateFeatureModel(AtlasFeature rootFeature)
            {
                CheckRoot(rootFeature);

                AddFeature(rootFeature);
                this.rootFeature = rootFeature;
            }
            public void CreateFeatureModel(string rootName)
            {
                CreateFeatureModel(new AtlasFeature(rootName));
            }
            internal void CheckRoot(AtlasFeature rootFeature)
            {
                if (this.rootFeature != null) throw new InvalidOperationException("This Feature Model already has a root");
                if (rootFeature == null) throw new ArgumentNullException("Root cannot be null");
            }
        #endregion

        #region Feature-related Operations
            public void AddFeature(AtlasFeature newFeature)
            {
                if (features.ContainsKey(newFeature.Name)) 
                    throw new ArgumentException("Feature already exists");

                features.Add(newFeature.Name, newFeature);
            }
            public void AddFeature(AtlasFeature newFeature, AtlasFeature parentFeature, AtlasConnectionType type)
            {
                if (features.ContainsKey(newFeature.Name)) throw new ArgumentException("Feature already exists");
                if (!features.ContainsKey(parentFeature.Name)) throw new ArgumentException("Parent Feature does not exist");

                AddFeature(newFeature);
                AddConnection(parentFeature, newFeature, type);
            }
            public void AddFeature(string name)
            {
                if (features.ContainsKey(name))
                    throw new ArgumentException("Feature already exists");

                AddFeature(new AtlasFeature(name));
            }
            public void AddFeature(string child, string parent, AtlasConnectionType type)
            {
                AtlasFeature parentFeature = GetFeature(parent);
                AtlasFeature childFeature = new AtlasFeature(child);
                AddFeature(childFeature, parentFeature, type);
            }
            public bool RemoveFeature(AtlasFeature toRemove)
            {
                if (!features.ContainsKey(toRemove.Name)) return false;

                foreach (AtlasConnection con in Connections)
                {
                    if (con.Parent.Equals(toRemove))
                    {
                        con.Child.MultipleParentFlag--;
                        connections.Remove(new AtlasFeatureTuple(con.Parent, con.Child));
                    }
                    else if (con.Child.Equals(toRemove))
                    {
                        connections.Remove(new AtlasFeatureTuple(con.Parent, con.Child));
                    }
                }

                return features.Remove(toRemove.Name);
            }
            public bool RemoveFeature(string toRemove)
            {
                return RemoveFeature(GetFeature(toRemove));
            }
            public bool RemoveSubTree(AtlasFeature toRemove)
            {
                //TODO Remove the subtree that has toRemove as its root.
                throw new NotImplementedException("Method not implemented");
            }
            public bool RemoveSubTree(string toRemove)
            {
                throw new NotImplementedException("Method not implemented");
                //return RemoveSubTree(GetFeature(toRemove));
            }
            public AtlasFeature GetFeature(string featureName)
            {
                return features[featureName];
            }
            public AtlasFeature[] GetChildren(AtlasFeature parent)
            {
                List<AtlasFeature> features = new List<AtlasFeature>();

                foreach(KeyValuePair<AtlasFeatureTuple, AtlasConnection> con in this.connections)
                    if(con.Key.ParentFeature == parent)
                        features.Add(con.Key.ChildFeature);

                return features.ToArray();
                //TODO Return all the children nodes of the specified feature.
                //throw new NotImplementedException("Method not implemented");
            }
            public AtlasFeature[] GetChildren(string parent)
            {
                throw new NotImplementedException("Method not implemented");
                //return GetChildren(GetFeature(parent));
            }
        #endregion

        #region Connection-related Operations
            public void AddConnection(AtlasFeature parent, AtlasFeature child, AtlasConnectionType type)
            {
                AddConnection(new AtlasConnection(parent, child, type));
            }
            public void AddConnection(string parent, string child, AtlasConnectionType type)
            {
                AtlasFeature parentFeature = GetFeature(parent);
                AtlasFeature childFeature = GetFeature(child);

                AddConnection(new AtlasConnection(parentFeature, childFeature, type));
            }
            public void AddConnection(AtlasConnection con)
            {
                ConnectionNodesCheck(con.Parent, con.Child);

                con.Child.MultipleParentFlag++;
                connections.Add(new AtlasFeatureTuple(con.Parent, con.Child), con);
            }
            public void RemoveConnection(AtlasFeature parent, AtlasFeature child)
            {
                RemoveConnection(new AtlasConnection(parent, child, 0));
            }
            public void RemoveConnection(string parent, string child)
            {
                AtlasFeature parentFeature = GetFeature(parent);
                AtlasFeature childFeature = GetFeature(child);

                RemoveConnection(new AtlasConnection(parentFeature, childFeature, 0));
            }
            public void RemoveConnection(AtlasConnection con)
            {
                ConnectionNodesCheck(con.Parent, con.Child);

                con.Child.MultipleParentFlag--;
                connections.Remove(new AtlasFeatureTuple(con.Parent, con.Child));
            }
            public void ClearConnections()
            {
                connections.Clear();
            }
            private void ConnectionNodesCheck(AtlasFeature parent, AtlasFeature child)
            {
                if (!features.ContainsKey(parent.Name)) throw new ArgumentException("Parent feature does not exist");
                if (!features.ContainsKey(child.Name)) throw new ArgumentException("Child feature does not exist");
            }
        #endregion

        #region Cardinality-related Operations
            public void SetCardinality(AtlasFeature feat, int min, int max)
            {
                if (!features.ContainsKey(feat.Name)) throw new ArgumentException("Feature does not exist");

                feat.Minimum = min;
                feat.Maximum = max;
            }
            public void SetCardinality(string feat, int min, int max)
            {
                SetCardinality(GetFeature(feat), min, max);
            }
            public void SetCardinalityMax(AtlasFeature feat, int max)
            {
                if (!features.ContainsKey(feat.Name)) throw new ArgumentException("Feature does not exist");

                feat.Maximum = max;
            }
            public void SetCardinalityMax(string feat, int max)
            {
                SetCardinalityMax(GetFeature(feat), max);
            }
            public void SetCardinalityMin(AtlasFeature feat, int min)
            {
                if (!features.ContainsKey(feat.Name)) throw new ArgumentException("Feature does not exist");

                feat.Minimum = min;
            }
            public void SetCardinalityMin(string feat, int min)
            {
                SetCardinalityMin(GetFeature(feat), min);
            }
            public bool RemoveCardinality(AtlasFeature feat)
            {
                if (!features.ContainsKey(feat.Name)) 
                    return false;

                feat.Minimum = 0;
                feat.Maximum = int.MaxValue;
                return true;
            }
            public void ClearAllCardinality()
            {
                foreach (AtlasFeature feat in features.Values)
                {
                    RemoveCardinality(feat);
                }
            }
        #endregion

        #region Constraint-related Operations
            public bool AddConstraint(string input)
            {
                AtlasConstraintValidationQueue queue = new AtlasConstraintValidationQueue(input);

                return AddConstraint(queue);
            }
            internal bool AddConstraint(AtlasConstraintValidationQueue queue)
            {
                AtlasConstraint newConstraint = new AtlasConstraint();

                try
                {
                    ConstraintValidationStateOne(queue, newConstraint);
                    constraints.Add(newConstraint);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
            internal void ConstraintValidationStateOne(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                ConstraintValidationStateTwo(queue, constraint);
                if (queue.Peek() != null && queue.Peek().Equals("<=>")) //TODO Placeholder
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Iff));
                    ConstraintValidationStateOne(queue, constraint);
                }
            }
            internal void ConstraintValidationStateTwo(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                ConstraintValidationStateThree(queue, constraint);
                if (queue.Peek() != null && queue.Peek().Equals("⇒"))
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Implies));
                    ConstraintValidationStateTwo(queue, constraint);
                }
            }
            internal void ConstraintValidationStateThree(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                ConstraintValidationStateFour(queue, constraint);
                if (queue.Peek() != null && queue.Peek().Equals("∨"))
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Or));
                    ConstraintValidationStateThree(queue, constraint);
                }
            }
            internal void ConstraintValidationStateFour(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                ConstraintValidationStateFive(queue, constraint);
                if (queue.Peek() != null && queue.Peek().Equals("∧"))
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.And));
                    ConstraintValidationStateFour(queue, constraint);
                }
            }
            internal void ConstraintValidationStateFive(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                if (queue.Peek() != null && queue.Peek().Equals("¬"))
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Not));
                }
                ConstraintValidationStateSix(queue, constraint);
            }
            internal void ConstraintValidationStateSix(AtlasConstraintValidationQueue queue, AtlasConstraint constraint)
            {
                if (queue.Peek() != null && queue.Peek().Equals("("))
                {
                    queue.Pop();
                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.LeftBracket));
                    ConstraintValidationStateOne(queue, constraint);
                    if (queue.Pop().Equals(")"))
                    {
                        constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.RightBracket));
                    }
                    else
                    {
                        throw new ArgumentException("Brackets were not properly closed");
                    }
                }
                else
                {
                    constraint.Constraints.Add(GetFeature(queue.Pop()));
                }
            }
            public void ClearConstraints()
            {
                constraints.Clear();
            }
        #endregion

        #region Serialization and Display
            public XmlSchema GetSchema()
            {
                return null;
            }
            public void ReadXml(XmlReader reader)
            {
                XmlSerializer featureSerializer = new XmlSerializer(typeof(AtlasFeature));

                //Reading Features
                reader.ReadToFollowing("Features");
                XmlReader featureReader = reader.ReadSubtree();
                while (featureReader.ReadToFollowing("AtlasFeature"))
                {
                    AddFeature((AtlasFeature)featureSerializer.Deserialize(featureReader));
                }
                featureReader.Close();
                
                reader.ReadToFollowing("RootFeature");
                string rootName = reader.ReadElementContentAsString();
                rootFeature = GetFeature(rootName);

                //Reading Connections
                if(!reader.Name.Equals("Connections"))
                {
                    reader.ReadToFollowing("Connections");
                }
                XmlReader connectionReader = reader.ReadSubtree();
                while (connectionReader.ReadToFollowing("AtlasConnection"))
                {
                    AtlasFeature child = GetFeature(reader["Child"]);
                    AtlasFeature parent = GetFeature(reader["Parent"]);
                    AtlasConnectionType type = (AtlasConnectionType)int.Parse(reader["Type"]);
                    AddConnection(parent, child, type);
                }
                connectionReader.Close();

                //Reading Constraints
                if (!reader.Name.Equals("Constraints"))
                {
                    reader.ReadToFollowing("Constraints");
                }
                XmlReader constraintReader = reader.ReadSubtree();
                while (constraintReader.ReadToFollowing("AtlasConstraint"))
                {
                    AtlasConstraintValidationQueue queue = new AtlasConstraintValidationQueue(constraintReader["Content"]);
                    queue.Normalize();
                    AddConstraint(queue);
                }
                constraintReader.Close();

                reader.Close();
            }
            public AtlasConstraint ReadXmlConstraint(XmlReader reader)
            {
                //TODO Revamp
                throw new NotImplementedException("Method not implemented");

                //    AtlasConstraint constraint = new AtlasConstraint();

                //    while (reader.ReadToFollowing("Constraint_Description"))
                //    {
                //        if (reader.Name.Equals("Constraint_Feature"))
                //        {
                //            constraint.Constraints.Add(GetFeature(reader.Value));
                //        }
                //        else
                //        {
                //            switch (reader.Value)
                //            {
                //                case "And":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.And));
                //                    break;

                //                case "Or":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Or));
                //                    break;

                //                case "Implies":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Implies));
                //                    break;

                //                case "Iff":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Iff));
                //                    break;

                //                case "LeftBracket":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.LeftBracket));
                //                    break;

                //                case "RightBracket":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.RightBracket));
                //                    break;

                //                case "Not":
                //                    constraint.Constraints.Add(new AtlasConstraintOperator(AtlasLogicalOperator.Not));
                //                    break;
                //            }
                //        }
                //    }

                //    constraint.Description = reader.Value;

                //    return constraint;
            }
            public void WriteXml(XmlWriter writer)
            {
                XmlSerializer featureSerializer = new XmlSerializer(typeof(AtlasFeature));
                XmlSerializer connectionSerializar = new XmlSerializer(typeof(AtlasConnection));
                XmlSerializer constraintSerializar = new XmlSerializer(typeof(AtlasConstraint));

                writer.WriteStartElement("Features");
                foreach (AtlasFeature feature in features.Values)
                {
                    featureSerializer.Serialize(writer, feature);
                }
                writer.WriteEndElement();

                writer.WriteElementString("RootFeature", rootFeature.Name);

                writer.WriteStartElement("Connections");
                foreach (AtlasConnection connection in connections.Values)
                {
                    connectionSerializar.Serialize(writer, connection);
                }
                writer.WriteEndElement();

                writer.WriteStartElement("Constraints");
                foreach (AtlasConstraint constraint in constraints)
                {
                    constraintSerializar.Serialize(writer, constraint);
                }
                writer.WriteEndElement();
            }
            public override string ToString()
            {
                return rootFeature.ToString();
            }
        #endregion
    }
}