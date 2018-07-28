using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Media;
using System.Windows;

namespace ShapeConnectors
{
    public class Loop : UcmlObject
    {
        public UcmlObject ObjectStart { get; set; }
        public UcmlObject ObjectEnd { get; set; }
        public double currentPercentage { get; set; }
        public BezierCurveShape bezierCurve { get; set; }
        public QuantityCircle myQuantityCircle { get; set; }
        public int idMyQuantityCircle { get; set; }
        public ArrowShape myArrow { get; set; }

        #region  Temp variables for Import methodd

        public int IdObjectStart { get; set; }
        public int IdObjectEnd { get; set; }
        public Point control1;
        public Point control2;

        #endregion
        

        public Loop()
            : base() { }

        public Loop(Point start, Point control1, Point control2, Point end, SolidColorBrush color)
        {
            if (color == null)
            {
                color = Brushes.Black;
            }

            this.bezierCurve = new BezierCurveShape(start, control1, control2, end);
            myColor = color;
            this.bezierCurve.Stroke = color;
            this.bezierCurve.StrokeDashArray.Add(6);
            this.bezierCurve.StrokeThickness = 1;
        }

        public Loop(Point p)
        {
            this.bezierCurve = new BezierCurveShape(p, p, p, p);
            this.bezierCurve.Stroke = Brushes.Black;
            this.bezierCurve.StrokeDashArray.Add(6);
            this.bezierCurve.StrokeThickness = 1;
        }

        public BezierCurveShape UpdateCurveEnd(Point end)
        {
            BezierCurveShape b = new BezierCurveShape(bezierCurve.start, bezierCurve.control1, bezierCurve.control2, end);
            this.bezierCurve.Stroke = Brushes.Black;
            this.bezierCurve.StrokeDashArray.Add(6);
            this.bezierCurve.StrokeThickness = 1;
            bezierCurve = b;
            return b;
        }

        public BezierCurveShape UpdateCurveBegin(Point begin)
        {
            BezierCurveShape b = new BezierCurveShape(begin, bezierCurve.control1, bezierCurve.control2, bezierCurve.end);
            this.bezierCurve.Stroke = Brushes.Black;
            this.bezierCurve.StrokeDashArray.Add(6);
            this.bezierCurve.StrokeThickness = 1;
            bezierCurve = b;
            return b;
        }

        public Loop Clone(UcmlObject obj)
        {
            var newObj = new Loop();
            var aux = (Loop)obj;

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
            newObj.Percentage = 0;

            return newObj;
        }

        public void ExportToXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("loop");
            writer.WriteAttributeString("idObjectStart", this.ObjectStart.Id.ToString());
            writer.WriteAttributeString("idObjectEnd", this.ObjectEnd.Id.ToString());
            writer.WriteAttributeString("control1_X", bezierCurve.control1.X.ToString());
            writer.WriteAttributeString("control1_Y", bezierCurve.control1.Y.ToString());
            writer.WriteAttributeString("control2_X", bezierCurve.control2.X.ToString());
            writer.WriteAttributeString("control2_Y", bezierCurve.control2.Y.ToString());
            writer.WriteAttributeString("id_quantityCircle", myQuantityCircle.Id.ToString());
            writer.WriteAttributeString("percentage", this.Percentage.ToString());
            writer.WriteAttributeString("color", myColor.ToString());
            writer.WriteEndElement();
        }

        public Loop ImportToXml(XmlNode xn)
        {
            var attributes = xn.Attributes;
            this.Percentage = Convert.ToDouble(attributes["percentage"].Value);
            this.IdObjectStart = Convert.ToInt32(attributes["idObjectStart"].Value);
            this.IdObjectEnd = Convert.ToInt32(attributes["idObjectEnd"].Value);
            this.idMyQuantityCircle = Convert.ToInt32(attributes["id_quantityCircle"].Value);
            var converter = new System.Windows.Media.BrushConverter();
            this.myColor = (SolidColorBrush)converter.ConvertFromString(attributes["color"].Value);
           
            this.control1 = new Point(Convert.ToDouble(attributes["control1_X"].Value), Convert.ToDouble(attributes["control1_Y"].Value));
            this.control2 = new Point(Convert.ToDouble(attributes["control2_X"].Value), Convert.ToDouble(attributes["control2_Y"].Value));

            return this;
        }
    }
}
