using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Xml;

namespace ShapeConnectors
{
    public class ConnectionLine : UcmlObject
    {
        public System.Windows.Shapes.Path path { get; set; }
        public int elementoOrigId { get; set; }
        public int elementoDestId { get; set; }

        public ConnectionLine(LineGeometry line)
        {
            path = new System.Windows.Shapes.Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 2.5;
            path.Data = line;
        }
        public ConnectionLine()
        {
            path = new System.Windows.Shapes.Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 2.5;
        }
        public void SetStartPointLine(Point startPoint)
        {
            LineGeometry lg = path.Data as LineGeometry;
            lg.StartPoint = startPoint;
        }
        public void SetEndPointLine(Point endPoint)
        {

            LineGeometry lg = path.Data as LineGeometry;
            lg.EndPoint = endPoint;
        }
        public void setColor(Brush color)
        {
            path.Stroke = color;
        }
        public void ExportToXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("ConnectionLine");
            writer.WriteAttributeString("elementoOrigId", this.elementoOrigId.ToString());
            writer.WriteAttributeString("elementoDestId", this.elementoDestId.ToString());
            writer.WriteAttributeString("myColor", this.path.Stroke.ToString());
            writer.WriteEndElement();
        }
        public ConnectionLine ImportToXml(XmlNode xn)
        {
            var attributes = xn.Attributes;
            this.elementoOrigId = Convert.ToInt32(attributes["elementoOrigId"].Value);
            this.elementoDestId = Convert.ToInt32(attributes["elementoDestId"].Value);
            this.path.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(attributes["myColor"].Value));
            return this;
        }

    }
}
