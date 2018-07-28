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
    public class ArrowShape : System.Windows.Shapes.Shape
    {
        public PathGeometry arrow;
        public Point position;
        public SolidColorBrush myColor;


        public ArrowShape(Point p, SolidColorBrush color)
        {
            myColor = color;
            position = p;
            this.Stroke = color;
            this.Fill = color;
            this.CreateGeometry(p);
        }

        public void ChangeColor(SolidColorBrush color)
        {
            myColor = color;
        }

        private void CreateGeometry(Point p)
        {

            Polygon polygon = new Polygon();
            Point p1 = new Point(p.X, p.Y);
            Point p2 = new Point(p.X + 4, p.Y + 10);
            Point p3 = new Point(p.X + 10, p.Y + 4);
            polygon.Stroke = myColor;

            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = p1;

            LineSegment myLineSegment1 = new LineSegment();
            myLineSegment1.Point = p2;

            LineSegment myLineSegment2 = new LineSegment();
            myLineSegment2.Point = p3;

            LineSegment myLineSegment3 = new LineSegment();
            myLineSegment3.Point = p1;

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment1);
            myPathSegmentCollection.Add(myLineSegment2);
            myPathSegmentCollection.Add(myLineSegment3);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            arrow = myPathGeometry;

        }

        protected override Geometry DefiningGeometry
        {
            get { return arrow; }
        }


    }
}
