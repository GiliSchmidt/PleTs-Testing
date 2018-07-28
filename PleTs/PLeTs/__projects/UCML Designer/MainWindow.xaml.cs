using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.Printing;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace ShapeConnectors
{
    public partial class Window1 : Window
    {
        #region "Variables"

        private List<Connection> ConnectionLineList = new List<Connection>();
        private List<Loop> listLoop = new List<Loop>();
        private List<UcmlObject> listUsers = new List<UcmlObject>();

        private ComboBox UserComboBox;
        private ComboBox valueTypeBox;
        private int globalId = 1;
        private ComboBox colorComboBox;
        private double canvasHeight = 0;
        private double canvasWidth = 0;
        private Boolean loopActive = false;
        // Var used to control load of properties of UCML objects
        private UcmlObject selectedObjUCML;
        private bool YesNoLine;
        private bool isAddLink;
        private bool isLoopInsert;
        private bool LoopStarted;
        private bool linkStarted;
        private Connection actualConnection = null;
        private UcmlObject originElement;
        private UcmlObject destinyElement;
        private Branch branchAux;
        private Condition conditionAux;
        private DescriptionLineActivity descriptionLineActivity;
        private DescriptionLineUser descriptionLineUser;
        private ExitPath epAux;
        private Merge mergeAux;
        private OptionBox obAux;
        private QuantityCircle qcAux;
        private QuantityCircle lqcAux;
        private SyncPoint spAux;
        private double defaultCanvasH;
        private double defaultCanvasW;
        private int[] listIdByType = new int[9];
        private String template = "";
        private UcmlObject copyUcmlObject;
        private Connection selectedConnection;
        private Border selectedLoop_Border;
        private TextBlock selectedLoop_TextBlock;
        private ScaleTransform sc = new ScaleTransform();
        private Loop actualLoop;

        #endregion

        public Window1()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  This method search the loops and work with your end and begin.
        /// </summary>
        /// <param name="element">Is a exclude element</param>
        private void ClearLoops(UcmlObject element)
        {
            if (element.GetType() == typeof(QuantityCircle))
            {
                QuantityCircle qtc = element as QuantityCircle;
                myCanvas.Children.Remove(qtc.LoopReference.bezierCurve);
                myCanvas.Children.Remove(qtc.LoopReference.myArrow);
                listLoop.Remove(qtc.LoopReference);
            }
            else
            {
                foreach (Loop loop in listLoop)
                {
                    if (loop.ObjectEnd == element || loop.ObjectStart == element)
                    {
                        myCanvas.Children.Remove(loop.myQuantityCircle);
                        myCanvas.Children.Remove(loop.bezierCurve);
                        listLoop.Remove(loop);
                        break;
                    }
                }
            }
        }

        private Loop getLoopifStart(UcmlObject ucmlObj)
        {
            foreach (var item in listLoop)
            {
                if (item.ObjectStart == ucmlObj)
                    return item as Loop;
            }
            return null;
        }

        private Loop getLoopifEnd(UcmlObject ucmlObj)
        {
            foreach (var item in listLoop)
            {
                if (item.ObjectEnd == ucmlObj)
                    return item as Loop;
            }
            return null;
        }


        void onDragCompleted(object sender, DragCompletedEventArgs e)
        {
            myThumb.Background = Brushes.Blue;
        }

        void onDragStarted(object sender, DragStartedEventArgs e)
        {
            myThumb.Background = Brushes.Orange;
        }

        private void thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Move the Thumb to the mouse position during the drag operation 
            double yadjust = myCanvas.Height + e.VerticalChange;
            double xadjust = myCanvas.Width + e.HorizontalChange;
            if ((xadjust >= 0) && (yadjust >= 0))
            {
                myCanvas.Width = xadjust;
                myCanvas.Height = yadjust;
                Canvas.SetLeft(myThumb, Canvas.GetLeft(myThumb) + e.HorizontalChange);
                Canvas.SetTop(myThumb, Canvas.GetTop(myThumb) +  e.VerticalChange);
            }
        }

        /// <summary>
        /// This method moves the objcts  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
           
            UcmlObject ucmlObj = e.Source as UcmlObject;
            //generate new coordinates
            double newLeft = Canvas.GetLeft(ucmlObj) + e.HorizontalChange;
            double newTop = Canvas.GetTop(ucmlObj) + e.VerticalChange;

            if (newLeft < 0)
                newLeft = 0;

            if (newTop < 0)
                newTop = 0;
            //Alter canvas dimensions
            if (newLeft + ucmlObj.UcmlWidth > canvasWidth)
            {
                myCanvas.Width = myCanvas.ActualWidth + (newLeft + ucmlObj.UcmlWidth - canvasWidth);
                canvasWidth = myCanvas.Width;
            }
            if (newTop + ucmlObj.UcmlHeight > canvasHeight)
            {
                myCanvas.Height = myCanvas.ActualHeight + (newTop + ucmlObj.UcmlHeight - canvasHeight);
                canvasHeight = myCanvas.Height;
            }

            //Alter object position
            ucmlObj.PosTopX = newLeft;
            ucmlObj.PosTopY = newTop;
            ucmlObj.PosBottonX = newLeft - ucmlObj.UcmlWidth;
            ucmlObj.PosBottonY = newTop - ucmlObj.UcmlHeight;

            //Update thumb coordenades

            Canvas.SetLeft(myThumb, myCanvas.Width - 20);
            Canvas.SetTop(myThumb, myCanvas.Height - 20);

            //Set coordenades in Canvas
            Canvas.SetLeft(ucmlObj, newLeft);
            Canvas.SetTop(ucmlObj, newTop);
            Canvas.SetRight(ucmlObj, ucmlObj.PosBottonX);
            Canvas.SetBottom(ucmlObj, ucmlObj.PosBottonY);

            //Test Final element

            Loop loop = getLoopifEnd(ucmlObj);

            if (loop != null)
            {
                BezierCurveShape b = loop.bezierCurve;
                Point p;

                if (e.Source.GetType().Name == EnumTemplates.TempOptionBox)
                {
                    p = new Point(Canvas.GetLeft(ucmlObj) + ucmlObj.UcmlWidth, Canvas.GetTop(ucmlObj));
                }
                else
                {
                    p = new Point(Canvas.GetLeft(ucmlObj) + ucmlObj.UcmlWidth, Canvas.GetTop(ucmlObj) + ucmlObj.UcmlHeight);
                }

                Point control1 = new Point(p.X - (p.X - loop.bezierCurve.end.X) - ucmlObj.UcmlWidth, p.Y + 100);
                Point control2 = new Point(loop.bezierCurve.end.X - ((p.X - loop.bezierCurve.end.X) / 20), p.Y + 25);

                Loop l = new Loop(b.start
                    , control1,
                    b.control2,
                    p, loop.myColor);

                l.myArrow = loop.myArrow;
                l.ObjectStart = loop.ObjectStart;
                l.ObjectEnd = loop.ObjectEnd;
                l.myQuantityCircle = loop.myQuantityCircle;
                l.myQuantityCircle.LoopReference = l;

                myCanvas.Children.Add(l.bezierCurve);
                listLoop.Add(l);

                myCanvas.Children.Remove(loop.bezierCurve);
                listLoop.Remove(loop);

                Canvas.SetLeft(loop.myQuantityCircle, l.bezierCurve.maxPoint.X);
                Canvas.SetTop(loop.myQuantityCircle, l.bezierCurve.maxPoint.Y);

                loop = new Loop();
            }
            //Test start element
            else if (getLoopifStart(ucmlObj) != null)
            {
                loop = getLoopifStart(ucmlObj);

                Point startPoint;

                switch (ucmlObj.GetType().Name)
                {
                    case EnumTemplates.TempBranch:
                        startPoint = new Point(Canvas.GetLeft(ucmlObj) + ucmlObj.ActualWidth / 2, Canvas.GetTop(ucmlObj) + ucmlObj.ActualHeight / 2);
                        break;
                    case EnumTemplates.TempDescriptionLineActivity:
                        startPoint = new Point((Canvas.GetLeft(ucmlObj) + ucmlObj.ActualWidth) - 1,
                                            Canvas.GetTop(ucmlObj) + ucmlObj.ActualHeight);
                        break;
                    case EnumTemplates.TempDescriptionLineUser:
                        startPoint = new Point((Canvas.GetLeft(ucmlObj) + ucmlObj.ActualWidth) - 1,
                                            Canvas.GetTop(ucmlObj) + ucmlObj.ActualHeight / 1.4);
                        break;
                    case EnumTemplates.TempMerge:
                        startPoint = new Point(Canvas.GetLeft(ucmlObj) + ucmlObj.ActualWidth / 2, Canvas.GetTop(ucmlObj) + ucmlObj.ActualHeight / 2);
                        break;
                    case EnumTemplates.TempOptionBox:
                        startPoint = new Point(((Canvas.GetLeft(ucmlObj) + ucmlObj.ActualWidth) - 1),
                                            Canvas.GetTop(ucmlObj));
                        break;
                }

                //Update the loop positions
                BezierCurveShape b = loop.bezierCurve;
                ///Calcule and Update 
                Point p = new Point(Canvas.GetLeft(loop.ObjectStart), Canvas.GetTop(loop.ObjectStart) + ucmlObj.UcmlHeight);

                Point control1 = new Point(p.X + ((p.X - loop.bezierCurve.end.X) / 4), p.Y + 100);
                Point control2 = new Point(loop.bezierCurve.end.X - ((p.X - loop.bezierCurve.end.X) / 20), p.Y + 25);
                //Create new Loop
                myCanvas.Children.Remove(loop.myArrow);
                Loop l = new Loop(p, b.control1, b.control2, b.end, loop.myColor);
                myCanvas.Children.Add(l.bezierCurve);

                l.ObjectStart = loop.ObjectStart;
                l.ObjectEnd = loop.ObjectEnd;
                l.myQuantityCircle = loop.myQuantityCircle;
                l.myQuantityCircle.LoopReference = l;

                Canvas.SetLeft(loop.myQuantityCircle, l.bezierCurve.maxPoint.X);
                Canvas.SetTop(loop.myQuantityCircle, l.bezierCurve.maxPoint.Y);

                //Create arrow
                ArrowShape a = new ArrowShape(new Point(Canvas.GetLeft(ucmlObj), Canvas.GetTop(ucmlObj) + ucmlObj.UcmlHeight), loop.myColor);
                l.myArrow = a;
                myCanvas.Children.Add(a);

                myCanvas.Children.Remove(loop.bezierCurve);

                listLoop.Remove(loop);
                listLoop.Add(l);
            }
            // Update lines's layouts
            UpdateConnections(ucmlObj);
        }

        /// <summary>
        /// Drag object event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragObject(object sender, MouseButtonEventArgs e)
        {
            UcmlObject objUCML = new UcmlObject();
            DataObject data = new DataObject();

            switch ((String)(e.Source as Image).Tag)
            {
                case "1": objUCML = new QuantityCircle();
                    data = new DataObject(EnumTemplates.TempQuantityCircle, objUCML);
                    break;
                case "2": objUCML = new DescriptionLineActivity();
                    data = new DataObject(EnumTemplates.TempDescriptionLineActivity, objUCML);
                    break;
                case "3": objUCML = new DescriptionLineUser();
                    data = new DataObject(EnumTemplates.TempDescriptionLineUser, objUCML);
                    break;
                case "4": objUCML = new SyncPoint();
                    data = new DataObject(EnumTemplates.TempSyncPoint, objUCML);
                    break;
                case "5": objUCML = new OptionBox();
                    data = new DataObject(EnumTemplates.TempOptionBox, objUCML);
                    break;
                case "6": objUCML = new Condition();
                    data = new DataObject(EnumTemplates.TempCondition, objUCML);
                    break;
                case "7": objUCML = new ExitPath();
                    data = new DataObject(EnumTemplates.TempExitPath, objUCML);
                    break;
                case "8": objUCML = new Merge();
                    data = new DataObject(EnumTemplates.TempMerge, objUCML);
                    break;
                case "9": objUCML = new Branch();
                    data = new DataObject(EnumTemplates.TempBranch, objUCML);
                    break;
            }

            DragDrop.DoDragDrop(objUCML, data, DragDropEffects.Copy);
        }

        /// <summary>
        /// Drop object event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropObject(object sender, DragEventArgs e)
        {
            UcmlObject objUCML = new UcmlObject();

            #region Define UCML Object Type

            if (e.Data.GetDataPresent(EnumTemplates.TempQuantityCircle))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempQuantityCircle) as QuantityCircle;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempQuantityCircle] as ControlTemplate;
                // Set name of template 
                objUCML.TemplateName = EnumTemplates.TempQuantityCircle;
                objUCML.UcmlName = EnumTemplates.TempQuantityCircle + "_" + listIdByType[0];
                listIdByType[0]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempDescriptionLineActivity))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempDescriptionLineActivity) as DescriptionLineActivity;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempDescriptionLineActivity] as ControlTemplate;
                // Set name of template 
                objUCML.TemplateName = EnumTemplates.TempDescriptionLineActivity;
                objUCML.UcmlName = EnumTemplates.TempDescriptionLineActivity + "_" + listIdByType[1];
                listIdByType[1]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempDescriptionLineUser))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempDescriptionLineUser) as DescriptionLineUser;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempDescriptionLineUser] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempDescriptionLineUser;
                objUCML.UcmlName = EnumTemplates.TempDescriptionLineUser + "_" + listIdByType[2];
                listIdByType[8]++;

                listUsers.Add(objUCML);
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempSyncPoint))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempSyncPoint) as SyncPoint;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempSyncPoint] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempSyncPoint;
                objUCML.UcmlName = EnumTemplates.TempSyncPoint + "_" + listIdByType[3];
                listIdByType[2]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempOptionBox))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempOptionBox) as OptionBox;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempOptionBox] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempOptionBox;
                objUCML.UcmlName = EnumTemplates.TempOptionBox + "_" + listIdByType[4];
                listIdByType[3]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempCondition))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempCondition) as Condition;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempCondition] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempCondition;
                objUCML.UcmlName = EnumTemplates.TempCondition + "_" + listIdByType[5];
                listIdByType[4]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempExitPath))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempExitPath) as ExitPath;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempExitPath] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempExitPath;
                objUCML.UcmlName = EnumTemplates.TempExitPath + "_" + listIdByType[6];
                listIdByType[5]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempMerge))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempMerge) as Merge;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempMerge] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempMerge;
                objUCML.UcmlName = EnumTemplates.TempMerge + "_" + listIdByType[7];
                listIdByType[6]++;
            }
            else if (e.Data.GetDataPresent(EnumTemplates.TempBranch))
            {
                // Get object conform its type
                objUCML = e.Data.GetData(EnumTemplates.TempBranch) as Branch;
                // Assign quantityCircle custom template to it
                objUCML.Template = this.Resources[EnumTemplates.TempBranch] as ControlTemplate;
                // Set name of template  
                objUCML.TemplateName = EnumTemplates.TempBranch;
                objUCML.UcmlName = EnumTemplates.TempBranch + "_" + listIdByType[8];
                listIdByType[7]++;
            }

            #endregion

            // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
            objUCML.ApplyTemplate();

            // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
            myCanvas.Children.Add(objUCML);
            // Update the layout of the ucml object

            objUCML.UpdateLayout();

            objUCML.UcmlWidth = objUCML.ActualWidth;
            objUCML.UcmlHeight = objUCML.ActualHeight;


            SetDefaultPropertiesOnNewObject(objUCML, e.GetPosition(myCanvas).X, e.GetPosition(myCanvas).Y);

            // Set position top and left of ucml object on the canvas
            Canvas.SetLeft(objUCML, objUCML.PosTopX);
            Canvas.SetTop(objUCML, objUCML.PosTopY);

            // Move our thumb to the front to be over the lines
            Canvas.SetZIndex(objUCML, 1);
            // Update the layout of the ucml object
            objUCML.UpdateLayout();
            // Increment global id 
            globalId++;

        }

        /// <summary>
        /// Set Top and left points of object in myCanvas
        /// </summary>
        /// <param name="objUCML"></param>
        /// <param name="posX">pos X is left point in canvas</param>
        /// <param name="posY">pos Y is top point in canvas</param>
        private void SetDefaultPropertiesOnNewObject(UcmlObject objUCML, Double posX, Double posY)
        {
            objUCML.Id = globalId;
            // Add the "onDragDelta" event handler that is common to all objects
            objUCML.DragDelta += new DragDeltaEventHandler(onDragDelta);
            objUCML.PreviewMouseLeftButtonDown += ObjetctSelected;

            if ((posX + objUCML.UcmlWidth) > myCanvas.ActualWidth)
                objUCML.PosTopX = Math.Round(myCanvas.ActualWidth - objUCML.UcmlWidth) - 2;
            else
                objUCML.PosTopX = posX;
            if ((posY + objUCML.UcmlHeight) > myCanvas.ActualHeight)
                objUCML.PosTopY = Math.Round(myCanvas.ActualHeight - objUCML.UcmlHeight) - 2;
            else
                objUCML.PosTopY = posY;
            Util.LoadAddicionalProperties(objUCML, myCanvas, template);

        }

        private void UpdateConnections(UcmlObject ucmlObj)
        {
            double left = Canvas.GetLeft(ucmlObj);
            double top = Canvas.GetTop(ucmlObj);

            switch (ucmlObj.TemplateName)
            {
                case EnumTemplates.TempSyncPoint:
                    SyncPoint sp = ucmlObj as SyncPoint;
                    foreach (Connection line in sp.StartLines)
                    {
                        if (line.initialPoint.X > 0.0)
                        {
                            myCanvas.Children.Remove(line);
                            line.SetStartPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                            myCanvas.Children.Add(line);
                        }
                    }

                    foreach (Connection line in sp.EndLines)
                    {
                        if (line.endPoint.X > 0.0)
                        {
                            myCanvas.Children.Remove(line);
                            line.SetEndPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                            myCanvas.Children.Add(line);
                        }
                    }
                    break;
                case EnumTemplates.TempBranch:
                    Branch b = ucmlObj as Branch;
                    foreach (Connection line in b.StartLines)
                    {
                        if (line.initialPoint.X > 0.0)
                        {
                            myCanvas.Children.Remove(line);
                            line.SetStartPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                            myCanvas.Children.Add(line);
                        }
                    }
                    if (b.EndLine.endPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(b.EndLine);
                        b.EndLine.SetEndPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                        myCanvas.Children.Add(b.EndLine);
                    }
                    break;
                case EnumTemplates.TempCondition:
                    Condition c = ucmlObj as Condition;

                    if (c.EndLine.endPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(c.EndLine);
                        c.EndLine.SetEndPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                        myCanvas.Children.Add(c.EndLine);
                    }

                    if (c.StartLineYes.initialPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(c.StartLineYes);
                        c.StartLineYes.SetStartPointLine(new Point((Canvas.GetLeft(c) + c.ActualWidth / 2) + 21,
                                              (Canvas.GetTop(c) + c.ActualHeight / 2) + 1));
                        myCanvas.Children.Add(c.StartLineYes);
                    }

                    if (c.StartLineNo.initialPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(c.StartLineNo);
                        c.StartLineNo.SetStartPointLine(new Point((Canvas.GetLeft(c) + c.ActualWidth / 2) - 6,
                                                  (Canvas.GetTop(c) + c.ActualHeight / 2) + 28));
                        myCanvas.Children.Add(c.StartLineNo);
                    }

                    break;
                case EnumTemplates.TempDescriptionLineActivity:
                    DescriptionLineActivity dl = ucmlObj as DescriptionLineActivity;
                    if (dl.StartLine.initialPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(dl.StartLine);
                        dl.StartLine.SetStartPointLine(new Point((Canvas.GetLeft(dl) + dl.ActualWidth) - 3, (Canvas.GetTop(dl) + dl.ActualHeight) - 2));
                        myCanvas.Children.Add(dl.StartLine);
                    }
                    if (dl.EndLine.endPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(dl.EndLine);
                        dl.EndLine.SetEndPointLine(new Point(Canvas.GetLeft(dl) + 2, (Canvas.GetTop(dl) + dl.ActualHeight) - 2));
                        myCanvas.Children.Add(dl.EndLine);
                    }
                    break;
                case EnumTemplates.TempDescriptionLineUser:
                    DescriptionLineUser descriptionLineUser = ucmlObj as DescriptionLineUser;
                    if (descriptionLineUser.StartLine.initialPoint.X > 0.0)
                    {

                        myCanvas.Children.Remove(descriptionLineUser.StartLine);
                        descriptionLineUser.StartLine.SetStartPointLine(new Point((Canvas.GetLeft(descriptionLineUser) + descriptionLineUser.ActualWidth) - 3,
                                                  (Canvas.GetTop(descriptionLineUser) + descriptionLineUser.ActualHeight / 1.4) - 2));
                        myCanvas.Children.Add(descriptionLineUser.StartLine);
                    }
                    break;
                case EnumTemplates.TempExitPath:
                    ExitPath ep = ucmlObj as ExitPath;
                    if (ep.EndLine.endPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(ep.EndLine);
                        ep.EndLine.SetEndPointLine(new Point((Canvas.GetLeft(ep) + (ep.ActualWidth / 2)) - 14,
                                                  Canvas.GetTop(ep) + 1));
                        myCanvas.Children.Add(ep.EndLine);
                    }
                    break;
                case EnumTemplates.TempMerge:
                    Merge m = ucmlObj as Merge;
                    foreach (Connection line in m.EndLines)
                    {
                        if (line.initialPoint.X > 0.0)
                        {
                            myCanvas.Children.Remove(line);
                            line.SetEndPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                            myCanvas.Children.Add(line);
                        }
                    }
                    if (m.StartLine.initialPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(m.StartLine);
                        m.StartLine.SetStartPointLine(new Point(left + ucmlObj.ActualWidth / 2, top + ucmlObj.ActualHeight / 2));
                        myCanvas.Children.Add(m.StartLine);
                    }

                    break;
                case EnumTemplates.TempOptionBox:
                    OptionBox ob = ucmlObj as OptionBox;
                    if (ob.StartLine.initialPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(ob.StartLine);
                        ob.StartLine.SetStartPointLine(new Point((Canvas.GetLeft(ob) + ob.ActualWidth) - 1, Canvas.GetTop(ob) + 1));
                        myCanvas.Children.Add(ob.StartLine);
                    }

                    if (ob.EndLine.endPoint.X > 0.0)
                    {
                        myCanvas.Children.Remove(ob.EndLine);
                        ob.EndLine.SetEndPointLine(new Point(Canvas.GetLeft(ob) + 1, Canvas.GetTop(ob)));
                        myCanvas.Children.Add(ob.EndLine);
                    }
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a default ucmlObject to use as base to know which ucmlObject is selected 
            selectedConnection = null;
            selectedObjUCML = new UcmlObject();
            selectedLoop_TextBlock = new TextBlock();
            selectedLoop_Border = new Border();

            // Set default values of canvas
            defaultCanvasH = myCanvas.ActualHeight;
            defaultCanvasW = myCanvas.ActualWidth;
            canvasHeight = myCanvas.ActualHeight;
            canvasWidth = myCanvas.ActualHeight;

            this.KeyDown += new KeyEventHandler(EventKeyDown);
            // Methods that can be called by design canvas
            myCanvas.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(WindowPreviewMouseLeftButtonDown);
            myCanvas.PreviewMouseMove += new MouseEventHandler(WindowPreviewMouseMove);
            myCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(WindowPreviewMouseLeftButtonUp);
        }

        private void WindowPreviewMouseMove(object sender, MouseEventArgs e)
        {
            //usuário inicio uma conexão
            if (isAddLink && linkStarted)
            {
                Point p = e.GetPosition(myCanvas);
                Connection connection = new Connection(actualConnection.initialPoint, p, null);
                myCanvas.Children.Add(connection);
                myCanvas.Children.Remove(actualConnection);
                actualConnection = connection;
                e.Handled = true;
            }
            ///User beggining the curve
            if (isLoopInsert && LoopStarted)
            {
                //Mouse point
                Point p = e.GetPosition(myCanvas);
                //
                Point control1 = new Point(p.X - ((p.X - actualLoop.bezierCurve.end.X) / 4), p.Y + 100);
                Point control2 = new Point(actualLoop.bezierCurve.end.X - ((p.X - actualLoop.bezierCurve.end.X) / 20), p.Y + 40);
                //Create Canvas
                Loop l = new Loop(p, control1, control2, actualLoop.bezierCurve.end, Brushes.Black);

                l.ObjectEnd = actualLoop.ObjectEnd;
                //Update canvas
                myCanvas.Children.Remove(actualLoop.myArrow);
                myCanvas.Children.Add(l.bezierCurve);
                myCanvas.Children.Remove(actualLoop.bezierCurve);
                actualLoop = l;

                ArrowShape a = new ArrowShape(p, Brushes.Black);
                actualLoop.myArrow = a;
                l.myArrow = a;
                myCanvas.Children.Add(a);

                e.Handled = true;
            }
        }
        /// <summary>
        /// when left button it's loose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
                LeftButtonUp(sender, e);
        }

        private void LeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Boolean linked = false;
            //usuário iniciou um loop
            if (isLoopInsert && LoopStarted)
            {
                if (e.Source.GetType().BaseType == typeof(UcmlObject))
                {
                    destinyElement = e.Source as UcmlObject;
                    if (originElement != destinyElement && destinyElement.GetType().Name.Equals(EnumTemplates.TempDescriptionLineActivity))
                    {
                        //Create loop
                        Loop l = new Loop(new Point(
                        Canvas.GetLeft(destinyElement), Canvas.GetTop(destinyElement) + destinyElement.UcmlHeight),
                        actualLoop.bezierCurve.control1, actualLoop.bezierCurve.control2, actualLoop.bezierCurve.end, Brushes.Black);
                        l.ObjectStart = destinyElement;
                        l.ObjectEnd = actualLoop.ObjectEnd;
                        //Update canvas
                        myCanvas.Children.Add(l.bezierCurve);
                        myCanvas.Children.Remove(actualLoop.myArrow);
                        myCanvas.Children.Remove(actualLoop.bezierCurve);
                        //Set global bCurve with a new bezier
                        this.actualLoop = l;
                        //Add loop
                        myCanvas.Children.Remove(actualLoop.myArrow);
                        listLoop.Remove(actualLoop);
                        listLoop.Add(l);

                        //Create a loop quantity circle
                        QuantityCircle objQuant = new QuantityCircle();
                        // Assign quantityCircle custom template to it
                        objQuant.Template = this.Resources[EnumTemplates.TempQuantityCircle] as ControlTemplate;
                        // Set name of template  
                        objQuant.UcmlName = EnumTemplates.TempQuantityCircle + "_" + listIdByType[7];
                        objQuant.TemplateName = EnumTemplates.TempQuantityCircle;
                        listIdByType[0]++;

                        objQuant.ApplyTemplate();
                        // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                        myCanvas.Children.Add(objQuant);
                        // Update the layout of the ucml object
                        objQuant.UpdateLayout();
                        SetDefaultPropertiesOnNewObject(objQuant, l.bezierCurve.maxPoint.X, l.bezierCurve.maxPoint.Y);
                        // Set position top and left of ucml object on the canvas
                        Canvas.SetLeft(objQuant, objQuant.PosTopX);
                        Canvas.SetTop(objQuant, objQuant.PosTopY);
                        // Move our thumb to the front to be over the lines
                        Canvas.SetZIndex(objQuant, 1);
                        // Update the layout of the ucml object
                        objQuant.UpdateLayout();
                        // Increment global id 
                        globalId++;

                        l.myQuantityCircle = objQuant;
                        objQuant.LoopReference = l;

                        l.bezierCurve.PreviewMouseLeftButtonDown += ObjetctSelected;

                        //Create arrow
                        Point p = new Point(Canvas.GetLeft(destinyElement), Canvas.GetTop(destinyElement) + destinyElement.ActualHeight);
                        ArrowShape a = new ArrowShape(p, Brushes.Black);
                        l.myArrow = a;
                        l.myColor = Brushes.Black;
                        myCanvas.Children.Add(a);

                        loopActive = true;
                        linked = true;
                    }
                    else
                    {
                        myCanvas.Children.Remove(actualLoop.myArrow);
                        myCanvas.Children.Remove(actualLoop.bezierCurve);
                        MessageBox.Show("Invalid UCML final element");
                        linked = false;
                    }
                }

                isLoopInsert = LoopStarted = false;
                destinyElement = new UcmlObject();
                originElement = new UcmlObject();
                Mouse.OverrideCursor = null;
                actualLoop = null;
                e.Handled = true;
            }
            //usuário iniciou uma conexão 
            if (isAddLink && linkStarted)
            {
                myCanvas.Children.Remove(actualConnection);
               
                if (e.Source.GetType().BaseType == typeof(UcmlObject))
                {
                    actualConnection.endPoint = e.GetPosition(this);
                    List<object> usersStr = new List<object>();

                    if (actualConnection.endPoint != actualConnection.initialPoint && originElement != destinyElement)
                    {
                        UcmlObject objCopy = new UcmlObject();
                        objCopy = e.Source as UcmlObject;

                        UcmlObject auxObject = e.Source as UcmlObject;

                        Connection connection = new Connection(actualConnection.initialPoint, Validators.GetDestinyPoint(e.Source.GetType().Name,
                                                                  actualConnection,
                                                                  originElement,
                                                                  auxObject), null);

                        bool isValido = Validators.ValidaConexao(originElement.GetType().ToString().Split('.')[1],
                                                                  e.Source.GetType().Name,
                                                                  connection,
                                                                  originElement,
                                                                  auxObject,
                                                                  YesNoLine,
                                                                  originElement.myUsers);

                        if (isValido)
                        {
                            myCanvas.Children.Add(connection);

                            connection.PreviewMouseLeftButtonDown += ObjetctSelected;
                            connection.elementoOrigId = originElement.Id;
                            connection.elementoDestId = auxObject.Id;
                            ConnectionLineList.Add(connection);

                            linked = true;

                            if (auxObject.GetType() == typeof(SyncPoint) || originElement.GetType() == typeof(QuantityCircle))
                            {
                                foreach (int id in (auxObject as SyncPoint).IdObjectStarLines)
                                {
                                    foreach (var item in myCanvas.Children)
                                    {
                                        if (item.GetType().BaseType == typeof(UcmlObject) && id == (item as UcmlObject).Id)
                                            (item as UcmlObject).myUsers = auxObject.myUsers;
                                    }
                                }
                            }

                        }
                        else
                        {
                            DeleteReferences(actualConnection);
                            DeleteReferences(connection);
                            myCanvas.Children.Remove(actualConnection);
                            actualConnection = null;
                        }
                    }
                }

                if (!linked)
                {
                    qcAux = null;
                    actualConnection = null;
                }

                isAddLink = linkStarted = isLoopInsert = false;
                actualConnection = null;
                Mouse.OverrideCursor = null;
                e.Handled = true;
            }
        }

        /// <summary>
        /// When left btn is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (loopActive == true)
            {
                LeftButtonUp(sender, e);
                loopActive = false;
                return;
            }

            //usuário começou um loop e selecionou o objeto origem
            if (isLoopInsert && e.Source.GetType().BaseType == typeof(UcmlObject) && LoopStarted == false)
            {
                Mouse.OverrideCursor = null;
                originElement = e.Source as UcmlObject;
                if (originElement.GetType() != typeof(OptionBox))
                {
                    MessageBox.Show("Invalid origin element");
                    return;
                }

                //calcule new Point
                Point p;

                if (e.Source.GetType().Name == EnumTemplates.TempOptionBox)
                {
                    p = new Point(Canvas.GetLeft(originElement) + originElement.UcmlWidth,
                    Canvas.GetTop(originElement));
                }
                else
                {
                    p = new Point(Canvas.GetLeft(originElement) + originElement.UcmlWidth,
                   Canvas.GetTop(originElement) + originElement.UcmlHeight);
                }

                //Create Loop
                Loop l = new Loop(p);
                actualLoop = l;
                //Set origin element, he has curve associated
                l.ObjectEnd = originElement;
                //Update canvas
                myCanvas.Children.Add(l.bezierCurve);
                loopActive = true;
                LoopStarted = true;
                e.Handled = true;
            }
            //usuário inciciou uma conexão e selecionou o objeto origem
            else if (isAddLink && e.Source.GetType().BaseType == typeof(UcmlObject))
            {
                // se não comecei a criar a conexão
                if (!linkStarted)
                {
                    if (actualConnection == null || actualConnection.endPoint != actualConnection.initialPoint)
                    {
                        Point position = new Point();
                        switch (e.Source.GetType().Name)
                        {
                            case EnumTemplates.TempBranch:
                                originElement = e.Source as Branch;
                                branchAux = e.Source as Branch;
                                position = new Point(Canvas.GetLeft(branchAux) + branchAux.ActualWidth / 2, Canvas.GetTop(branchAux) + branchAux.ActualHeight / 2);
                                break;
                            case EnumTemplates.TempCondition:
                                originElement = e.Source as Condition;
                                conditionAux = e.Source as Condition;
                                MessageBoxResult result = MessageBox.Show("Do you want to create connection in YES flow or NO flow?", "Confirmation", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    YesNoLine = true;
                                    position = new Point((Canvas.GetLeft(conditionAux) + conditionAux.ActualWidth / 2) + 21,
                                              (Canvas.GetTop(conditionAux) + conditionAux.ActualHeight / 2) + 1);
                                }
                                else if (result == MessageBoxResult.No)
                                {
                                    YesNoLine = false;
                                    position = new Point((Canvas.GetLeft(conditionAux) + conditionAux.ActualWidth / 2) - 6,
                                              (Canvas.GetTop(conditionAux) + conditionAux.ActualHeight / 2) + 28);
                                }
                                break;
                            case EnumTemplates.TempDescriptionLineActivity:
                                originElement = e.Source as DescriptionLineActivity;
                                descriptionLineActivity = e.Source as DescriptionLineActivity;
                                Polyline pl = (Polyline)originElement.Template.FindName("Line", originElement);

                                position = new Point((Canvas.GetLeft(descriptionLineActivity) + descriptionLineActivity.ActualWidth) - 1,
                                                  Canvas.GetTop(descriptionLineActivity) + descriptionLineActivity.ActualHeight);
                                break;
                            case EnumTemplates.TempDescriptionLineUser:

                                originElement = e.Source as DescriptionLineUser;
                                descriptionLineUser = e.Source as DescriptionLineUser;
                                Polyline descriptionUser = (Polyline)originElement.Template.FindName("Line", originElement);

                                position = new Point((Canvas.GetLeft(descriptionLineUser) + descriptionLineUser.ActualWidth) - 1,
                                                  Canvas.GetTop(descriptionLineUser) + descriptionLineUser.ActualHeight / 1.4);
                                break;
                            case EnumTemplates.TempExitPath:
                                originElement = e.Source as ExitPath;
                                epAux = e.Source as ExitPath;
                                position = new Point(Canvas.GetLeft(epAux) + 1,
                                                  Canvas.GetTop(epAux));
                                break;
                            case EnumTemplates.TempMerge:
                                originElement = e.Source as Merge;
                                mergeAux = e.Source as Merge;
                                position = new Point(Canvas.GetLeft(mergeAux) + mergeAux.ActualWidth / 2, Canvas.GetTop(mergeAux) + mergeAux.ActualHeight / 2);
                                break;
                            case EnumTemplates.TempOptionBox:
                                originElement = e.Source as OptionBox;
                                obAux = e.Source as OptionBox;
                                position = new Point((Canvas.GetLeft(obAux) + obAux.ActualWidth) - 1,
                                                  Canvas.GetTop(obAux));
                                break;
                            case EnumTemplates.TempQuantityCircle:
                                originElement = e.Source as QuantityCircle;
                                lqcAux = e.Source as QuantityCircle;
                                position = new Point(Canvas.GetLeft(qcAux) + qcAux.ActualWidth / 2, Canvas.GetTop(qcAux) + qcAux.ActualHeight / 2);
                                break;

                            case EnumTemplates.TempSyncPoint:
                                originElement = e.Source as SyncPoint;
                                spAux = e.Source as SyncPoint;
                                position = new Point(Canvas.GetLeft(spAux) + spAux.ActualWidth / 2, Canvas.GetTop(spAux) + spAux.ActualHeight / 2);
                                break;
                            default:
                                DeleteReferences(actualConnection);
                                return;
                        }

                        Connection connection = new Connection(position, position, originElement.myColor);
                        //ConnectionLineList.Add(connection);
                        myCanvas.Children.Add(connection);
                        actualConnection = connection;
                        linkStarted = true;
                        e.Handled = true;
                    }
                    DeleteReferences(actualConnection);
                }
            }
            //Se o objeto selecionado não for null e o id dele for diferente de 0
            if (selectedObjUCML != null || selectedObjUCML.Id != 0)
            {
                SetIsSelected(false);
                this.NameNewProperty.IsEnabled = false;
                this.ValueNewProperty.IsEnabled = false;
                this.InsertNewProperty.IsEnabled = false;
            }

            if (selectedConnection != null)
            {
                SetIsSelectedConnection(false);
            }

        }

        #region Grid

        private void InsertNewPropAtGrid(object sender, RoutedEventArgs e)
        {
            // If no one object is selected return and does insert new property
            if (selectedObjUCML.Id == 0)
                return;

            if (this.NameNewProperty.Text.Equals(""))
            {
                this.NameNewProperty.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
                if (this.ValueNewProperty.Text.Equals(""))
                {
                    this.ValueNewProperty.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
                }
                return;
            }
            if (this.ValueNewProperty.Text.Equals(""))
            {
                this.ValueNewProperty.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
                if (this.NameNewProperty.Text.Equals(""))
                {
                    this.NameNewProperty.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
                }
                return;
            }

            this.NameNewProperty.BorderBrush = new SolidColorBrush(Colors.DarkGray);
            this.ValueNewProperty.BorderBrush = new SolidColorBrush(Colors.DarkGray);

            // Create a new row on grip properties for the new add property, setting it's height 20  
            // Insert new row at the end of list properties
            gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });

            // Create a label to the new property's name
            var newLabelDescription = new TextBlock { Text = this.NameNewProperty.Text, HorizontalAlignment = HorizontalAlignment.Left, Margin = new System.Windows.Thickness(1), Padding = new System.Windows.Thickness(3) };
            // Insert textBlock at last row of the grid list and firt column 
            Grid.SetRow(newLabelDescription, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(newLabelDescription, 0);
            // Add textBlock as Property Grid child
            gridProp.Children.Add(newLabelDescription);
            // Create a textBox for the property value
            var newValue = new TextBox { Text = this.ValueNewProperty.Text, Margin = new System.Windows.Thickness(1), Tag = selectedObjUCML.listNewProp.Count, TextAlignment = TextAlignment.Right };
            // Associate the event-handling method with the textChange event
            //newValue.KeyDown += new KeyEventHandler(NewPropertyValueKeyDown);

            Grid.SetRow(newValue, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(newValue, 1);

            // Add textBox as Property Grid child
            gridProp.Children.Add(newValue);

            Button bt = new Button { Margin = new System.Windows.Thickness(1), Tag = selectedObjUCML.listNewProp.Count, Height = 15, Width = 15, Name = "Bt", Content = "X", FontSize = 8, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            bt.Click += new RoutedEventHandler(DeleteProperty);

            Grid.SetRow(bt, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(bt, 2);

            // Add button as Property Grid child
            gridProp.Children.Add(bt);

            // For each object already in the canvas check if the kind of them are the same as the selected object  
            foreach (var child in myCanvas.Children)
            {
                // Check if the kind of the ucml object is the same as the selected object, if it´s create the same new property for it 
                if (child.GetType() == selectedObjUCML.GetType() && !child.Equals(selectedObjUCML))
                {
                    var c = (UcmlObject)child;
                    // Create a new property and add it to the list of new properties
                    var prop1 = new AdditionalProperty { name = this.NameNewProperty.Text, value = "" };
                    // Add new property to new property list  
                    c.listNewProp.Add(prop1);
                }
            }

            // Create a new property for the selected object and add it to the list of new properties
            var prop2 = new AdditionalProperty { name = this.NameNewProperty.Text, value = this.ValueNewProperty.Text };
            selectedObjUCML.listNewProp.Add(prop2);

            // Reset values of Name Property and Value Property
            this.NameNewProperty.Text = "";
            this.ValueNewProperty.Text = "";
        }

        private void ObjetctSelected(object sender, MouseButtonEventArgs e)
        {
            ClearGridProperties();
            // Reset values of Name Property and Value Property
            this.NameNewProperty.Text = "";
            this.ValueNewProperty.Text = "";

            // Just clean Property Grid and load all properties of a ucml object if it´s not the selected object
            // If e is a UcmlObject
            if (e.Source.GetType().BaseType == typeof(UcmlObject))
            {
                if (selectedObjUCML.Id != (e.Source as UcmlObject).Id)
                {
                    this.NameNewProperty.IsEnabled = true;
                    this.ValueNewProperty.IsEnabled = true;
                    this.InsertNewProperty.IsEnabled = true;

                    this.NameNewProperty.BorderBrush = new SolidColorBrush(Colors.DarkGray);
                    this.ValueNewProperty.BorderBrush = new SolidColorBrush(Colors.DarkGray);

                    // Verify it´s not the first you select a ucml object, if isn´t clean Property grid and load headers 
                    if (selectedObjUCML.Id != 0)
                    {
                        SetIsSelected(false);
                        // Call method to clean Property grid and load headers
                        ClearGridProperties();
                    }

                    // Cast source parameter as UcmlObject and set it in variable global ucml object 
                    selectedObjUCML = e.Source as UcmlObject;


                    SetIsSelected(true);

                    // Call method to load default properties of the selected ucml object 
                    LoadDefaultUcmlObjectsProperties();
                    // Call method to load all addicional properties of the selected ucml object 
                    LoadAddicionalsProperties();
                    if (selectedObjUCML.isAbleUserSelection)
                    {
                        InsertNewUserAtGrid();
                    }
                }
            }
            else if (e.Source.GetType() == typeof(Connection))
            {
                selectedConnection = e.Source as Connection;
                SetIsSelectedConnection(true);
            }
            else if (e.Source.GetType() == typeof(BezierCurveShape))
            {
                SetIsSelectedConnection(true);
            }
            else if (e.Source.GetType() == typeof(Border) || e.Source.GetType() == typeof(TextBlock))
            {
                selectedLoop_Border = new Border();
                selectedLoop_TextBlock = new TextBlock();
            }
        }

        private void SetIsSelected(bool isSelected)
        {
            if (selectedObjUCML != null && selectedObjUCML.Id != 0)
            {
                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                selectedObjUCML.ApplyTemplate();

                // Access the textblock element of template and change it too
                var border = (Border)selectedObjUCML.Template.FindName("Border", selectedObjUCML);
                border.BorderThickness = new Thickness(1);

                if (isSelected)
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                else
                    border.BorderBrush = new SolidColorBrush(Colors.Transparent);

                // Update the layout of the ucml object
                selectedObjUCML.UpdateLayout();

                if (!isSelected)
                {
                    selectedObjUCML = new UcmlObject();
                    ClearGridProperties();
                }
            }
        }

        private void SetIsSelectedConnection(bool isSelected)
        {
            if (selectedConnection != null)
            {

                if (isSelected)
                {

                 #region Color Properties

                    // Create a new row at the Property Grid
                    gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                    // Create a label for the property name
                    var labelColor = new TextBlock { Text = "Color", Padding = new System.Windows.Thickness(3) };
                    Grid.SetRow(labelColor, gridProp.RowDefinitions.Count - 1);
                    Grid.SetColumn(labelColor, 0);
                    // Add label as Property Grid child
                    gridProp.Children.Add(labelColor);
                    // Create new comboBox 
                    colorComboBox = new ComboBox { Margin = new System.Windows.Thickness(1) };
                    // Load all colors
                    foreach (System.Reflection.PropertyInfo prop in typeof(Colors).GetProperties())
                    {
                        if (prop.PropertyType.FullName == "System.Windows.Media.Color")
                            colorComboBox.Items.Add(prop.Name);
                    }

                    for (int i = 0; i < colorComboBox.Items.Count; i++)
                    {
                        if ((selectedConnection.Stroke as SolidColorBrush) == new BrushConverter().ConvertFromString((string)colorComboBox.Items[i]) as SolidColorBrush)
                        {
                            colorComboBox.SelectedIndex = i;
                        }
                    }
                    // Associate the event-handling method with the selectedIndexChanged event. 
                    colorComboBox.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxChanged);

                    Grid.SetRow(colorComboBox, gridProp.RowDefinitions.Count - 1);
                    Grid.SetColumn(colorComboBox, 1);
                    // Add comboBox as Property Grid child
                    gridProp.Children.Add(colorComboBox);

                 #endregion

                 # region Users Properties

                    // Create a new row at the Property Grid
                    gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                    // Create a label for the property name
                    var labelAddUser = new TextBlock { Text = "Add user", Padding = new System.Windows.Thickness(3) };
                    Grid.SetRow(labelAddUser, gridProp.RowDefinitions.Count - 1);
                    Grid.SetColumn(labelAddUser, 0);
                    // Add label as Property Grid child
                    gridProp.Children.Add(labelAddUser);
                    // Create new comboBox 
                    UserComboBox = new ComboBox { Margin = new System.Windows.Thickness(1) };

                    foreach (var userName in listUsers)
                    {
                        bool aux = true;
                        foreach (var objName in selectedConnection.myUsers.Keys)
                        {
                            if (objName == userName)
                            {
                                aux = false;
                                break;
                            }
                        }

                        if (aux)
                        {
                            UserComboBox.Items.Add(userName.Description);
                        }
                    }

                    // Associate the event-handling method with the selectedIndexChanged event. 
                    UserComboBox.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxUserChanged);

                    Grid.SetRow(UserComboBox, gridProp.RowDefinitions.Count - 1);
                    Grid.SetColumn(UserComboBox, 1);
                    // Add comboBox as Property Grid child
                    gridProp.Children.Add(UserComboBox);

                    InsertNewUserAtGrid();

                #endregion
                   
                }

                if (!isSelected)
                {
                    selectedConnection = null;
                }
            }
        }

        private void LoadAddicionalsProperties()
        {
            var tag = 0;
            // For each new property added at selected object, load each one and put it on the Property Grid 
            foreach (var prop in selectedObjUCML.listNewProp)
            {
                // Create a new row at the Property Grid
                gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                // Create a label for the property name
                var labelName = new TextBlock { Text = prop.name, Margin = new System.Windows.Thickness(1), Padding = new System.Windows.Thickness(3) };
                Grid.SetRow(labelName, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(labelName, 0);
                // Add label as Property Grid child
                gridProp.Children.Add(labelName);

                // Create a textBox for the property value
                var textValue = new TextBox { Margin = new System.Windows.Thickness(1), Tag = tag, TextAlignment = TextAlignment.Right };
                // Associate the event-handling method with the events
                textValue.TextChanged += new TextChangedEventHandler(PropertiesTextChanged);
                // Verify property value, if it already has a value set it
                if (prop.value != null)
                    textValue.Text = prop.value;

                Grid.SetRow(textValue, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(textValue, 1);
                // Add textBox as Property Grid child
                gridProp.Children.Add(textValue);

                Button bt = new Button { Margin = new System.Windows.Thickness(1), Tag = selectedObjUCML.listNewProp.Count - 1, Height = 15, Width = 15, Name = "bt", Content = "X", FontSize = 8, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
                bt.Click += new RoutedEventHandler(DeleteProperty);

                Grid.SetRow(bt, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(bt, 2);

                // Add button as Property Grid child
                gridProp.Children.Add(bt);

                // Increment tag
                tag++;
            }
        }

        private UcmlObject GetUserByName(String name)
        {
            foreach (var item in listUsers)
	        {
		            if (item.Description == name)
                            return item;
	        }
            return null;
        }

        private void OnMyComboBoxUserChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserComboBox.SelectedValue == String.Empty)
                return;

            UcmlObject user = GetUserByName((String)UserComboBox.SelectedValue);

            if (user == null)
                return;

            if (selectedConnection != null)
            {
                //Add user to object users list
                selectedConnection.myUsers.Add(GetUserByName((String)UserComboBox.SelectedValue), 0);

                //Update grid
                InsertNewUserAtGrid();

                for (int i = 0; i < UserComboBox.Items.Count; i++)
                {
                    if (UserComboBox.Items[i].Equals(user.Description))
                    {
                        UserComboBox.Items.RemoveAt(i);
                    }
                }
            }
            else if (!selectedObjUCML.myUsers.ContainsKey(user))
            {
                //Add user to object users list
                selectedObjUCML.myUsers.Add(GetUserByName((String)UserComboBox.SelectedValue), 0);

                //Update grid
                InsertNewUserAtGrid();

                for (int i = 0; i < UserComboBox.Items.Count; i++)
                {
                    if (UserComboBox.Items[i] == user.Description)
                    {
                        UserComboBox.Items.RemoveAt(i);
                    }
                }
            }

         }

        /// <summary>
        /// Change the quantity circle type value 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMyComboBoxValueTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            String selectedType = (string)valueTypeBox.SelectedValue;
            var txt = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
            QuantityCircle lqc = selectedObjUCML as QuantityCircle;

            if ((selectedObjUCML as QuantityCircle).iterationProperty == "Percentage")
            {
                txt.Text = lqc.Percentage.ToString();
                lqc.iterationProperty = selectedType;
            }
            else
            {
                txt.Text = lqc.Percentage + "%";
                lqc.iterationProperty = selectedType;
            }
        }

        /// <summary>
        /// Change the ucml object color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMyComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            SolidColorBrush color = new BrushConverter().ConvertFromString((string)colorComboBox.SelectedValue) as SolidColorBrush;
            if (selectedConnection != null)
            {
                Connection connectionLine = GetConnectionLineObj(selectedConnection);
                connectionLine.Stroke = color;
                selectedConnection = null;
                return;
            }

            // Update layout to set changes in template
            selectedObjUCML.UpdateLayout();

            // Enable changes on ucml object template 
            selectedObjUCML.ApplyTemplate();

            //Change the object colors
            #region Changing Colors

            Border border;
            Polyline polygon;
            TextBlock description, percentage;
            string templateName = selectedObjUCML.TemplateName;


            if (templateName.Equals(EnumTemplates.TempDescriptionLineUser))
            {
                border = (Border)selectedObjUCML.Template.FindName("circle", selectedObjUCML);
                border.BorderBrush = color;
                description = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);
                description.Foreground = color;
                percentage = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
                percentage.Foreground = color;
            }
            if (templateName.Equals(EnumTemplates.TempDescriptionLineActivity) || templateName.Equals(EnumTemplates.TempDescriptionLineUser))
            {
                polygon = (Polyline)selectedObjUCML.Template.FindName("Line", selectedObjUCML);
                description = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);
                description.Foreground = color;
                polygon.Stroke = color;
            }
            if (templateName.Equals(EnumTemplates.TempMerge) || templateName.Equals(EnumTemplates.TempBranch))
            {
                border = (Border)selectedObjUCML.Template.FindName("Circle", selectedObjUCML);
                border.BorderBrush = color;
            }

            switch (selectedObjUCML.TemplateName)
            {
                case EnumTemplates.TempOptionBox:
                    polygon = (Polyline)selectedObjUCML.Template.FindName("Line1", selectedObjUCML);
                    polygon.Stroke = color;
                    polygon = (Polyline)selectedObjUCML.Template.FindName("Line2", selectedObjUCML);
                    polygon.Stroke = color;
                    description = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);
                    description.Foreground = color;
                    break;
                case EnumTemplates.TempQuantityCircle:
                    QuantityCircle lqc = selectedObjUCML as QuantityCircle;
                    lqc.ChangeColor(color, selectedObjUCML);

                    break;
                case EnumTemplates.TempExitPath:
                    percentage = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
                    percentage.Foreground = color;
                    description = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);
                    description.Foreground = color;
                    Polygon poly = (Polygon)selectedObjUCML.Template.FindName("Polygon", selectedObjUCML);
                    poly.Stroke = color;
                    poly.Fill = color;
                    border = (Border)selectedObjUCML.Template.FindName("BorderPercentage", selectedObjUCML);
                    border.BorderBrush = color;
                    System.Windows.Shapes.Path path;
                    path = (System.Windows.Shapes.Path)selectedObjUCML.Template.FindName("Line", selectedObjUCML);
                    path.Stroke = color;
                    break;
            }
            #endregion

            // Create a brush converter to get selected value at combo box color and convert it to solidbrush and set it to the ucml object border
            selectedObjUCML.myColor = new BrushConverter().ConvertFromString((string)colorComboBox.SelectedValue) as SolidColorBrush;
            // Update layout to set changes in template
            selectedObjUCML.UpdateLayout();
        }

        /// <summary>
        /// When properties text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesTextChanged(object sender, TextChangedEventArgs e)
        {
            // Convert sender in TextBox
            var textBox = (TextBox)sender;

            if (selectedConnection != null)
            {
                //Get user name and user object
                String userName = textBox.Tag.ToString().Substring(0, textBox.Tag.ToString().LastIndexOf("_PercentageUser"));
                UcmlObject obj = GetUserByName(userName);
                //Set correct values in existent object
                if (textBox.Text.Equals(""))
                {
                    textBox.Text = "0";
                }

                string value = textBox.Text.Replace("%", "");

                //If value isn't empty ,Update actors dictionary
                if (value != string.Empty)
                {
                    selectedConnection.myUsers[obj] = Convert.ToInt32(value);
                }
                else
                {
                    // Access the textblock element of template and change it too
                    textBox.Text = value + "0%";
                }
            }
            //Verify if selected object is not null and exist a tag in TextBox
            else if (selectedObjUCML != null && selectedObjUCML.Id != 0 && textBox.Tag != null)
            {
                double doubleValue;
                string description = textBox.Tag.ToString();

                if (textBox.Tag.Equals("Description"))
                {
                    // Set new value to the property description
                    selectedObjUCML.Description = textBox.Text;
                    // Access the textblock element of template and change it too
                    var txt = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);

                    if (txt != null)
                    {
                        if (selectedObjUCML.GetType() == typeof(DescriptionLineActivity))
                       
                            txt.Text = textBox.Text + " (" + selectedObjUCML.Percentage + "%)";
                        else
                            txt.Text = textBox.Text;
                    }
                }
                else if (textBox.Tag.Equals("Percentage") || textBox.Tag.Equals("PercentageLoop")
                    && (selectedObjUCML.TemplateName.Equals(EnumTemplates.TempQuantityCircle)
                    || selectedObjUCML.TemplateName.Equals(EnumTemplates.TempExitPath) ||
                        selectedObjUCML.TemplateName.Equals(EnumTemplates.TempLoop) ||
                    selectedObjUCML.TemplateName.Equals(EnumTemplates.TempDescriptionLineActivity) ||
                    selectedObjUCML.TemplateName.Equals(EnumTemplates.TempQuantityCircle)))
                {
                    Boolean validateValue = Double.TryParse(textBox.Text, out doubleValue);
                    if (textBox.Text.Equals("") || validateValue == false || doubleValue > 100 || doubleValue < 0)
                    {
                        return;
                    }
                    // Set new value to the property percentage according to the type
                    switch (selectedObjUCML.TemplateName)
                    {
                        case EnumTemplates.TempQuantityCircle:
                            ((QuantityCircle)selectedObjUCML).Percentage = doubleValue;
                            break;
                        case EnumTemplates.TempLoop:
                            ((Loop)selectedObjUCML).Percentage = doubleValue;
                            break;
                        case EnumTemplates.TempExitPath:
                            ((ExitPath)selectedObjUCML).Percentage = doubleValue;
                            break;
                        case EnumTemplates.TempDescriptionLineActivity:
                            ((DescriptionLineActivity)selectedObjUCML).Percentage = doubleValue;
                            break;
                        case EnumTemplates.TempDescriptionLineUser:
                            if (UserPercentageValidate(doubleValue))
                                ((DescriptionLineUser)selectedObjUCML).Percentage = doubleValue;
                            else
                            {
                                MessageBox.Show("Total user percentage sum invalid (>100) ");
                                textBox.Text = "0"; doubleValue = 0;
                            }
                            break;
                    }

                    // Define percentage at DescriptionLine in it's description 
                    if (selectedObjUCML.TemplateName.Equals(EnumTemplates.TempDescriptionLineActivity))
                    {
                        // Access the textblock element of template and change it too
                        var txt = (TextBlock)selectedObjUCML.Template.FindName("Description", selectedObjUCML);
                        if (txt != null)
                            txt.Text = txt.Text.Split('(')[0] + " (" + textBox.Text + "%)";
                        txt.Width = Util.MeasureString(txt.Text, txt).Width;
                    }
                    else
                    {
                        // Access the textblock element of template and change it too
                        var txt = (TextBlock)selectedObjUCML.Template.FindName("Percentage", selectedObjUCML);
                        if (txt != null && !selectedObjUCML.GetType().Name.Equals(EnumTemplates.TempQuantityCircle))
                        {
                            txt.Text = textBox.Text + "%";
                        }
                        else
                        {
                            QuantityCircle lqc = selectedObjUCML as QuantityCircle;
                            if (lqc.iterationProperty == "Iteration")
                            {
                                txt.Text = textBox.Text;
                            }
                            else
                            {
                                txt.Text = textBox.Text + "%";
                            }
                            lqc.Percentage = double.Parse(txt.Text.Trim(new char[] { '%' }));
                        }
                    }
                }
                else if (textBox.Tag.ToString().Contains("_PercentageUser") && selectedObjUCML.GetType() != typeof(DescriptionLineUser))
                {
                    //Get user name and user object
                    String userName = textBox.Tag.ToString().Substring(0, textBox.Tag.ToString().LastIndexOf("_PercentageUser"));
                    UcmlObject obj = GetUserByName(userName);
                    //Set correct values in existent object
                    if (textBox.Text.Equals(""))
                    {
                        textBox.Text = "0";
                    }

                    string value = textBox.Text.Replace("%", "");

                    //If value isn't empty ,Update actors dictionary
                    if (value != string.Empty)
                    {
                        selectedObjUCML.myUsers[obj] = Convert.ToInt32(value);
                    }
                    else
                    {
                        // Access the textblock element of template and change it too
                        textBox.Text = value + "0%";
                    }
                }
                else
                {
                    try
                    {
                        var prop = selectedObjUCML.listNewProp[(Int32)textBox.Tag];
                        prop.value = textBox.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro!" + ex.Message);
                    }
                }
            }
        }

        private void LoadDefaultUcmlObjectsProperties()
        {
            // Create a new row at the Property Grid
            gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
            // Create a label for the property name
            var labelName = new TextBlock { Text = "Name", Margin = new System.Windows.Thickness(1), Padding = new System.Windows.Thickness(3) };
            Grid.SetRow(labelName, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(labelName, 0);
            // Add label as Property Grid child
            gridProp.Children.Add(labelName);

            // Create a textBox for the property name value
            var valueName = new TextBlock { Margin = new System.Windows.Thickness(1), Tag = "Name", TextAlignment = TextAlignment.Center };
            // Verify property value, if it already has a value set it
            valueName.Text = selectedObjUCML.UcmlName;

            Grid.SetRow(valueName, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(valueName, 1);
            // Add textBox as Property Grid child
            gridProp.Children.Add(valueName);

            // Create a new row at the Property Grid
            gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
            // Create a label for the property name
            var labelDescription = new TextBlock { Text = "Description", Margin = new System.Windows.Thickness(1), Padding = new System.Windows.Thickness(3) };
            Grid.SetRow(labelDescription, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(labelDescription, 0);
            // Add label as Property Grid child
            gridProp.Children.Add(labelDescription);

            // Create a textBox for the property value
            var valueDescrip = new TextBox { Margin = new System.Windows.Thickness(1), Tag = "Description", TextAlignment = TextAlignment.Right };
            // Verify property value, if it already has a value set it
            if (selectedObjUCML.Description != null)
                valueDescrip.Text = selectedObjUCML.Description;
            // Associate the event-handling method with the textChange event
            valueDescrip.TextChanged += new TextChangedEventHandler(PropertiesTextChanged);

            Grid.SetRow(valueDescrip, gridProp.RowDefinitions.Count - 1);
            Grid.SetColumn(valueDescrip, 1);
            // Add textBox as Property Grid child
            gridProp.Children.Add(valueDescrip);


            if (selectedObjUCML.GetType() != typeof(DescriptionLineUser)  && selectedObjUCML.GetType() != typeof(Condition)
                && selectedObjUCML.GetType() != typeof(Branch) && selectedObjUCML.GetType() != typeof(Merge))
            {
                // Create a new row at the Property Grid
                gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                // Create a label for the property name
                var labelAddUser = new TextBlock { Text = "Add user", Padding = new System.Windows.Thickness(3) };
                Grid.SetRow(labelAddUser, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(labelAddUser, 0);
                // Add label as Property Grid child
                gridProp.Children.Add(labelAddUser);
                // Create new comboBox 
                UserComboBox = new ComboBox { Margin = new System.Windows.Thickness(1) };

                foreach (var userName in listUsers)
                {
                    bool aux = true;
                    foreach (var objName in selectedObjUCML.myUsers.Keys)
                    {
                        if (objName == userName)
                        {
                            aux = false;
                            break;
                        }
                    }
                    if (aux)
                    {
                        UserComboBox.Items.Add(userName.Description);
                    }
                }
                // Associate the event-handling method with the selectedIndexChanged event. 
                UserComboBox.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxUserChanged);

                Grid.SetRow(UserComboBox, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(UserComboBox, 1);
                // Add comboBox as Property Grid child
                gridProp.Children.Add(UserComboBox);

                InsertNewUserAtGrid();
            }

            if (selectedObjUCML.GetType().Name.Equals(EnumTemplates.TempQuantityCircle))
            {
                // Create a new row at the Property Grid
                gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                // Create a label for the property name
                var labelValuesType = new TextBlock { Text = "Value type", Padding = new System.Windows.Thickness(3) };
                Grid.SetRow(labelValuesType, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(labelValuesType, 0);
                // Add label as Property Grid child
                gridProp.Children.Add(labelValuesType);
                // Create new comboBox 
                valueTypeBox = new ComboBox { Margin = new System.Windows.Thickness(1) };

                valueTypeBox.Items.Add("Iteration");
                valueTypeBox.Items.Add("Percentage");

                for (int i = 0; i < valueTypeBox.Items.Count; i++)
                {
                    if ((selectedObjUCML as QuantityCircle).iterationProperty == valueTypeBox.Items[i])
                    {
                        valueTypeBox.SelectedIndex = i;
                    }
                }

                // Associate the event-handling method with the selectedIndexChanged event. 
                valueTypeBox.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxValueTypeChanged);

                Grid.SetRow(valueTypeBox, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(valueTypeBox, 1);
                // Add comboBox as Property Grid child
                gridProp.Children.Add(valueTypeBox);

            }

            // This property belongs just to Quantity Circle, DescriptionLine, ExitPath and Loop
            if (Util.HasPercentageProperty(selectedObjUCML))
            {
                // Create a new row at the Property Grid
                gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                // Create a label for the property name
                var labelPercentage = new TextBlock { Text = "Value", Margin = new System.Windows.Thickness(1), Padding = new System.Windows.Thickness(3) };
                Grid.SetRow(labelPercentage, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(labelPercentage, 0);
                // Add label as Property Grid child
                gridProp.Children.Add(labelPercentage);

                // Create a textBox for the property value
                var valuePercentage = new TextBox { Margin = new System.Windows.Thickness(1), Tag = "Percentage", TextAlignment = TextAlignment.Right };

                // Verify property value, if it already has a value set it
                if (selectedObjUCML.Description != null)
                {
                    switch (selectedObjUCML.TemplateName)
                    {
                        case EnumTemplates.TempQuantityCircle:
                            valuePercentage.Text = ((QuantityCircle)selectedObjUCML).Percentage.ToString();
                            break;
                        case EnumTemplates.TempLoop:
                            valuePercentage.Text = ((Loop)selectedObjUCML).Percentage.ToString();
                            break;
                        case EnumTemplates.TempExitPath:
                            valuePercentage.Text = ((ExitPath)selectedObjUCML).Percentage.ToString();
                            break;
                        case EnumTemplates.TempDescriptionLineActivity:
                            valuePercentage.Text = Convert.ToString(((DescriptionLineActivity)selectedObjUCML).Percentage.ToString());
                            break;
                        case EnumTemplates.TempDescriptionLineUser:
                            valuePercentage.Text = Convert.ToString(((DescriptionLineUser)selectedObjUCML).Percentage.ToString());
                            break;
                    }
                }

                // Associate the event-handling method with the textChange event
                valuePercentage.TextChanged += new TextChangedEventHandler(PropertiesTextChanged);

                Grid.SetRow(valuePercentage, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(valuePercentage, 1);
                // Add textBox as Property Grid child
                gridProp.Children.Add(valuePercentage);
            }

            #region LoadColorCombo


            if (!(selectedObjUCML.TemplateName.Equals(EnumTemplates.TempSyncPoint) ||
                selectedObjUCML.TemplateName.Equals(EnumTemplates.TempCondition)))
            {
                // Create a new row at the Property Grid
                gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });
                // Create a label for the property name
                var labelColor = new TextBlock { Text = "Color", Padding = new System.Windows.Thickness(3) };
                Grid.SetRow(labelColor, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(labelColor, 0);
                // Add label as Property Grid child
                gridProp.Children.Add(labelColor);
                // Create new comboBox 
                colorComboBox = new ComboBox { Margin = new System.Windows.Thickness(1) };
                // Load all colors
                foreach (System.Reflection.PropertyInfo prop in typeof(Colors).GetProperties())
                {
                    if (prop.PropertyType.FullName == "System.Windows.Media.Color")
                        colorComboBox.Items.Add(prop.Name);
                }

                //colorComboBox.SelectedItem = (selectedObjUCML.myColor as SolidColorBrush).Color;

                for (int i = 0; i < colorComboBox.Items.Count; i++)
                {
                    if ((selectedObjUCML.myColor as SolidColorBrush) == new BrushConverter().ConvertFromString((string)colorComboBox.Items[i]) as SolidColorBrush)
                    {
                        colorComboBox.SelectedIndex = i;
                    }
                }

                // Associate the event-handling method with the selectedIndexChanged event. 
                colorComboBox.SelectionChanged += new SelectionChangedEventHandler(OnMyComboBoxChanged);

                Grid.SetRow(colorComboBox, gridProp.RowDefinitions.Count - 1);
                Grid.SetColumn(colorComboBox, 1);
                // Add comboBox as Property Grid child
                gridProp.Children.Add(colorComboBox);
            }

            #endregion
        }

        private void ClearGridProperties(bool IsCheckEvent = false)
        {
            // Clear all row definitions and children of the Property Grid 
            gridProp.RowDefinitions.Clear();
            gridProp.Children.Clear();
            // Create a new row at the Property Grid
            gridProp.RowDefinitions.Insert(gridProp.RowDefinitions.Count, new RowDefinition { Height = new GridLength(20) });
            // Create a label for the Name Header
            var headerName = new TextBlock { Text = "Name", Padding = new System.Windows.Thickness(3), HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(headerName, 0);
            Grid.SetColumn(headerName, 0);

            // Add label as Property Grid child
            gridProp.Children.Add(headerName);

            // Create a label for the Value Header
            var headerValue = new TextBlock { Text = "Value", Padding = new System.Windows.Thickness(3), HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(headerValue, 0);
            Grid.SetColumn(headerValue, 1);

            // Add textBox as Property Grid child
            gridProp.Children.Add(headerValue);

            if (!IsCheckEvent)
            {
                // Clear all row definitions and children of the Users Grid 
                gridUsers.RowDefinitions.Clear();
                gridUsers.Children.Clear();
                // Create a new row at the Users Grid
                gridUsers.RowDefinitions.Insert(gridUsers.RowDefinitions.Count, new RowDefinition { Height = new GridLength(20) });
                // Create a label for the User Name Header
                var headerNameUser = new TextBlock { Text = "Name", Padding = new System.Windows.Thickness(3), HorizontalAlignment = HorizontalAlignment.Center };
                Grid.SetRow(headerNameUser, 0);
                Grid.SetColumn(headerNameUser, 1);

                // Add label as Users Grid child
                gridUsers.Children.Add(headerNameUser);

                // Create a label for the Percent Header
                var headerPercent = new TextBlock { Text = "Percent", Padding = new System.Windows.Thickness(3), HorizontalAlignment = HorizontalAlignment.Center };
                Grid.SetRow(headerPercent, 0);
                Grid.SetColumn(headerPercent, 2);

                // Add textBox as Users Grid child
                gridUsers.Children.Add(headerPercent);
            }
        }

        /// <summary>
        /// ---
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private Connection GetConnectionLineObj(Connection connection)
        {
            for (int i = 0; i < ConnectionLineList.Count; i++)
            {
                if (ConnectionLineList[i] == connection)
                    return ConnectionLineList[i];
            }
            return null;
        }

        private void InsertNewUserAtGrid()
        {
            gridUsers.Children.Clear();
            gridUsers.RowDefinitions.Clear();

            if (selectedConnection != null)
            {
                #region Connection selected
                foreach (UcmlObject item in selectedConnection.myUsers.Keys)
                {
                    UcmlObject currentObject = item;
                    gridUsers.RowDefinitions.Insert(gridUsers.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });

                    Button btnDelete = new Button { Tag = item.Description, Margin = new System.Windows.Thickness(1), Height = 15, Width = 15, Name = "Bt", Content = "X", FontSize = 8, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
                    btnDelete.Click += new RoutedEventHandler(RemoveUserAtElement);

                    TextBlock tb = new TextBlock { Text = currentObject.Description, TextAlignment = TextAlignment.Center };

                    TextBox tbox = new TextBox { IsEnabled = true, Text = Convert.ToString(selectedConnection.myUsers[currentObject]) + "%", Tag = currentObject.Description + "_PercentageUser" };

                    tbox.TextChanged += new TextChangedEventHandler(PropertiesTextChanged);

                    Grid.SetRow(btnDelete, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(btnDelete, 0);
                    gridUsers.Children.Add(btnDelete);

                    Grid.SetRow(tb, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(tb, 1);
                    gridUsers.Children.Add(tb);

                    Grid.SetRow(tbox, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(tbox, 2);
                    gridUsers.Children.Add(tbox);
                }
                #endregion
            }
            else
            {
                #region UCML selected

                if (selectedObjUCML.Id == 0)
                    return;

                foreach (UcmlObject item in selectedObjUCML.myUsers.Keys)
                {
                    UcmlObject currentObject = item;
                    gridUsers.RowDefinitions.Insert(gridUsers.RowDefinitions.Count, new RowDefinition { Height = new GridLength(22) });

                    Button btnDelete = new Button { Tag= item.Description, Margin = new System.Windows.Thickness(1), Height = 15, Width = 15, Name = "Bt", Content = "X", FontSize = 8, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center};
                    btnDelete.Click += new RoutedEventHandler(RemoveUserAtElement);
               
                    TextBlock tb = new TextBlock { Text = currentObject.Description, TextAlignment = TextAlignment.Center };

                    TextBox tbox = new TextBox { IsEnabled = true, Text = Convert.ToString(selectedObjUCML.myUsers[currentObject]) +"%", Tag = currentObject.Description+"_PercentageUser" };
                
                    tbox.TextChanged += new TextChangedEventHandler(PropertiesTextChanged);

                    Grid.SetRow(btnDelete, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(btnDelete, 0);
                    gridUsers.Children.Add(btnDelete);

                    Grid.SetRow(tb, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(tb, 1);
                    gridUsers.Children.Add(tb);

                    Grid.SetRow(tbox, gridUsers.RowDefinitions.Count - 1);
                    Grid.SetColumn(tbox, 2);
                    gridUsers.Children.Add(tbox);
                }
                #endregion
            }
        }

        private void RemoveUserAtElement(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button bt = (Button)sender;

                selectedObjUCML.myUsers.Remove(GetUserByName(bt.Tag.ToString()));

                ClearGridProperties();
                LoadDefaultUcmlObjectsProperties();
                LoadAddicionalsProperties();
            }
        }

        #endregion

        #region MenuItens

        private void NewDiagram(object sender, RoutedEventArgs e)
        {
            NewDiagram();
        }

        public bool NewDiagram()
        {
            if (myCanvas.Children.Count > 0)
            {
                if (MessageBox.Show("Do you want to open a new diagram?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (MessageBox.Show("Do you want to save your current diagram?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SaveDiagram();
                    }
                    Clear();
                   
                    return true;
                }
                return false;
            }
            return true;
        }

        private void Clear()
        {
            //We save the actual project and clear all
            List<Loop> loops = new List<Loop>();
            listUsers = new List<UcmlObject>();
            globalId = 1;
            ConnectionLineList = new List<Connection>();
            loops.Clear();

            for (int i = 0; i < myCanvas.Children.Count; i++)
            {
                var item = myCanvas.Children[i];
                if (item.GetType() != typeof(Thumb))
                {
                    myCanvas.Children.RemoveAt(i);
                    i--;
                }
            }
            Canvas.SetLeft(myThumb, 680);
            Canvas.SetTop(myThumb,480);
            myCanvas.Height = 500;
            myCanvas.Width = 700;
        }

        private void SaveDiagram(object sender, RoutedEventArgs e)
        {
            SaveDiagram();
        }

        private void SaveDiagram()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"C:\";
            saveFile.Title = "Save diagram";
            saveFile.CheckPathExists = true;
            saveFile.DefaultExt = "xml";
            saveFile.Filter = "UCML diagrams (*.xml)|*.*";
            saveFile.FilterIndex = 2;
            string validator1 = @"^\w+?(\w*|\d*)$";
            string validator2 = @"^\w+?(\w*|\d*).xml$";

            if (saveFile.ShowDialog() == true)
            {
                if (Regex.Match(saveFile.SafeFileName, validator1).Success || Regex.Match(saveFile.SafeFileName, validator2).Success)
                {
                    ExportXml.ExportDiagramToXml(myCanvas, listLoop, ConnectionLineList, saveFile.FileName, globalId);
                    MessageBox.Show("Diagram saved.");
                }
                else
                {
                    MessageBox.Show("File name invalid.");
                }
            }
        }

        private void ValidateDiagram(object sender, RoutedEventArgs e)
        {
            ValidateDiagram();
        }

        /// <summary>
        /// This method open a xml archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDiagram(object sender, RoutedEventArgs e)
        {
                myGroupBox.Header = "Diagram";
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory = @"C:\";
                openFile.Title = "Open diagram";
                openFile.CheckPathExists = true;
                openFile.DefaultExt = "xml";
                openFile.Filter = "UCML diagrams (*.xml)|*.xml|All files (*.*)|*.*";
                openFile.FilterIndex = 2;
                string validator1 = @"^\w+?(\w*|\d*)$";
                string validator2 = @"^\w+?(\w*|\d*).xml$";

                if (openFile.ShowDialog() == true)
                {
                    Clear();
                    listLoop.Clear();
                    if (Regex.Match(openFile.SafeFileName, validator1).Success || Regex.Match(openFile.SafeFileName, validator2).Success)
                    {
                        listUsers = new List<UcmlObject>();
                        List<Loop> tempListLoop = new List<Loop>();
                        var listTemplate = LoadListOfTemplates();
                        ImportXml.ImportDiagram(ref myCanvas, openFile.FileName.ToString(), ref listIdByType, ref tempListLoop, ref listUsers, listTemplate);
                        ConnectionLineList = ImportXml.connectionLineList;

                        foreach (var connection in ConnectionLineList)
                        {
                            connection.PreviewMouseLeftButtonDown += ObjetctSelected;
                        }

                        globalId = ImportXml.globalId;
                        foreach (var child in myCanvas.Children)
                        {
                            if (child.GetType().BaseType == typeof(UcmlObject))
                            {
                                var obj = (UcmlObject)child;
                                if (obj.Template != null)
                                {
                                    obj.DragDelta += new DragDeltaEventHandler(onDragDelta);
                                    obj.PreviewMouseLeftButtonDown += ObjetctSelected;
                                }
                            }
                            else if (child.GetType() == typeof(Connection))
                            {
                                var obj = (Connection)child;
                                obj.PreviewMouseLeftButtonDown += ObjetctSelected;
                            }
                        }

                        foreach (Loop loop in tempListLoop)
                        {
                            DescriptionLineActivity tempDestiny = Util.FindObjectById(ref myCanvas, loop.IdObjectStart) as DescriptionLineActivity;
                            OptionBox tempOrigin = Util.FindObjectById(ref myCanvas, loop.IdObjectEnd) as OptionBox;

                            if (tempDestiny == null || tempDestiny == null)
                                break;

                            //Create loop
                            Loop l = new Loop(new Point(
                            Canvas.GetLeft(tempDestiny), Canvas.GetTop(tempDestiny) + tempDestiny.ActualHeight), loop.control1, loop.control2,
                            new Point(Canvas.GetLeft(tempOrigin) + tempOrigin.ActualWidth, Canvas.GetTop(tempOrigin)), loop.myColor);
                            l.ObjectStart = tempDestiny;
                            l.ObjectEnd = tempOrigin;
                            l.bezierCurve.Stroke = loop.myColor;
                            l.myColor = loop.myColor;
                            //Update canvas
                            myCanvas.Children.Add(l.bezierCurve);
                            //Set global bCurve with a new bezier
                            this.actualLoop = l;
                            //Add loop
                            listLoop.Add(l);

                            //Get quantity circle
                            QuantityCircle objQuant = (QuantityCircle)FindObjectById(loop.idMyQuantityCircle);

                            l.myQuantityCircle = objQuant;
                            objQuant.LoopReference = l;

                            l.bezierCurve.PreviewMouseLeftButtonDown += ObjetctSelected;

                            //Draw arrow
                            ArrowShape a = new ArrowShape(new Point(Canvas.GetLeft(tempDestiny), Canvas.GetTop(tempDestiny) + tempDestiny.ActualHeight), loop.myColor);
                            l.myArrow = a;
                            objQuant.ChangeColor(loop.myColor, null);
                            myCanvas.Children.Add(a);

                        }
                        myCanvas.Width = ImportXml.canvasWidth;
                        myCanvas.Height = ImportXml.canvasHeight;
                        Canvas.SetLeft(myThumb, myCanvas.Width-20);
                        Canvas.SetTop(myThumb, myCanvas.Height-20);
                        actualLoop = null;
                    }
                    else
                    {
                        MessageBox.Show("File name invalid.");
                    }
                }
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            if (selectedObjUCML != null && selectedObjUCML.Id != 0)
            {
                switch (selectedObjUCML.TemplateName)
                {
                    case EnumTemplates.TempQuantityCircle:
                        copyUcmlObject = new QuantityCircle().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempDescriptionLineActivity:
                        copyUcmlObject = new DescriptionLineActivity().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempSyncPoint:
                        copyUcmlObject = new SyncPoint().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempOptionBox:
                        copyUcmlObject = new OptionBox().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempCondition:
                        copyUcmlObject = new Condition().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempExitPath:
                        copyUcmlObject = new ExitPath().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempMerge:
                        copyUcmlObject = new Merge().Clone(selectedObjUCML);
                        break;
                    case EnumTemplates.TempBranch:
                        copyUcmlObject = new Branch().Clone(selectedObjUCML);
                        break;
                }
                // Add the "onDragDelta" event handler that is common to all objects
                copyUcmlObject.DragDelta += new DragDeltaEventHandler(onDragDelta);
                copyUcmlObject.PreviewMouseLeftButtonDown += ObjetctSelected;
            }
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            if (copyUcmlObject != null && copyUcmlObject.Id != 0)
            {
                copyUcmlObject.Id = globalId;
                globalId++;

                switch (copyUcmlObject.TemplateName)
                {
                    case EnumTemplates.TempQuantityCircle:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempQuantityCircle] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempQuantityCircle + "_" + listIdByType[0];
                        listIdByType[0]++;
                        break;
                    case EnumTemplates.TempDescriptionLineActivity:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempDescriptionLineActivity] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempDescriptionLineActivity + "_" + listIdByType[1];
                        listIdByType[1]++;
                        break;
                    case EnumTemplates.TempSyncPoint:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempSyncPoint] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempSyncPoint + "_" + listIdByType[2];
                        listIdByType[2]++;
                        break;
                    case EnumTemplates.TempOptionBox:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempOptionBox] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempOptionBox + "_" + listIdByType[3];
                        listIdByType[3]++;
                        break;
                    case EnumTemplates.TempCondition:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempCondition] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempCondition + "_" + listIdByType[4];
                        listIdByType[4]++;
                        break;
                    case EnumTemplates.TempExitPath:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempExitPath] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempExitPath + "_" + listIdByType[5];
                        listIdByType[5]++;
                        break;
                    case EnumTemplates.TempMerge:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempMerge] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempMerge + "_" + listIdByType[6];
                        listIdByType[6]++;
                        break;
                    case EnumTemplates.TempBranch:
                        copyUcmlObject.Template = this.Resources[EnumTemplates.TempBranch] as ControlTemplate;
                        copyUcmlObject.UcmlName = EnumTemplates.TempBranch + "_" + listIdByType[7];
                        listIdByType[7]++;
                        break;
                }

                // Calling ApplyTemplate enables us to navigate the visual tree right now (important!)
                copyUcmlObject.ApplyTemplate();

                // Update the layout of the ucml object
                copyUcmlObject.UpdateLayout();

                // Add new object as child of canvas to allow get ucmlObjsct's size from it's template
                myCanvas.Children.Add(copyUcmlObject);
                copyUcmlObject.PosTopX = 0;
                copyUcmlObject.PosTopY = 0;
                // Set position top and left of ucml object on the canvas
                Canvas.SetLeft(copyUcmlObject, copyUcmlObject.PosTopX);
                Canvas.SetTop(copyUcmlObject, copyUcmlObject.PosTopY);
                // Move our thumb to the front to be over the lines
                Canvas.SetZIndex(copyUcmlObject, 1);
                // Update the layout of the ucml object
                copyUcmlObject.UpdateLayout();
            }
            copyUcmlObject = null;
        }

        private void DeleteProperty(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this property?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button bt = (Button)sender;

                // For each object already in the canvas check if the kind of them are the same as the selected object  
                foreach (var child in myCanvas.Children)
                {
                    // Check if the kind of the ucml object is the same as the selected object, if it´s create the same new property for it 
                    if (child.GetType() == selectedObjUCML.GetType())
                    {
                        var c = (UcmlObject)child;
                        // Remove property from ListNewProp
                        c.listNewProp.RemoveAt(Convert.ToInt32(bt.Tag));
                    }
                }

                ClearGridProperties();
                LoadDefaultUcmlObjectsProperties();
                LoadAddicionalsProperties();
            }
        }

        private void DeleteUcmlObject(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void EventKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                Delete();
            }
            else if (e.Key == Key.Escape)
            {
                isLoopInsert = false;
                LoopStarted = false;
                isAddLink = false;
                Mouse.OverrideCursor = null;
                //falta remover loop atual
                if (actualConnection != null)
                {
                    myCanvas.Children.Remove(actualConnection);
                    actualConnection = null;
                    isAddLink = false;
                    linkStarted = false;
                    Mouse.OverrideCursor = null;
                    e.Handled = true;
                }
                else if (actualLoop != null)
                {
                    if (actualLoop.bezierCurve != null)
                    {
                        myCanvas.Children.Remove(actualLoop.bezierCurve);
                        myCanvas.Children.Remove(actualLoop.myArrow);
                        actualLoop = null;
                        LoopStarted = false;
                    }
                }
            }

        }

        /// <summary>
        /// This method print the canvas in the printer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print(object sender, RoutedEventArgs e)
        {
            ValidateDiagram();
            PrintDialog printDlg = new PrintDialog();

            if (printDlg.ShowDialog() != true)
            {
                return;
            }
            printDlg.PrintVisual(myCanvas, "");

        }

        private void NewConnection(object sender, RoutedEventArgs e)
        {
            isAddLink = true;
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void NewLoop(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = null;
            isLoopInsert = true;
        }

        #endregion

        private void DockPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            canvasHeight = myCanvas.ActualHeight;
            canvasWidth = myCanvas.ActualWidth;
        }

        private void myCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            //(selectedObjUCML.myUsers[Convert.ToInt32(cb.Uid)] as UcmlObject).IsSelected = true;

            foreach (var item in gridUsers.Children)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    if ((item as TextBox).Uid == cb.Uid)
                    {
                        (item as TextBox).IsEnabled = true;
                    }
                }
            }

            Util.setPercentage(selectedObjUCML);
            ClearGridProperties(true);
            LoadDefaultUcmlObjectsProperties();
            LoadAddicionalsProperties();
        }

        private void myCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            //(selectedObjUCML.myUsers[Convert.ToInt32(cb.Uid)] as UcmlObject).IsSelected = false;

            foreach (var item in gridUsers.Children)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    if ((item as TextBox).Uid == cb.Uid)
                    {
                        (item as TextBox).IsEnabled = false;
                    }
                }
            }

            Util.setPercentage(selectedObjUCML);
            ClearGridProperties(true);
            LoadDefaultUcmlObjectsProperties();
            LoadAddicionalsProperties();
        }

        private void userPercent_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox tbox = sender as TextBox;
            /*
            if (//Convert.ToDouble(tbox.Text) > (selectedObjUCML.myUsers[Convert.ToInt32(tbox.Uid)] as QuantityCircle).Percentage)
            {
                MessageBox.Show("Percentage greater than the available");
            }
            else
            {
                //selectedObjUCML.myUsers[Convert.ToInt32(tbox.Uid)] as QuantityCircle).Percentage = Convert.ToDouble(tbox.Text);
            }*/
        }

        private object getLoopInformation(bool getBorder = false, bool getLoop = false)
        {
            foreach (var item in myCanvas.Children)
            {
                if (item.GetType() == typeof(StackPanel))
                {
                    StackPanel panelAux = item as StackPanel;
                    if (panelAux.Children.Count > 1)
                    {
                        if (getBorder)
                        {
                            if (panelAux.Children[1].GetType() == typeof(Border))
                            {
                                if ((panelAux.Children[1] as Border).Name.Equals("Circle"))
                                {
                                    return (panelAux.Children[1] as Border);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void ValidateDiagram()
        {
            /*
            string strAlert = "";
            bool isValid = true, hasExitPath = false;
            List<QuantityCircle> users = new List<QuantityCircle>();
            UcmlObject auxObject = new UcmlObject();
            double inputPercentage = 0, outputPercentage = 0;

            foreach (var item in myCanvas.Children)
            {
                if (item.GetType() == typeof(QuantityCircle))
                {
                    inputPercentage += (item as QuantityCircle).Percentage;
                }

                if (item.GetType() == typeof(ExitPath))
                {
                    hasExitPath = true;
                    outputPercentage += (item as ExitPath).Percentage;
                }
                
                switch (item.GetType().Name)
                {
                    case EnumTemplates.TempSyncPoint:
                        users = ConvertToUsersList((item as SyncPoint).myUsers);
                        foreach (int id in (item as SyncPoint).IdObjectStarLines)
                        {
                            auxObject = FindObjectById(id);
                            if (auxObject != null)
                            {
                                foreach (QuantityCircle user in users)
                                {
                                    foreach (var myUser in auxObject.myUsers)
                                    {
                                        QuantityCircle auxUser = myUser as QuantityCircle;
                                        if (auxUser.IsSelected && (auxUser.Id == user.Id))
                                        {
                                            user.UsedPercentage += auxUser.Percentage;
                                            user.UsedAtDestination = true;
                                            break;
                                        }
                                        else
                                        {
                                            user.UsedAtDestination = false;
                                        }
                                    }
                                    if (user.UsedPercentage > user.Percentage && user.UsedAtDestination)
                                    {
                                        //MessageBox.Show("ERROR! Percentage exceeded at " + auxObject.UcmlName + " - You have " + user.Percentage + "% and used " + user.UsedPercentage + "%");
                                        strAlert += "\nERROR! Percentage exceeded at " + auxObject.Description + " - You have " + user.Percentage + "% and used " + user.UsedPercentage + "%";
                                        isValid = false;
                                    }
                                }
                            }
                        }
                        ZeroUsedPercentage(users);
                        break;

                    case EnumTemplates.TempBranch:
                        users = ConvertToUsersList((item as Branch).myUsers);
                        foreach (int id in (item as Branch).IdObjectStartLine)
                        {
                            auxObject = FindObjectById(id);
                            if (auxObject != null)
                            {
                                foreach (QuantityCircle user in users)
                                {
                                    foreach (var myUser in auxObject.myUsers)
                                    {
                                        QuantityCircle auxUser = myUser as QuantityCircle;
                                        if (auxUser.IsSelected && (auxUser.Id == user.Id))
                                        {
                                            user.UsedPercentage += auxUser.Percentage;
                                            user.UsedAtDestination = true;
                                            break;
                                        }
                                        else
                                        {
                                            user.UsedAtDestination = false;
                                        }
                                    }
                                    if (user.UsedPercentage > user.Percentage && user.UsedAtDestination)
                                    {
                                        //MessageBox.Show("ERROR! Percentage exceeded at " + auxObject.UcmlName + " - You have " + user.Percentage + "% and used " + user.UsedPercentage + "%");
                                        strAlert += "\nERROR! Percentage exceeded at " + auxObject.Description + " - You have " + user.Percentage + "% and used " + user.UsedPercentage + "%";
                                        isValid = false;
                                    }
                                }
                            }
                        }
                        ZeroUsedPercentage(users);
                        break;

                    case EnumTemplates.TempDescriptionLineActivity:
                        DescriptionLineActivity descLineObj = item as DescriptionLineActivity;
                        if (descLineObj.StartLine.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Description Line (" + descLineObj.Description + ") doesn't have a connection.";
                            isValid = false;
                        }
                        break;
                    case EnumTemplates.TempOptionBox:
                        OptionBox optionBoxObj = item as OptionBox;
                        if (optionBoxObj.StartLine.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Description Line (" + optionBoxObj.Description + ") doesn't have a connection.";
                            isValid = false;
                        }
                        break;
                    case EnumTemplates.TempQuantityCircle:
                        QuantityCircle quantCircObj = item as QuantityCircle;
                        if (quantCircObj.StartLine.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Branch (" + quantCircObj.Description + ") doesn't have a connection.";
                            isValid = false;
                        }
                        break;
                    case EnumTemplates.TempMerge:
                        Merge mergeObj = item as Merge;
                        if (mergeObj.StartLine.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Merge (" + mergeObj.Description + ") doesn't have a connection.";
                            isValid = false;
                        }
                        break;
                    case EnumTemplates.TempCondition:
                        Condition conditionObj = item as Condition;
                        if (conditionObj.StartLineNo.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Branch (" + conditionObj.Description + ") doesn't have a NO connection.";
                            isValid = false;
                        }

                        if (conditionObj.StartLineYes.endPoint.X == 0.0)
                        {
                            strAlert += "\nERROR! Branch (" + conditionObj.Description + ") doesn't have a YES connection.";
                            isValid = false;
                        }
                        break;
                }
            }

                if (!hasExitPath)
                {
                    strAlert += "\nERROR! Diagram should have at least 1 Exit Path.";
                    isValid = false;
                }

                if (inputPercentage != outputPercentage && hasExitPath)
                {
                    isValid = false;
                    //MessageBox.Show("ERROR! Your input % is different from your output % - Check your objects");
                    strAlert += "\nERROR! Your input % is different from your output % - Check your objects";
                }

                if (isValid)
                {
                    //MessageBox.Show("SUCCESS! Diagram doesn't have errors.");
                    strAlert += "SUCCESS! Diagram doesn't have errors.";
                }
                MessageBox.Show(strAlert);
                 * 
            }*/
        }

        private UcmlObject FindObjectById(int id)
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

        private List<QuantityCircle> ConvertToUsersList(List<object> genericUsers)
        {
            List<QuantityCircle> users = new List<QuantityCircle>();
            foreach (var item in genericUsers)
            {
                users.Add(item as QuantityCircle);
            }
            return users;
        }

        private void ZeroUsedPercentage(List<QuantityCircle> users)
        {
            foreach (QuantityCircle item in users)
            {
                item.UsedPercentage = 0;
            }
        }

        private ControlTemplate[] LoadListOfTemplates()
        {
            var listTemplate = new ControlTemplate[10];
            listTemplate[0] = this.Resources[EnumTemplates.TempQuantityCircle] as ControlTemplate;
            listTemplate[1] = this.Resources[EnumTemplates.TempDescriptionLineActivity] as ControlTemplate;
            listTemplate[2] = this.Resources[EnumTemplates.TempSyncPoint] as ControlTemplate;
            listTemplate[3] = this.Resources[EnumTemplates.TempOptionBox] as ControlTemplate;
            listTemplate[4] = this.Resources[EnumTemplates.TempCondition] as ControlTemplate;
            listTemplate[5] = this.Resources[EnumTemplates.TempExitPath] as ControlTemplate;
            listTemplate[6] = this.Resources[EnumTemplates.TempMerge] as ControlTemplate;
            listTemplate[7] = this.Resources[EnumTemplates.TempBranch] as ControlTemplate;
            listTemplate[8] = this.Resources[EnumTemplates.TempQuantityCircle] as ControlTemplate;
            listTemplate[9] = this.Resources[EnumTemplates.TempDescriptionLineUser] as ControlTemplate;

            return listTemplate;
        }

        private void Delete()
        {
            if (selectedObjUCML.Id != 0)
            {
                if (myCanvas.Children.Count > 0)
                { 
                    if (MessageBox.Show("Do you want to delete this ucml object?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ClearLoops(selectedObjUCML);
                        FindAndDeleteConnection(selectedObjUCML.Id);
                        myCanvas.Children.Remove(selectedObjUCML);
                        ClearGridProperties();
                    }
                }
            }
            if (selectedConnection != null)
            {
                if (selectedConnection.GetType() == typeof(Connection))
                {
                    if (MessageBox.Show("Do you want to delete this connection?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        FindAndDeleteConnection();
                    }
                }
            }
        }

        private void FindAndDeleteConnection(int id)
        {
            for (int i = 0; i < ConnectionLineList.Count; i++)
            {
                if (ConnectionLineList[i].elementoOrigId == id ||
                   ConnectionLineList[i].elementoDestId == id)
                {
                    myCanvas.Children.Remove(ConnectionLineList[i]);
                    Connection connection = ConnectionLineList[i];
                    DeleteReferences(connection);
                    ConnectionLineList.RemoveAt(i);
                    ConnectionLineList.Remove(connection);
                    connection = null;
                    i--;
                }
            }
        }

        private void FindAndDeleteConnection()
        {
            for (int i = 0; i < ConnectionLineList.Count; i++)
            {
                if (ConnectionLineList[i] == selectedConnection)
                {
                    myCanvas.Children.Remove(selectedConnection);
                    Connection connection = ConnectionLineList[i];
                    ConnectionLineList.RemoveAt(i);
                    DeleteReferences(connection);
                    ConnectionLineList.Remove(connection);
                    connection = new Connection();
                }
            }
        }

        private void DeleteReferences(Connection connection)
        {
            #region
            foreach (var item in myCanvas.Children)
            {
                if (item.GetType().BaseType == typeof(UcmlObject))
                {
                    UcmlObject ucmlObj = item as UcmlObject;

                    switch (ucmlObj.TemplateName)
                    {
                        case EnumTemplates.TempSyncPoint:
                            SyncPoint sp = ucmlObj as SyncPoint;

                            for (int i = 0; i < sp.EndLines.Count; i++)
                            {
                                Connection c = sp.EndLines[i];
                                if (c == connection)
                                {
                                    sp.EndLines[i] = new Connection();
                                    break;
                                }
                            }
                            for (int i = 0; i < sp.StartLines.Count; i++)
                            {
                                Connection c = sp.StartLines[i];
                                if (c == connection)
                                {
                                    sp.StartLines[i] = new Connection();
                                    break;
                                }
                            }
                            break;
                        case EnumTemplates.TempBranch:
                            Branch b = ucmlObj as Branch;
                            for (int i = 0; i < b.StartLines.Count; i++)
                            {
                                Connection c = b.StartLines[i];
                                if (c == connection)
                                {
                                    b.StartLines[i] = new Connection();
                                    break;
                                }
                            }

                            if (b.EndLine == connection)
                            {
                                b.EndLine = new Connection();
                            }

                            break;
                        case EnumTemplates.TempCondition:
                            Condition co = ucmlObj as Condition;

                            if (co.StartLineNo == connection)
                            {
                                co.StartLineNo = new Connection();
                            }
                            else if (co.StartLineYes == connection)
                            {
                                co.StartLineYes = new Connection();
                            }
                            else if (co.EndLine == connection)
                            {
                                co.EndLine = connection;
                            }

                            break;
                        case EnumTemplates.TempDescriptionLineActivity:
                            DescriptionLineActivity dl = ucmlObj as DescriptionLineActivity;
                            if (connection == dl.StartLine)
                                dl.StartLine = new Connection();
                            else if (connection == dl.EndLine)
                                dl.EndLine = new Connection();
                            break;
                        case EnumTemplates.TempDescriptionLineUser:
                            DescriptionLineUser descriptionLineUser = ucmlObj as DescriptionLineUser;
                            if (connection == descriptionLineUser.StartLine)
                                descriptionLineUser.StartLine = new Connection();
                            break;
                        case EnumTemplates.TempExitPath:
                            ExitPath ep = ucmlObj as ExitPath;
                            if (connection == ep.StartLine)
                                ep.StartLine = new Connection();
                            else if (connection == ep.EndLine)
                                ep.EndLine = new Connection();
                            break;
                        case EnumTemplates.TempMerge:
                            Merge m = ucmlObj as Merge;

                            for (int i = 0; i < m.EndLines.Count; i++)
                            {
                                Connection c = m.EndLines[i];
                                if (c == connection)
                                {
                                    m.EndLines[i] = new Connection();
                                    break;
                                }
                            }
                            if (m.StartLine == connection)
                            {
                                m.StartLine = new Connection();
                            }

                            break;
                        case EnumTemplates.TempOptionBox:
                            OptionBox ob = ucmlObj as OptionBox;
                            if (connection == ob.StartLine)
                                ob.StartLine = new Connection();
                            else if (connection == ob.EndLine)
                                ob.EndLine = new Connection();
                            break;
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// This method return true if a total sum users percentage value is less then or
        /// equals 100, otherwise return false.
        /// </summary>
        /// <param name="percentageValue">Percentage value parameter</param>
        /// <returns></returns>
        private Boolean UserPercentageValidate(Double percentageValue)
        {
            Double percentageSum = percentageValue;
            for (int i = 0; i < myCanvas.Children.Count; i++)
            {
                //If element is DescriptionLineUserType
                if (myCanvas.Children[i].GetType().Name.Equals(EnumTemplates.TempDescriptionLineUser) &&
                    selectedObjUCML != myCanvas.Children[i])
                {
                    DescriptionLineUser dlu = myCanvas.Children[i] as DescriptionLineUser;
                    percentageSum += dlu.Percentage;
                }
            }
            if (percentageSum > 100)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Zoom event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                sc.ScaleX *= 1.1;
                sc.ScaleY *= 1.1;
                myCanvas.RenderTransform = sc;
            }
            else
            {
                sc.ScaleX /= 1.1;
                sc.ScaleY /= 1.1;
                myCanvas.RenderTransform = sc;
            }
        }
    }
}