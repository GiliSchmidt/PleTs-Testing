using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ShapeConnectors
{
    public class QuantityCircle : UcmlObject
    {
        public Connection StartLine { get; set; }
        public int IdObjectStarLine { get; set; }
        public bool UsedAtDestination { get; set; }
        public Loop LoopReference { get; set; }
        public double currentPercentage { get; set; }
        public String iterationProperty { get; set; }

        public QuantityCircle()
            : base()
        {
            iterationProperty = "Percentage";
            StartLine = new Connection();
            UsedAtDestination = false;
            tempUsers = new Dictionary<string, int>();
        }

        public QuantityCircle Clone(UcmlObject obj)
        {
            var newObj = new QuantityCircle();
            var aux = (QuantityCircle)obj;

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
            newObj.StartLine = aux.StartLine;
            newObj.IsSelected = aux.IsSelected;
            newObj.IdObjectStarLine = aux.IdObjectStarLine;
            newObj.isAbleUserSelection = aux.isAbleUserSelection;
            newObj.UsedAtDestination = aux.UsedAtDestination;

            return newObj;
        }

        public void ChangeColor(SolidColorBrush color, UcmlObject selectedObj)
        {
            UcmlObject selectedObjUCML;
            Border border;
            TextBlock percentage;

            if (selectedObj == null)
            {
                selectedObjUCML = this;
            }
            else
            {
                selectedObjUCML = selectedObj;
            }

            border = (Border)selectedObjUCML.Template.FindName("circle", selectedObjUCML);
            border.BorderBrush = color;
            percentage = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
            percentage.Foreground = color;

            System.Windows.Shapes.Path ph = new System.Windows.Shapes.Path();
            PathFigure pf = new PathFigure();
            PathGeometry pg = new PathGeometry();
            border = (Border)selectedObjUCML.Template.FindName("circle", selectedObjUCML);
            border.BorderBrush = color;
            percentage = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
            percentage.Foreground = color;

            QuantityCircle lqc = selectedObjUCML as QuantityCircle;
            lqc.myColor = color;
            lqc.LoopReference.bezierCurve.Stroke = color;
            lqc.LoopReference.myColor = color;
            lqc.LoopReference.myArrow.Stroke = color;
            lqc.LoopReference.myArrow.Fill = color;
            lqc.LoopReference.myColor = color;
        }

        

        public void ExportToXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("quantityCircle");
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
            writer.WriteAttributeString("iterationProperty", this.iterationProperty.ToString());

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
                writer.WriteAttributeString("percentage", myUsers[user].ToString());
                writer.WriteAttributeString("isAbleUserSelection", obj.isAbleUserSelection.ToString());
                writer.WriteAttributeString("isSelected", obj.IsSelected.ToString());
                writer.WriteEndElement();
            }
            writer.WriteElementString("connectStart", IdObjectStarLine.ToString());
            writer.WriteEndElement();
        }

        public QuantityCircle ImportToXml(XmlNode xn)
        {
            var attributes = xn.Attributes;
            this.Id = Convert.ToInt32(attributes["id"].Value);
            this.UcmlName = attributes["ucmlName"].Value;
            this.iterationProperty = attributes["iterationProperty"].Value;
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
                tempUsers.Add(((XmlNode)user).Attributes["description"].Value,
                    Convert.ToInt32(((XmlNode)user).Attributes["percentage"].Value));
            }
            this.IdObjectStarLine = Convert.ToInt32(xn.SelectSingleNode("connectStart").InnerText);

            return this;
        }
    }
}
