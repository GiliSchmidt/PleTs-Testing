using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml;

namespace ShapeConnectors
{
    public class Connection : System.Windows.Shapes.Shape
    {
        public Dictionary<String, int> tempUsers = new Dictionary<string, int>();
        public Dictionary<object, int> myUsers { get; set; }
        public UcmlObject destinyElement { get; set; }
        public UcmlObject originElement { get; set; }
        public LineGeometry line { get; set; }
        public int elementoOrigId { get; set; }
        public int elementoDestId { get; set; }
        public Point initialPoint { get; set; }
        public Point endPoint { get; set; }

        public Connection(){}

        public Connection(Point initialPoint, Point endPoint, SolidColorBrush color)
        {
            this.myUsers = new Dictionary<object, int>();
            this.initialPoint = initialPoint;
            this.endPoint = endPoint;

            if (color == null)
            {
                this.Stroke = Brushes.Black;
            }
            else
            {
                this.Stroke = color;
            }

            line = new LineGeometry(initialPoint, endPoint);
            this.StrokeThickness = 2.5;
        }

        public Connection(Point initialPoint)
        {
            this.initialPoint = initialPoint;
            this.Stroke = Brushes.Black;
            this.StrokeThickness = 2.5;
        }

        protected override Geometry DefiningGeometry
        {
            get { return line; }
        }

        public void SetStartPointLine(Point initialPoint)
        {
            line = new LineGeometry(initialPoint, this.endPoint);
            this.initialPoint = initialPoint; 
        }

        public void SetEndPointLine(Point endPoint)
        {
            line = new LineGeometry(this.initialPoint, endPoint);
            this.endPoint = endPoint;
        }

        public void SetEndPointLine(Point endPoint, SolidColorBrush color)
        {
            line = new LineGeometry(this.initialPoint, endPoint);
            this.endPoint = endPoint;
        }
        
        public void ExportToXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("ConnectionLineShape");
            writer.WriteAttributeString("elementoOrigId", this.elementoOrigId.ToString());
            writer.WriteAttributeString("elementoDestId", this.elementoDestId.ToString());
            writer.WriteAttributeString("myColor", this.Stroke.ToString());
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
            writer.WriteEndElement();
        }
        public Connection ImportToXml(XmlNode xn)
        {
            var attributes = xn.Attributes;
            this.elementoOrigId = Convert.ToInt32(attributes["elementoOrigId"].Value);
            this.elementoDestId = Convert.ToInt32(attributes["elementoDestId"].Value);
            this.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(attributes["myColor"].Value));

            foreach (var user in xn.SelectNodes("user"))
            {
                tempUsers.Add(((XmlNode)user).Attributes["description"].Value,
                    Convert.ToInt32(((XmlNode)user).Attributes["percentage"].Value));
            }

            return this;
        }

    }
}
