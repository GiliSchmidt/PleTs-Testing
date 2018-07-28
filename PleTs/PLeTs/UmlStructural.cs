using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Diagnostics;
using Plets;
using Plets.StructuraTestData;

namespace UmlStructural
{
    public class UmlStructural : IParser
    {
        private object parsedStructure = null;

        public string InputFile { get; set; }

        public bool TryParse()
        {
            StructuralTestData testData = new StructuralTestData();

            XmlNamespaceManager ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("xmi", "http://schema.omg.org/spec/XMI/1.3");
            ns.AddNamespace("UML", "org.omg.xmi.namespace.UML");

            XmlDocument doc = new XmlDocument();
            doc.Load(InputFile);
            
            String  stereotypeId   = null;
            List<XmlNode> classifierNodes = new List<XmlNode>();

            //1st, Locate SATestData stereotype xmi.id attribute
            foreach (XmlNode classNode in doc.SelectNodes("//UML:Stereotype", ns))
            {   
                try{
                    if(classNode.Attributes["name"].Value == "SATestData"){
                        stereotypeId = classNode.Attributes["xmi.id"].Value;
                        break;
                    }
                }catch(Exception){}                
            }

            //2nd, Find stereotyped classifier role (with SATestData stereotype)
            XmlNodeList classifiers = doc.SelectNodes("//UML:ClassifierRole", ns);
            foreach (XmlNode classifier in classifiers){
                try{
                    //Locate stereotype inside classifier
                    foreach (XmlNode ste in classifier.SelectNodes(".//UML:Stereotype[@xmi.idref='" + stereotypeId + "']", ns))
                        classifierNodes.Add(classifier);
                }catch(Exception){}
            }

            //3rd, Locate messages linked to found CR
            //List<XmlNode> messageNodes = new List<XmlNode>();
            XmlNodeList messages = doc.SelectNodes("//UML:Message", ns);
            List<KeyValuePair<string, XmlNode>> messageNodes = new List<KeyValuePair<string, XmlNode>>();

            foreach (XmlNode classifierNode in classifierNodes)
            {
                string roleid = classifierNode.Attributes["xmi.id"].Value;
                foreach (XmlNode message in messages)
                {

                    //checks if meesage is send to required classifier role
                    try
                    {
                        
                        foreach (XmlNode n in message.SelectNodes(".//UML:Message.receiver/UML:ClassifierRole[@xmi.idref='" + roleid + "']", ns))
                        {
                            
                            messageNodes.Add(
                                new KeyValuePair<string, XmlNode>(
                                    classifierNode.Attributes["name"].Value,
                                    message));
                        }
                    }
                    catch (Exception) { }
                }

                UmlClass c = new UmlClass();
                c.Name = classifierNode.Attributes["name"].Value;

                //4th, parse information from message names
                foreach (KeyValuePair<string,XmlNode> n in messageNodes)
                {
                    foreach (XmlNode a in n.Value.SelectNodes(".//UML:Message.action/UML:CallAction", ns))
                    {
                        string actionId = a.Attributes["xmi.idref"].Value;
                        foreach (XmlNode action in doc.SelectNodes("//UML:CallAction[@xmi.id='" + actionId + "']", ns))
                        {

                            string assignature = action.Attributes["name"].Value;

                            UmlMethod m = new UmlMethod();
                            m.Id = Convert.ToInt32(assignature.Split(':')[0]);
                            m.Name = assignature.Split(':')[1].Split('(')[0].Split(' ')[1];
                            m.Return = assignature.Split(':')[1].Split('(')[0].Split(' ')[0];

                            int id = 0;
                            foreach (string s in assignature.Split('(')[1].Replace(")", "").Split(','))
                            {
                                UmlMethodParam par = new UmlMethodParam();
                                par.Id = ++id;
                                par.Type = s.Trim().Split(' ')[0];
                                par.Name = s.Trim().Split(' ')[1];
                                m.Params.Add(par);
                            }

                            if (c.Name == n.Key)
                            {
                                c.Methods.Add(m);
                            }
                        }
                    }
                }
                testData.Classes.Add(c);
            }

            //5th, collect stereotypes and their tags
            XmlNodeList stereotypes = doc.SelectNodes("//UML:Stereotype[@xmi.id='" + stereotypeId + "']", ns);
            XmlNode stereotype = null;

            foreach(XmlNode s in stereotypes){
                stereotype = s;
                break;
            }

            //XmlNode firstClassifierNode = classifierNodes.FirstOrDefault();

            foreach (XmlNode xm in classifierNodes)
            {
                foreach (XmlNode t in stereotype.SelectNodes(".//UML:TagDefinition", ns))
                {
                    string tagId = t.Attributes["xmi.id"].Value;

                    foreach (XmlNode tv in xm.SelectNodes(".//UML:TaggedValue", ns))
                    {
                        foreach (XmlNode tvt in tv.SelectNodes(".//UML:TagDefinition[@xmi.idref='" + tagId + "']", ns))
                        {
                            foreach (XmlNode tdv in tv.SelectNodes(".//UML:TaggedValue.dataValue", ns))
                            {

                                testData.Tags.Add(new UmlTag()
                                {
                                    Name = t.Attributes["name"].Value,
                                    Value = tdv.InnerXml
                                });
                                break;
                            }
                            break;
                        }
                    }
                }
            }

            this.parsedStructure = testData;
            return true;
        }

        public void SetLogger(Plets.Logger logger)
        {
            this.Logger = logger;
        }

        public object GetParsedStructure()
        {
            return this.parsedStructure;
        }

        public bool ValidateInput()
        {
            return true;
        }

        public void SetContainer(System.Windows.Controls.Panel container)
        {
            StructuralParser parser = new StructuralParser(this);
            container.Children.Add(parser);
        }

        public Plets.Logger Logger { get; set; }
    }
}
 