using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace ShapeConnectors
{
    class ExportXml
    {
        private static List<QuantityCircle> listQuantityCircle = new List<QuantityCircle>();
        private static List<ExitPath> listExitPath = new List<ExitPath>();
        private static List<Branch> listBranch = new List<Branch>();
        private static List<Merge> listMerge = new List<Merge>();
        private static List<Loop> listLoop = new List<Loop>();
        private static List<Condition> listCondition = new List<Condition>();
        private static List<DescriptionLineActivity> listDescriptionLineActivity = new List<DescriptionLineActivity>();
        private static List<DescriptionLineUser> listDescriptionLineUser = new List<DescriptionLineUser>();
        private static List<OptionBox> listOptionBox = new List<OptionBox>();
        private static List<SyncPoint> listSyncPoint = new List<SyncPoint>();
        private static List<Connection> listConnectionLine = new List<Connection>();

        public static void ExportDiagramToXml(Canvas canvas, List<Loop> loop, List<Connection> connectionLineList , String name, int globalId)
        {
            listConnectionLine = connectionLineList;
            var myCanvas = canvas;
            var fileName = name;
            listLoop = loop;

            XmlTextWriter writer = new XmlTextWriter(fileName.Replace(".xml", "") + ".xml", null);
            writer.WriteStartDocument();
            writer.Formatting = Formatting.Indented;
            writer.WriteStartElement("diagram");
            writer.WriteAttributeString("globalId", ""+globalId);
            writer.WriteAttributeString("Canvas_width", "" + myCanvas.Width);
            writer.WriteAttributeString("Canvas_height", "" + myCanvas.Height);
            DiagramToXml(myCanvas, ref writer);

            writer.WriteFullEndElement();
            writer.Close();
            ClearLists();
        }

        private static void DiagramToXml(Canvas myCanvas, ref XmlTextWriter writer)
        {
            foreach (var child in myCanvas.Children)
            {
                if (child.GetType() != typeof(BezierCurveShape) && child.GetType() != typeof(ArrowShape) && child.GetType() != typeof(System.Windows.Shapes.Path))
                {
                    if (child.GetType() != typeof(Connection) && child.GetType() != typeof(System.Windows.Controls.Primitives.Thumb))
                    {
                        var ucmlObj = (UcmlObject)child;
                        SortByType(ucmlObj);
                    }
                }
            }

            LoadQuantityCircleXml(ref writer);
            LoadBranchXml(ref writer);
            LoadConditionXml(ref writer);
            LoadExitPathXml(ref writer);
            LoadOptionBoxXml(ref writer);
            LoadDescriptionLineActivityXml(ref writer);
            LoadDescriptionLineUserXml(ref writer);
            LoadSyncPointXml(ref writer);
            LoadMergeXml(ref writer);
            LoadLoopXml(listLoop, ref writer);
            LoadConnetionLineXml(ref writer);
        }

        private static void LoadConnetionLineXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("connectionLines");
            foreach (var item in listConnectionLine)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadQuantityCircleXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("quantityCircles");
            foreach (var item in listQuantityCircle)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadLoopXml(List<Loop> loops, ref XmlTextWriter writer)
        {
            writer.WriteStartElement("loops");
            foreach (var item in loops)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadSyncPointXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("syncPoints");
            foreach (var item in listSyncPoint)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadExitPathXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("exitPaths");
            foreach (var item in listExitPath)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadBranchXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("branchs");
            foreach (var item in listBranch)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadMergeXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("merges");
            foreach (var item in listMerge)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadConditionXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("conditions");
            foreach (var item in listCondition)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadDescriptionLineActivityXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("descriptionLineActivity");
            foreach (var item in listDescriptionLineActivity)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadDescriptionLineUserXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("descriptionLineUser");
            foreach (var item in listDescriptionLineUser)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadOptionBoxXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("optionBoxes");
            foreach (var item in listOptionBox)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }

        private static void LoadLoopXml(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("loops");
            foreach (var item in listLoop)
            {
                item.ExportToXml(ref writer);
            }
            writer.WriteEndElement();
        }
        private static void SortByType(UcmlObject ucmlObj)
        {
            switch (ucmlObj.TemplateName)
            {
                case EnumTemplates.TempBranch:
                    listBranch.Add(ucmlObj as Branch);
                    break;
                case EnumTemplates.TempCondition:
                    listCondition.Add(ucmlObj as Condition);
                    break;
                case EnumTemplates.TempDescriptionLineActivity:
                    listDescriptionLineActivity.Add(ucmlObj as DescriptionLineActivity);
                    break;
                case EnumTemplates.TempDescriptionLineUser:
                    listDescriptionLineUser.Add(ucmlObj as DescriptionLineUser);
                    break;
                case EnumTemplates.TempExitPath:
                    listExitPath.Add(ucmlObj as ExitPath);
                    break;
                case EnumTemplates.TempMerge:
                    listMerge.Add(ucmlObj as Merge);
                    break;
                case EnumTemplates.TempOptionBox:
                    listOptionBox.Add(ucmlObj as OptionBox);
                    break;
                case EnumTemplates.TempQuantityCircle:
                    listQuantityCircle.Add(ucmlObj as QuantityCircle);
                    break;
                case EnumTemplates.TempSyncPoint:
                    listSyncPoint.Add(ucmlObj as SyncPoint);
                    break;
                case EnumTemplates.TempLoop:
                    listLoop.Add(ucmlObj as Loop);
                    break;
            }
        }
        private static void ClearLists()
        {
             listConnectionLine.Clear();
             listQuantityCircle.Clear();
             listQuantityCircle.Clear();
             listExitPath.Clear();
             listBranch.Clear();
             listMerge.Clear();
             listCondition.Clear();
             listDescriptionLineActivity.Clear();
             listDescriptionLineUser.Clear();
             listOptionBox.Clear();
             listSyncPoint.Clear();
        }

    }

}
