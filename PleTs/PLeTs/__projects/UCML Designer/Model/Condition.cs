﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;

namespace ShapeConnectors
{
    class Condition : UcmlObject
    {
        public Connection StartLineYes { get; set; }
        public Connection StartLineNo { get; set; }
        public Connection EndLine { get; set; }
        public int IdObjectStarLineYes { get; set; }
        public int IdObjectStarLineNo { get; set; }
        public int IdObjectEndLine { get; set; }

        public Condition()
            : base()
        {
            StartLineYes = new Connection();
            StartLineNo = new Connection();
            EndLine = new Connection();
        }

        public Condition Clone(UcmlObject obj)
        {
            var newObj = new Condition();
            var aux = (Condition)obj;

            newObj.Id = aux.Id;
            newObj.UcmlName = aux.UcmlName;
            newObj.Color = aux.Color;
            newObj.PosTopX = aux.PosTopX;
            newObj.PosTopY = aux.PosTopY;
            newObj.Description = aux.Description;
            newObj.TemplateName = aux.TemplateName;
            newObj.listNewProp = aux.listNewProp;
            newObj.UcmlWidth = aux.UcmlWidth;
            newObj.UcmlHeight = aux.UcmlHeight;
            newObj.myColor = aux.myColor;
            newObj.Percentage = aux.Percentage;
            newObj.StartLineYes = aux.StartLineYes;
            newObj.StartLineNo = aux.StartLineNo;
            newObj.EndLine = aux.EndLine;
            newObj.IdObjectStarLineYes = aux.IdObjectStarLineYes;
            newObj.IdObjectStarLineNo = aux.IdObjectStarLineNo;
            newObj.IdObjectEndLine = aux.IdObjectEndLine;
            newObj.isAbleUserSelection = aux.isAbleUserSelection;

            return newObj;
        }

        public void ExportToXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("condition");
            writer.WriteAttributeString("id", this.Id.ToString());
            writer.WriteAttributeString("ucmlName", this.UcmlName);
            writer.WriteAttributeString("posTopX", this.PosTopX.ToString());
            writer.WriteAttributeString("posTopY", this.PosTopY.ToString());
            writer.WriteAttributeString("description", this.Description);
            writer.WriteAttributeString("templateName", this.TemplateName);
            writer.WriteAttributeString("ucmlWidth", this.UcmlWidth.ToString());
            writer.WriteAttributeString("ucmlHeight", this.UcmlHeight.ToString());
            writer.WriteAttributeString("percentage", this.Percentage.ToString());
            writer.WriteAttributeString("isAbleUserSelection", this.isAbleUserSelection.ToString());
            writer.WriteAttributeString("isSelected", this.IsSelected.ToString());
            writer.WriteAttributeString("myColor", this.myColor.Color.ToString());

            foreach (var prop in listNewProp)
            {
                writer.WriteStartElement("property");
                writer.WriteAttributeString("name", prop.name);
                writer.WriteAttributeString("value", prop.value);
                writer.WriteEndElement();
            }
            foreach (var user in myUsers.Keys)
            {
                var obj = (UcmlObject)user;
                writer.WriteStartElement("user");
                writer.WriteAttributeString("id", obj.Id.ToString());
                writer.WriteAttributeString("myColor", obj.myColor.Color.ToString());
                writer.WriteAttributeString("description", obj.Description);
                writer.WriteAttributeString("percentage", obj.Percentage.ToString());
                writer.WriteAttributeString("isAbleUserSelection", obj.isAbleUserSelection.ToString());
                writer.WriteAttributeString("isSelected", obj.IsSelected.ToString());
                writer.WriteEndElement();
            }
            writer.WriteElementString("connectstartYes", this.IdObjectStarLineYes.ToString());
            writer.WriteElementString("connectstartNo", this.IdObjectStarLineNo.ToString());
            writer.WriteEndElement();
        }

        public Condition ImportToXml(XmlNode xn)
        {
            var attributes = xn.Attributes;
            this.Id = Convert.ToInt32(attributes["id"].Value);
            this.UcmlName = attributes["ucmlName"].Value;
            this.PosTopX = Convert.ToDouble(attributes["posTopX"].Value);
            this.PosTopY = Convert.ToDouble(attributes["posTopY"].Value);
            this.Description = attributes["description"].Value;
            this.TemplateName = attributes["templateName"].Value;
            this.UcmlWidth = Convert.ToDouble(attributes["ucmlWidth"].Value);
            this.UcmlHeight = Convert.ToDouble(attributes["ucmlHeight"].Value);
            this.Percentage = Convert.ToDouble(attributes["percentage"].Value);
            this.isAbleUserSelection = Convert.ToBoolean(attributes["isAbleUserSelection"].Value);
            this.IsSelected = Convert.ToBoolean(attributes["isSelected"].Value);
            this.myColor = (SolidColorBrush)(new BrushConverter().ConvertFrom(attributes["myColor"].Value));

            foreach (var prop in xn.SelectNodes("property"))
            {
                this.listNewProp.Add(new AdditionalProperty { name = ((XmlNode)prop).Attributes["name"].Value, value = ((XmlNode)prop).Attributes["value"].Value });
            }
            foreach (var user in xn.SelectNodes("user"))
            {
                /*this.myUsers.Add(new QuantityCircle
                {
                    Id = Convert.ToInt32(((XmlNode)user).Attributes["id"].Value),
                    myColor = (SolidColorBrush)(new BrushConverter().ConvertFrom(((XmlNode)user).Attributes["myColor"].Value)),
                    Description = ((XmlNode)user).Attributes["description"].Value,
                    Percentage = Convert.ToDouble(((XmlNode)user).Attributes["percentage"].Value),
                    isAbleUserSelection = Convert.ToBoolean(((XmlNode)user).Attributes["isAbleUserSelection"].Value),
                    IsSelected = Convert.ToBoolean(((XmlNode)user).Attributes["isSelected"].Value)
                });*/
            }
            this.IdObjectStarLineYes = Convert.ToInt32(xn.SelectSingleNode("connectstartYes").InnerText);
            this.IdObjectStarLineNo = Convert.ToInt32(xn.SelectSingleNode("connectstartNo").InnerText);

            return this;
        }
    }
}
