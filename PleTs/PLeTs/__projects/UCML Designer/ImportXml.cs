using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeConnectors
{
    public class ImportXml
    {
        private static int[] list;
        public static int globalId { get; set; }
        public static List<Connection> connectionLineList;
        private static List<UcmlObject> listUsers;
        private static List<DescriptionLineUser> listDescriptionUser;
        public static double canvasWidth;
        public static double canvasHeight;

        public static void ImportDiagram(ref Canvas canvas, String fileName, ref int[] listIdByType, ref List<Loop> loop, ref List<UcmlObject> users, ControlTemplate[] listTemplate)
        {
            listUsers = users;
            globalId = 0;
            list = listIdByType;
            XmlDocument xml = new XmlDocument();
            string text = System.IO.File.ReadAllText(fileName);
            xml.LoadXml(text.Trim());
            XmlElement elem = xml.DocumentElement;
            globalId = Int32.Parse(elem.Attributes[globalId].Value);
            canvasHeight = Double.Parse(elem.Attributes["Canvas_height"].Value);
            canvasWidth = Double.Parse(elem.Attributes["Canvas_width"].Value);

            var listDescriptionLineUser = xml.SelectNodes("/diagram/descriptionLineUser/descriptionLineUser");
            ImportDescriptionLineUser(ref canvas, listDescriptionLineUser, listTemplate[9]);

            var listQuantityCircle = xml.SelectNodes("/diagram/quantityCircles/quantityCircle");
            ImportQuantityCircles(ref canvas, listQuantityCircle, listTemplate[0]);

            var listDescriptionLineActivity = xml.SelectNodes("/diagram/descriptionLineActivity/descriptionLineActivity");
            ImportDescriptionLineActivity(ref canvas, listDescriptionLineActivity, listTemplate[1]);

            var listSyncPoint = xml.SelectNodes("/diagram/syncPoints/syncPoint");
            ImportSyncPoint(ref canvas, listSyncPoint, listTemplate[2]);

            var listOptionBox = xml.SelectNodes("/diagram/optionBoxes/optionBox");
            ImportOptionBox(ref canvas, listOptionBox, listTemplate[3]);

            var listExitPath = xml.SelectNodes("/diagram/exitPaths/exitPath");
            ImportExitPath(ref canvas, listExitPath, listTemplate[5]);

            var listCondition = xml.SelectNodes("/diagram/conditions/condition");
            ImportCondition(ref canvas, listCondition, listTemplate[4]);

            var listMerge = xml.SelectNodes("/diagram/merges/merge");
            ImportMerge(ref canvas, listMerge, listTemplate[6]);

            var listBranch = xml.SelectNodes("/diagram/branchs/branch");
            ImportBranch(ref canvas, listBranch, listTemplate[7]);

            var listLoop = xml.SelectNodes("/diagram/loops/loop");
            ImportLoop(ref canvas, listLoop, ref loop);

            var listConnectionLine = xml.SelectNodes("/diagram/connectionLines/ConnectionLineShape");
            ImportConnections(ref canvas, listConnectionLine, canvas);
            //listConnections.Clear();
        }

        private static void ImportQuantityCircles(ref Canvas myCanvas, XmlNodeList listQuantityCircle, ControlTemplate template)
        {
            QuantityCircle ucmlObject;
            foreach (XmlNode xn in listQuantityCircle)
            {
                ucmlObject = new QuantityCircle().ImportToXml(xn);
                list[0]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var p = (TextBlock)ucmlObject.Template.FindName("Percentage", ucmlObject);
                p.Text = ucmlObject.Percentage.ToString()+"%";

                // Set value 
                if (ucmlObject.iterationProperty == "Iteration")
                {
                    p.Text = ucmlObject.Percentage.ToString();
                }
                else
                {
                    p.Text = ucmlObject.Percentage.ToString() + "%";
                }

                Border b = (Border)ucmlObject.Template.FindName("circle", ucmlObject);
                b.BorderBrush = ucmlObject.myColor;
                
                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                LoadMyUsers(ucmlObject, myCanvas);
           }
        }

        private static void ImportDescriptionLineActivity(ref Canvas myCanvas, XmlNodeList listDescriptionLine, ControlTemplate template)
        {
            DescriptionLineActivity ucmlObject;
            foreach (XmlNode xn in listDescriptionLine)
            {
                ucmlObject = new DescriptionLineActivity().ImportToXml(xn);
                list[1]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                TextBlock  d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString() + " (" + ucmlObject.Percentage + "%)";
                d.Width = Util.MeasureString(d.Text, d).Width;
                Polyline pl = (Polyline)ucmlObject.Template.FindName("Line", ucmlObject);
                TextBlock tb = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                pl.Stroke = ucmlObject.myColor;
                tb.Foreground = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();
                // Load my users
                LoadMyUsers(ucmlObject, myCanvas);
           }
        }

        private static void LoadMyUsers(UcmlObject ucmlObject, Canvas myCanvas)
        {
            foreach (var item in ucmlObject.tempUsers.Keys)
            {
                UcmlObject obj = GetObjectByName(myCanvas, item);
                if (obj != null)
                    ucmlObject.myUsers.Add(obj, ucmlObject.tempUsers[item]);
            }
        }

        private static void LoadMyUsers(Connection connection, Canvas myCanvas)
        {
            foreach (var item in connection.tempUsers.Keys)
            {
                UcmlObject obj = GetObjectByName(myCanvas, item);
                if (obj != null)
                    connection.myUsers.Add(obj, connection.tempUsers[item]);
            }
        }

        private static UcmlObject GetObjectByName(Canvas myCanvas, String name)
        {
            foreach (var item  in myCanvas.Children)
            {
                if (item.GetType().BaseType == typeof(UcmlObject))
                {
                    if ((item as UcmlObject).Description.Equals(name))
                    {
                        return item as UcmlObject;
                    }
                }
            }
            return null;
        }

        private static void ImportDescriptionLineUser(ref Canvas myCanvas, XmlNodeList listDescriptionLine, ControlTemplate template)
        {
            DescriptionLineUser ucmlObject;
            foreach (XmlNode xn in listDescriptionLine)
            {
                ucmlObject = new DescriptionLineUser().ImportToXml(xn);
                list[1]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString();

                var p = (TextBlock)ucmlObject.Template.FindName("Percentage", ucmlObject);
                p.Text = ucmlObject.Percentage.ToString() + "%";
                p.Foreground = ucmlObject.myColor;

                Border b = (Border)ucmlObject.Template.FindName("circle", ucmlObject);
                b.BorderBrush = ucmlObject.myColor;

                Polyline pl = (Polyline)ucmlObject.Template.FindName("Line", ucmlObject);
                TextBlock tb = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                pl.Stroke = ucmlObject.myColor;
                tb.Foreground = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                listUsers.Add(ucmlObject);
           }
        }

        private static void ImportSyncPoint(ref Canvas myCanvas, XmlNodeList listSyncPoint, ControlTemplate template)
        {
            SyncPoint ucmlObject;
            foreach (XmlNode xn in listSyncPoint)
            {
                ucmlObject = new SyncPoint().ImportToXml(xn);
                list[2]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString();

                Line l = (Line)ucmlObject.Template.FindName("Line", ucmlObject);
                TextBlock tbSync = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                l.Stroke = ucmlObject.myColor;
                tbSync.Foreground = ucmlObject.myColor;
                
                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                LoadMyUsers(ucmlObject, myCanvas);
            }
        }

        private static void ImportOptionBox(ref Canvas myCanvas, XmlNodeList listOptionBox, ControlTemplate template)
        {
            OptionBox ucmlObject;
            foreach (XmlNode xn in listOptionBox)
            {
                ucmlObject = new OptionBox().ImportToXml(xn);
                list[3]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString();

                Polyline line1 = (Polyline)ucmlObject.Template.FindName("Line1", ucmlObject);
                Polyline line2 = (Polyline)ucmlObject.Template.FindName("Line2", ucmlObject);
                TextBlock descriptionOptionBox = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                line1.Stroke = ucmlObject.myColor;
                line2.Stroke = ucmlObject.myColor;
                descriptionOptionBox.Foreground = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                LoadMyUsers(ucmlObject, myCanvas);
            }
        }

        private static void ImportCondition(ref Canvas myCanvas, XmlNodeList listCondition, ControlTemplate template)
        {
            Condition ucmlObject;
            foreach (XmlNode xn in listCondition)
            {
                ucmlObject = new Condition().ImportToXml(xn);
                list[4]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString();

                Polygon polygonCondition = (Polygon)ucmlObject.Template.FindName("Line", ucmlObject);
                polygonCondition.Stroke = ucmlObject.myColor;
                TextBlock tbCondition = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                tbCondition.Foreground = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();
            }
        }

        private static void ImportExitPath(ref Canvas myCanvas, XmlNodeList listExitPath, ControlTemplate template)
        {
            ExitPath ucmlObject;
            foreach (XmlNode xn in listExitPath)
            {
                ucmlObject = new ExitPath().ImportToXml(xn);
                list[5]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                var d = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);
                d.Text = ucmlObject.Description.ToString();
              
                
                var p = (TextBlock)ucmlObject.Template.FindName("Percentage", ucmlObject);
                p.Text = ucmlObject.Percentage.ToString()+"%";
                p.Foreground = ucmlObject.myColor;

                System.Windows.Shapes.Path path = (System.Windows.Shapes.Path)ucmlObject.Template.FindName("Line", ucmlObject);
                Polygon polygon = (Polygon)ucmlObject.Template.FindName("Polygon", ucmlObject);
                Border border = (Border)ucmlObject.Template.FindName("BorderPercentage", ucmlObject);
                TextBlock tbExitPath = (TextBlock)ucmlObject.Template.FindName("Description", ucmlObject);

                tbExitPath.Foreground = ucmlObject.myColor;
                border.BorderBrush = ucmlObject.myColor;
                path.Stroke = ucmlObject.myColor;
                polygon.Stroke = ucmlObject.myColor;
                polygon.Fill = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();


                LoadMyUsers(ucmlObject, myCanvas);

                //listConnections.Add(new ConnectionLineShape { startConect = ucmlObject.Id, endConect = ucmlObject.IdObjectStartLine });
            }
        }

        private static void ImportMerge(ref Canvas myCanvas, XmlNodeList listMerge, ControlTemplate template)
        {
            Merge ucmlObject;
            foreach (XmlNode xn in listMerge)
            {
                ucmlObject = new Merge().ImportToXml(xn);
                list[6]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                Border borderMerge = (Border)ucmlObject.Template.FindName("Circle", ucmlObject);
                borderMerge.BorderBrush = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();
            }
        }

        private static void ImportBranch(ref Canvas myCanvas, XmlNodeList listBranch, ControlTemplate template)
        {
            Branch ucmlObject;
            foreach (XmlNode xn in listBranch)
            {
                ucmlObject = new Branch().ImportToXml(xn);
                list[7]++;
                ucmlObject.Template = template;

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                ucmlObject.ApplyTemplate();

                Border borderBranch = (Border)ucmlObject.Template.FindName("Circle", ucmlObject);
                borderBranch.BorderBrush = ucmlObject.myColor;

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(ucmlObject);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();

                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(ucmlObject, ucmlObject.PosTopX);
                Canvas.SetTop(ucmlObject, ucmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(ucmlObject, 1);
                // Update the layout of the ucml object
                ucmlObject.UpdateLayout();
            }
        }

        private static void ImportLoop(ref Canvas myCanvas, XmlNodeList listLoop, ref List<Loop> list)
        {
            foreach (XmlNode xn in listLoop)
            {
                Loop ucmlObject = new Loop().ImportToXml(xn);
                list.Add(ucmlObject);
            }
        }

        private static void ImportConnections(ref Canvas myCanvas, XmlNodeList listConnections, Canvas canvas)
        {
            connectionLineList = new List<Connection>();
            foreach (XmlNode connectItem in listConnections)
            {
                Connection connect = new Connection().ImportToXml(connectItem);
                if (connect.elementoDestId != 0 && (connect.elementoOrigId != 0 || connect.elementoOrigId != 0))
                {
                    var startObj = new UcmlObject();
                    var endObj = new UcmlObject();
                    var isConnectionNo = false;

                    if (connect.elementoOrigId != 0)
                    {
                        startObj = FindObjectById(ref myCanvas, connect.elementoOrigId);
                        isConnectionNo = true;
                    }
                    else
                        startObj = FindObjectById(ref myCanvas, connect.elementoOrigId);
                        endObj = FindObjectById(ref myCanvas, connect.elementoDestId);

                    var y = new Point();
                    var x = new Point();


                    if (startObj.GetType() == typeof(Condition))
                    {
                        if ((startObj as Condition).IdObjectStarLineYes == connect.elementoDestId)
                        {
                            x = GetPointToConnection(startObj, false);
                        }
                        else
                        {
                            x = GetPointToConnection(startObj, true);
                        }
                    }
                    else
                    {
                        x = GetPointToConnection(startObj, isConnectionNo);
                    }

                    if (endObj.TemplateName.Equals(EnumTemplates.TempDescriptionLineActivity) || endObj.TemplateName.Equals(EnumTemplates.TempOptionBox))
                        y = GetEndPointToConnection(endObj);
                    else
                        y = GetPointToConnection(endObj);

                    var link = new Connection(x, y,null);

                    Connection connection = new Connection(link.initialPoint, link.endPoint, (SolidColorBrush)connect.Stroke);


                    if (startObj.GetType() == typeof(Condition))
                    {
                        if (endObj.Id == (startObj as Condition).IdObjectStarLineYes)
                        {
                            (startObj as Condition).StartLineYes = connection;
                            SetPathLines(endObj, connection, true, isConnectionNo);
                        }
                        else if (endObj.Id == (startObj as Condition).IdObjectStarLineNo)
                        {
                            (startObj as Condition).StartLineNo = connection;
                            SetPathLines(endObj, connection, true, isConnectionNo);
                        }
                        else
                        {
                            if (endObj.GetType() == typeof(Condition))
                            {
                                (endObj as Condition).EndLine = connection;
                                SetPathLines(startObj, connection, false, isConnectionNo);
                            }
                        }
                    }
                    else
                    {
                        SetPathLines(startObj, connection, false, isConnectionNo);
                        SetPathLines(endObj, connection, true, isConnectionNo);
                    }
                    

                    connection.tempUsers = connect.tempUsers;
  
                    connection.elementoOrigId = startObj.Id;
                    connection.elementoDestId = endObj.Id;        
                    myCanvas.Children.Add(connection);
                    
                    //Load users
                    LoadMyUsers(connection, myCanvas);

                    connectionLineList.Add(connection);
                }

            }
        }

        private static UcmlObject FindObjectById(ref Canvas myCanvas, int id)
        {
            foreach (var item in myCanvas.Children)
            {
                if (item.GetType().BaseType == typeof(UcmlObject))
                {
                    if ((item as UcmlObject).Id == id)
                    {
                        return item as UcmlObject;
                    }
                }
            }
            return null;
        }

        private static Point GetPointToConnection(UcmlObject obj, bool isConnectionNo = false)
        {
            var point = new Point();

            switch (obj.TemplateName)
            {
                case EnumTemplates.TempBranch:
                    var branchAux = obj as Branch;
                    point = new Point(Canvas.GetLeft(branchAux) + branchAux.ActualWidth / 2, Canvas.GetTop(branchAux) + branchAux.ActualHeight / 2);
                    break;
                case EnumTemplates.TempCondition:
                    var conditionAux = obj as Condition;
                    if (!isConnectionNo)
                    {
                        point = new Point((Canvas.GetLeft(conditionAux) + conditionAux.UcmlWidth / 2) + 21,
                                  (Canvas.GetTop(conditionAux) + conditionAux.UcmlHeight / 2) + 1);
                    }
                    else if (isConnectionNo)
                    {
                        point = new Point((Canvas.GetLeft(conditionAux) + conditionAux.UcmlWidth / 2) - 6,
                                  (Canvas.GetTop(conditionAux) + conditionAux.UcmlHeight / 2) + 28);
                    }
                    break;
                case EnumTemplates.TempDescriptionLineActivity:
                    var dlAux = obj as DescriptionLineActivity;
                    point = new Point((Canvas.GetLeft(dlAux) + dlAux.UcmlWidth),
                                      Canvas.GetTop(dlAux) + dlAux.UcmlHeight);
                    break;
                case EnumTemplates.TempDescriptionLineUser:
                    var dlU = obj as DescriptionLineUser;
                    point = new Point((Canvas.GetLeft(dlU) +  dlU.UcmlWidth) - 1,
                                      Canvas.GetTop(dlU) + dlU.UcmlHeight/1.4 - 1);
                break;

                case EnumTemplates.TempExitPath:
                    var epAux = obj as ExitPath;
                    point = new Point(Canvas.GetLeft(epAux) + 8,
                                      Canvas.GetTop(epAux));
                    break;
                case EnumTemplates.TempMerge:
                    var mergeAux = obj as Merge;
                    point = new Point(Canvas.GetLeft(mergeAux) + mergeAux.ActualWidth / 2, Canvas.GetTop(mergeAux) + mergeAux.ActualHeight / 2);
                    break;
                case EnumTemplates.TempOptionBox:
                    var obAux = obj as OptionBox;
                    point = new Point((Canvas.GetLeft(obAux) + obAux.ActualWidth) - 2,
                                      Canvas.GetTop(obAux));
                    break;
                case EnumTemplates.TempSyncPoint:
                    var spAux = obj as SyncPoint;
                    point = new Point(Canvas.GetLeft(spAux) + spAux.ActualWidth / 2, Canvas.GetTop(spAux) + spAux.ActualHeight / 2);
                    break;
            }
            return point;
        }

        private static Point GetEndPointToConnection(UcmlObject obj)
        {
            var point = new Point();

            switch (obj.TemplateName)
            {
                case EnumTemplates.TempDescriptionLineActivity:
                    var dlAux = obj as DescriptionLineActivity;
                    point = new Point(Canvas.GetLeft(dlAux) +2,
                                      Canvas.GetTop(dlAux) + dlAux.UcmlHeight - 1);
                    break;
                case EnumTemplates.TempOptionBox:
                    var obAux = obj as OptionBox;
                    point = new Point(Canvas.GetLeft(obAux),
                                      Canvas.GetTop(obAux));
                    break;
            }
            return point;
        }

        private static void SetPathLines(UcmlObject obj, Connection connection, bool isEndLine = false, bool isConnectionNo = false)
        {
            switch (obj.TemplateName)
            {
                case EnumTemplates.TempBranch:
                    if (isEndLine)
                        (obj as Branch).EndLine = connection;
                    else
                        (obj as Branch).StartLines.Add(connection);
                    break;
                case EnumTemplates.TempCondition:
                    if (isEndLine)
                        (obj as Condition).EndLine = connection;
                    else
                    {
                        if (isConnectionNo)
                            (obj as Condition).StartLineNo = connection;
                        else
                            (obj as Condition).StartLineYes = connection;
                    }
                    break;
                case EnumTemplates.TempDescriptionLineActivity:
                    if (isEndLine)
                        (obj as DescriptionLineActivity).EndLine = connection;
                    else
                        (obj as DescriptionLineActivity).StartLine = connection;
                    break;
                case EnumTemplates.TempDescriptionLineUser:
                        (obj as DescriptionLineUser).StartLine = connection;
                    break;
                case EnumTemplates.TempExitPath:
                    if (isEndLine)
                        (obj as ExitPath).EndLine = connection;
                    else
                        (obj as ExitPath).StartLine = connection;
                    break;
                case EnumTemplates.TempMerge:
                    if (isEndLine)
                        (obj as Merge).EndLines.Add(connection);
                    else
                        (obj as Merge).StartLine = connection;
                    break;
                case EnumTemplates.TempOptionBox:
                    if (isEndLine)
                        (obj as OptionBox).EndLine = connection;
                    else
                        (obj as OptionBox).StartLine = connection;
                    break;

                case EnumTemplates.TempSyncPoint:
                    if (isEndLine)
                        (obj as SyncPoint).EndLines.Add(connection);
                    else
                        (obj as SyncPoint).StartLines.Add(connection);
                    break;
            }
        }
    }

}


