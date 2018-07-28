using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace ShapeConnectors
{
    public class UcmlObject : Thumb
    {
        public int Id { get; set; }
        public string UcmlName { get; set; }
        public string Color { get; set; }
        public double PosTopX { get; set; }
        public double PosTopY { get; set; }
        public double PosBottonX { get; set; }
        public double PosBottonY { get; set; }
        public string Description { get; set; }
        public string TemplateName { get; set; }
        public List<AdditionalProperty> listNewProp { get; set; }
        public double UcmlWidth { get; set; }
        public double UcmlHeight { get; set; }
        public double Percentage { get; set; }
        public SolidColorBrush myColor { get; set; }
        public Dictionary<object, int> myUsers { get; set; }
        public bool isAbleUserSelection { get; set; }
        public bool IsSelected { get; set; }
        public double UsedPercentage { get; set; } // use only for validate diagram
        public bool NeedCheck { get; set; }
        public Dictionary<string, int> tempUsers { get; set; }

        #region "Used for LOOP"
        // Specific properties for LOOP
        public Polygon myPolygon { get; set; }
        public Border myCircle { get; set; }
        public TextBlock myPercent { get; set; }
        public Loop LoopEnd;
        public Loop LoopStart;

        #endregion

        public UcmlObject()
            : base()
        {
            listNewProp = new List<AdditionalProperty>();
            myColor = Brushes.Black;
            myUsers = new Dictionary<object, int>();
            isAbleUserSelection = false;
            IsSelected = false;
            Percentage = 0;
            NeedCheck = false;
            myPolygon = new Polygon();
            myCircle = new Border();
            myPercent = new TextBlock();
            Description = "";
            TemplateName = "";

        }
    }
}