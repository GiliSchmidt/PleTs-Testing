using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShapeConnectors
{
    public class BezierCurveShape : System.Windows.Shapes.Shape
    {
        public PathGeometry bezierCurve;
        public Point start;
        public Point end;
        public Point control1;
        public Point control2;
        public Point maxPoint;

        public BezierCurveShape(Point start, Point control1, Point control2, Point end)
        {
            this.start = start;
            this.end = end;
            this.control1 = control1;
            this.control2 = control2;

            this.CreateGeometry();
        }

        private void CreateGeometry()
        {
            Point[] bezierPoints =
             {
                 start, control1, control2, end
             };

            //Generate bezier points
            var b = GetBezierApproximation(bezierPoints, 256);
            //Calcule TopPosition
            CalculateTopCurvePoint(b);

            PathFigure pf = new PathFigure(b.Points[0], new[] { b }, false);
            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);
            var pge = new PathGeometry();
            pge.Figures = pfc;
            bezierCurve = new PathGeometry();
            bezierCurve = pge;
        }

        private Point CalculateTopCurvePoint(PolyLineSegment segment)
        {
            //Search for necessary position(height) 
            //If control points coordenades as more little then initial and end points
            if (control1.Y < start.Y && control2.Y < end.Y)
            {
                maxPoint = new Point(500, 500);
                for (int i = 0; i < segment.Points.Count; i++)
                {
                    Point point = segment.Points[i];
                    if (point.Y < maxPoint.Y)
                    {
                        maxPoint = point;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                maxPoint = new Point(0, 0);
                for (int i = 0; i < segment.Points.Count; i++)
                {
                    Point point = segment.Points[i];
                    if (point.Y > maxPoint.Y)
                    {
                        maxPoint = point;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return maxPoint;
        }

        private PolyLineSegment GetBezierApproximation(Point[] controlPoints, int outputSegmentCount)
        {
            Point[] points = new Point[outputSegmentCount + 1];
            for (int i = 0; i <= outputSegmentCount; i++)
            {
                double t = (double)i / outputSegmentCount;
                points[i] = GetBezierPoint(t, controlPoints, 0, controlPoints.Length);
            }
            return new PolyLineSegment(points, true);
        }

        private Point GetBezierPoint(double t, Point[] controlPoints, int index, int count)
        {
            if (count == 1)
                return controlPoints[index];
            var P0 = GetBezierPoint(t, controlPoints, index, count - 1);
            var P1 = GetBezierPoint(t, controlPoints, index + 1, count - 1);
            return new Point((1 - t) * P0.X + t * P1.X, (1 - t) * P0.Y + t * P1.Y);
        }

        protected override Geometry DefiningGeometry
        {
            get { return bezierCurve; }
        }

       
    }
}
