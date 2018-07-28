using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShapeConnectors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapeConnectors
{
    public class Validadores
    {

        public Validadores() { }

        /*
         * This method validate connection between two ucml objects. 
         * Get the origin and destination objects, validate the start point and end point for each object.
         * Parameters are type of each object, the line that will be connected and the two ucml objects.
         * */
        public static bool ValidaConexao(string tipoObjetoOrigem,
                                         string tipoObjetoDestino,
                                         LineGeometry link,
                                         UcmlObject origem,
                                         UcmlObject destino,
                                         bool YesNoLine,
                                         List<object> Users
                                         )
        {
            // Get the color from origin object
            SolidColorBrush color = origem.myColor;

            if (origem.GetType() == typeof(SyncPoint) || origem.GetType() == typeof(Branch) || origem.GetType() == typeof(Condition))
            {
                destino.isAbleUserSelection = true;
            }

            // Validate origin object
            # region Valida Objetos de Origem

            // Check each type of object and validate each one in particular
            switch (tipoObjetoOrigem)
            {
                case EnumTemplates.TempQuantityCircle:
                    QuantityCircle AuxQuantityCircle = origem as QuantityCircle;
                    // Check if object already has a initial line
                    if (AuxQuantityCircle.StartLine.StartPoint.X == 0.0
                        && AuxQuantityCircle.StartLine.StartPoint.Y == 0.0)
                    {
                        AuxQuantityCircle.StartLine = link;
                        AuxQuantityCircle.IdObjectStarLine = destino.Id;
                        AuxQuantityCircle.UpdateLayout();
                        link.StartPoint = new Point(Canvas.GetLeft(AuxQuantityCircle) + AuxQuantityCircle.ActualWidth / 2,
                                                    Canvas.GetTop(AuxQuantityCircle) + AuxQuantityCircle.ActualHeight / 2
                                                    );
                    }
                    else
                    {
                        MessageBox.Show("Quantity Circle already has a initial line.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempBranch:
                    Branch AuxBranch = origem as Branch;
                    AuxBranch.StartLines.Add(link);
                    AuxBranch.IdObjectStartLine.Add(destino.Id);
                    AuxBranch.UpdateLayout();
                    link.StartPoint = new Point(Canvas.GetLeft(AuxBranch) + AuxBranch.ActualWidth / 2,
                                              Canvas.GetTop(AuxBranch) + AuxBranch.ActualHeight / 2);
                    break;
                case EnumTemplates.TempCondition:
                    Condition AuxCondition = origem as Condition;
                    // Check if object already has a initial line
                    if (YesNoLine)
                    {
                        if (AuxCondition.StartLineYes.StartPoint.X == 0.0
                        && AuxCondition.StartLineYes.StartPoint.Y == 0.0)
                        {
                            AuxCondition.StartLineYes = link;
                            AuxCondition.IdObjectStarLineYes = destino.Id;
                            AuxCondition.UpdateLayout();
                            link.StartPoint = new Point((Canvas.GetLeft(AuxCondition) + AuxCondition.ActualWidth / 2) + 21,
                                              (Canvas.GetTop(AuxCondition) + AuxCondition.ActualHeight / 2) + 1);
                        }
                        else
                        {
                            MessageBox.Show("Condition can only have one YES connection.");
                            return false;
                        }
                    }
                    else
                    {
                        if (AuxCondition.StartLineNo.StartPoint.X == 0.0
                        && AuxCondition.StartLineNo.StartPoint.Y == 0.0)
                        {
                            AuxCondition.StartLineNo = link;
                            AuxCondition.IdObjectStarLineNo = destino.Id;
                            AuxCondition.UpdateLayout();
                            link.StartPoint = new Point((Canvas.GetLeft(AuxCondition) + AuxCondition.ActualWidth / 2) - 6,
                                              (Canvas.GetTop(AuxCondition) + AuxCondition.ActualHeight / 2) + 28);
                        }
                        else
                        {
                            MessageBox.Show("Condition can only have one NO connection.");
                            return false;
                        }
                    }


                    break;
                case EnumTemplates.TempDescriptionLineActivityType:
                    DescriptionLineActivityType AuxDescriptionLine = origem as DescriptionLineActivityType;
                    // Check if object already has a initial line
                    if (AuxDescriptionLine.StartLine.StartPoint.X == 0.0
                        && AuxDescriptionLine.StartLine.StartPoint.Y == 0.0)
                    {
                        AuxDescriptionLine.StartLine = link;
                        AuxDescriptionLine.IdObjectStarLine = destino.Id;
                        AuxDescriptionLine.UpdateLayout();
                        link.StartPoint = new Point((Canvas.GetLeft(AuxDescriptionLine) + AuxDescriptionLine.ActualWidth) - 3,
                                                  (Canvas.GetTop(AuxDescriptionLine) + AuxDescriptionLine.ActualHeight) - 1);
                    }
                    else
                    {
                        MessageBox.Show("Description Line can only have one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempDescriptionLineUserType:
                    DescriptionLineUserType descriptionLine = origem as DescriptionLineUserType;
                    // Check if object already has a initial line
                    if (descriptionLine.StartLine.StartPoint.X == 0.0
                        && descriptionLine.StartLine.StartPoint.Y == 0.0)
                    {
                        descriptionLine.StartLine = link;
                        descriptionLine.IdObjectStarLine = destino.Id;
                        descriptionLine.UpdateLayout();
                        link.StartPoint = new Point((Canvas.GetLeft(descriptionLine) + descriptionLine.ActualWidth) - 3,
                                                  (Canvas.GetTop(descriptionLine) + descriptionLine.ActualHeight/1.4));
                    }
                    else
                    {
                        MessageBox.Show("Description Line can only have one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempExitPath:
                    MessageBox.Show("Exit Path can't connect with another objects.");
                    return false;
                case EnumTemplates.TempMerge:
                    Merge AuxMerge = origem as Merge;
                    // Check if object already has a initial line
                    if (AuxMerge.StartLine.StartPoint.X == 0.0
                        && AuxMerge.StartLine.StartPoint.Y == 0.0)
                    {
                        AuxMerge.StartLine = link;
                        AuxMerge.IdObjectStarLine = destino.Id;
                        AuxMerge.UpdateLayout();
                        link.StartPoint = new Point(Canvas.GetLeft(AuxMerge) + AuxMerge.ActualWidth / 2,
                                              Canvas.GetTop(AuxMerge) + AuxMerge.ActualHeight / 2);
                    }
                    else
                    {
                        MessageBox.Show("Merge can only have one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempOptionBox:
                    OptionBox AuxOptionBox = origem as OptionBox;
                    // Check if object already has a initial line
                    if (AuxOptionBox.StartLine.StartPoint.X == 0.0
                        && AuxOptionBox.StartLine.StartPoint.Y == 0.0)
                    {
                        AuxOptionBox.StartLine = link;
                        AuxOptionBox.IdObjectStarLine = destino.Id;
                        AuxOptionBox.UpdateLayout();
                        link.StartPoint = new Point((Canvas.GetLeft(AuxOptionBox) + AuxOptionBox.ActualWidth) - 1,
                                                  Canvas.GetTop(AuxOptionBox));
                    }
                    else
                    {
                        MessageBox.Show("Option Box can only have one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempSyncPoint:
                    SyncPoint AuxSyncPoint = origem as SyncPoint;
                    AuxSyncPoint.StartLines.Add(link);
                    AuxSyncPoint.IdObjectStarLines.Add(destino.Id);
                    AuxSyncPoint.UpdateLayout();
                    link.StartPoint = new Point(Canvas.GetLeft(AuxSyncPoint) + AuxSyncPoint.ActualWidth / 2,
                                              Canvas.GetTop(AuxSyncPoint) + AuxSyncPoint.ActualHeight / 2);
                    break;
            }
            #endregion

            // Set users from source to destiny
            List<object> listAux = verifyList(Users);
            bool syncPointUserSelection = false;
            if (origem.GetType() == typeof(SyncPoint))
	        {
		        if ((origem as SyncPoint).isAbleUserSelection)
	            {
		            syncPointUserSelection = true;
	            }
	        }

            if ((listAux.Count > 0 && !destino.isAbleUserSelection) || syncPointUserSelection)
            {
                destino.myUsers.AddRange(listAux);
                removeDuplicates(destino.myUsers);
            }
            else
            {
                List<QuantityCircle> list = new List<QuantityCircle>();
                foreach (var item in Users)
                {
                    QuantityCircle qcAux = new QuantityCircle();
                    QuantityCircle aux = item as QuantityCircle;
                    qcAux.IsSelected = false;
                    qcAux.Percentage = aux.Percentage;
                    qcAux.currentPercentage = aux.currentPercentage;
                    qcAux.isAbleUserSelection = aux.isAbleUserSelection;
                    qcAux.Description = aux.Description;
                    qcAux.Id = aux.Id;
                    qcAux.myColor = aux.myColor;

                    list.Add(qcAux);
                }
                
                destino.myUsers.AddRange(list);
                deleteDuplicateUsers(destino.myUsers);
            }

            if (destino.myUsers.Count > 1)
            {
                destino.myColor = Brushes.Black;
            }
            else if (destino.myUsers.Count == 1)
            {
                destino.myColor = (destino.myUsers[0] as UcmlObject).myColor;
            }

            // Validate destination object
            #region Valida Objetos de Destino

            // Check each type of object and validate each one in particular
            switch (tipoObjetoDestino)
            {
                case EnumTemplates.TempQuantityCircle:
                    MessageBox.Show("Quantity Circle can't receive connection.");
                    return false;
                case EnumTemplates.TempBranch:
                    Branch AuxBranch = destino as Branch;
                    // Check if object already has a end line
                    if (AuxBranch.EndLine.EndPoint.X == 0.0
                        && AuxBranch.EndLine.EndPoint.Y == 0.0)
                    {
                        AuxBranch.EndLine = link;
                        AuxBranch.IdObjectEndLine = origem.Id;
                        AuxBranch.UpdateLayout();
                        link.EndPoint = new Point(Canvas.GetLeft(AuxBranch) + AuxBranch.ActualWidth / 2,
                                              Canvas.GetTop(AuxBranch) + AuxBranch.ActualHeight / 2);
                    }
                    else
                    {
                        MessageBox.Show("Branch can only receive one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempCondition:
                    Condition AuxCondition = destino as Condition;
                    // Check if object already has a end line
                    if (AuxCondition.EndLine.EndPoint.X == 0.0
                        && AuxCondition.EndLine.EndPoint.Y == 0.0)
                    {
                        AuxCondition.EndLine = link;
                        AuxCondition.IdObjectEndLine = origem.Id;
                        AuxCondition.UpdateLayout();
                        link.EndPoint = new Point(Canvas.GetLeft(AuxCondition) + AuxCondition.ActualWidth / 2,
                                              Canvas.GetTop(AuxCondition) + AuxCondition.ActualHeight / 2);
                    }
                    else
                    {
                        MessageBox.Show("Condition can only receive one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempDescriptionLineActivityType:
                    DescriptionLineActivityType AuxDescriptionLine = destino as DescriptionLineActivityType;

                    // Check if object already has a end line
                    if (AuxDescriptionLine.EndLine.EndPoint.X == 0.0
                        && AuxDescriptionLine.EndLine.EndPoint.Y == 0.0)
                    {
                        AuxDescriptionLine.EndLine = link;
                        AuxDescriptionLine.IdObjectEndLine = origem.Id;
                        AuxDescriptionLine.UpdateLayout();
                        link.EndPoint = new Point(Canvas.GetLeft(AuxDescriptionLine) + 2,
                                                  (Canvas.GetTop(AuxDescriptionLine) + AuxDescriptionLine.ActualHeight) - 2);
                    }
                    else
                    {
                        MessageBox.Show("Description Line can only receive one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempExitPath:
                    ExitPath AuxExitPath = destino as ExitPath;
                    // Check if object already has a end line
                    if (AuxExitPath.EndLine.EndPoint.X == 0.0
                        && AuxExitPath.EndLine.EndPoint.Y == 0.0)
                    {
                        AuxExitPath.EndLine = link;
                        AuxExitPath.IdObjectEndLine = origem.Id;
                        AuxExitPath.UpdateLayout();
                        link.EndPoint = new Point((Canvas.GetLeft(AuxExitPath) + (AuxExitPath.ActualWidth / 2)) - 14,
                                                  Canvas.GetTop(AuxExitPath));
                    }
                    else
                    {
                        MessageBox.Show("Exit Path can only receive one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempMerge:
                    Merge AuxMerge = destino as Merge;
                    AuxMerge.EndLines.Add(link);
                    AuxMerge.IdObjectEndLine.Add(origem.Id);
                    AuxMerge.UpdateLayout();
                    link.EndPoint = new Point(Canvas.GetLeft(AuxMerge) + AuxMerge.ActualWidth / 2,
                                              Canvas.GetTop(AuxMerge) + AuxMerge.ActualHeight / 2);
                    break;
                case EnumTemplates.TempOptionBox:
                    OptionBox AuxOptionBox = destino as OptionBox;
                    // Check if object already has a end line
                    if (AuxOptionBox.EndLine.EndPoint.X == 0.0
                        && AuxOptionBox.EndLine.EndPoint.Y == 0.0)
                    {
                        AuxOptionBox.EndLine = link;
                        AuxOptionBox.IdObjectEndLine = origem.Id;
                        AuxOptionBox.UpdateLayout();
                        link.EndPoint = new Point(Canvas.GetLeft(AuxOptionBox) + 2,
                                                  Canvas.GetTop(AuxOptionBox) + 1);
                    }
                    else
                    {
                        MessageBox.Show("Option Box can only receive one connection.");
                        return false;
                    }
                    break;
                case EnumTemplates.TempSyncPoint:
                    SyncPoint AuxSyncPoint = destino as SyncPoint;
                    AuxSyncPoint.EndLines.Add(link);
                    AuxSyncPoint.IdObjectEndLines.Add(origem.Id);
                    AuxSyncPoint.UpdateLayout();
                    link.EndPoint = new Point(Canvas.GetLeft(AuxSyncPoint) + AuxSyncPoint.ActualWidth / 2,
                                              Canvas.GetTop(AuxSyncPoint) + AuxSyncPoint.ActualHeight / 2);
                    break;
            }
            #endregion

            return true;
        }

        public static List<object> verifyList(List<object> users)
        {
            List<object> returnList = new List<object>();

            foreach (var item in users)
            {
                if ((item as QuantityCircle).IsSelected)
                {
                    QuantityCircle newUser = new QuantityCircle();
                    newUser = newUser.Clone(item as QuantityCircle);
                    returnList.Add(newUser);
                }
            }
            return returnList;
        }

        public static bool ValidaDiagram(Canvas myCanvas)
        {
            return true;
        }

        private static void removeDuplicates(List<object> users) 
        {
            int length = users.Count-1;
            int lengthJ = users.Count;
            for (int i = 0; i < length; i++)
            {
                for (int j = i+1; j < lengthJ; j++)
                {
                    if ((users[i] as QuantityCircle).Description.Equals((users[j] as QuantityCircle).Description))
                    {
                        (users[i] as QuantityCircle).Percentage += (users[j] as QuantityCircle).Percentage;
                        users.RemoveAt(j);
                        lengthJ--;
                        j--;
                        length--;
                    }
                }
                lengthJ = users.Count;
            }
        }

        private static void deleteDuplicateUsers(List<object> users)
        {
            int length = users.Count - 1;
            int lengthJ = users.Count;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < lengthJ; j++)
                {
                    if ((users[i] as QuantityCircle).Description.Equals((users[j] as QuantityCircle).Description))
                    {
                        users.RemoveAt(j);
                        lengthJ--;
                        j--;
                        length--;
                    }
                }
                lengthJ = users.Count;
            }
        }
    }
}
