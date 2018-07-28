using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Media;
using System.Globalization;
using System.Windows;

namespace ShapeConnectors
{
    class Util
    {
        public static bool HasPercentageProperty(UcmlObject selectedObjUCML)
        {
            if (selectedObjUCML.TemplateName.Equals(EnumTemplates.TempQuantityCircle) ||
                selectedObjUCML.TemplateName.Equals(EnumTemplates.TempExitPath) ||
                selectedObjUCML.TemplateName.Equals(EnumTemplates.TempLoop) ||
                selectedObjUCML.TemplateName.Equals(EnumTemplates.TempDescriptionLineActivity) ||
                selectedObjUCML.TemplateName.Equals(EnumTemplates.TempDescriptionLineUser))
                return true;
            return false;
        }

        public static Size MeasureString(string candidate, TextBlock textBlock)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }

        public static void LoadAddicionalProperties(UcmlObject objUCML, Canvas myCanvas, String template)
        {
            // Goes through canvas's list of children looking for a object as the same type and
            // set all new properties it's has to the new object
            var obj = getObjectByType(myCanvas, objUCML.TemplateName);
            if (obj.listNewProp.Count > 0)
            {
                foreach (var p in obj.listNewProp)
                {
                    var prop1 = new AdditionalProperty { name = p.name, value = "" };
                    objUCML.listNewProp.Add(prop1);
                }
            }
            else if (!template.Equals(""))
            {
                var list = getListPropertiestFromTemplate(char.ToLower(objUCML.TemplateName[0]) + objUCML.TemplateName.Substring(1), template);
                foreach (var p in list)
                {
                    var prop1 = new AdditionalProperty { name = p.name, value = "" };
                    objUCML.listNewProp.Add(prop1);
                }
            }
        }

        public static UcmlObject getObjectByType(Canvas myCanvas, String type)
        {
            foreach (var child in myCanvas.Children)
            {
                if (child.GetType().Name.Equals(type))
                {
                    return (UcmlObject)child;
                }
            }
            return new UcmlObject();
        }

        public static List<AdditionalProperty> getListPropertiestByType(Canvas myCanvas, String type)
        {
            foreach (var child in myCanvas.Children)
            {
                if (child.GetType().Name.Equals(type))
                {
                    return ((UcmlObject)child).listNewProp;
                }
            }
            return new UcmlObject().listNewProp;
        }

        public static List<AdditionalProperty> getListPropertiestFromTemplate(String type, String template)
        {
            var list = new List<AdditionalProperty>();

            XmlDocument xml = new XmlDocument();
            string text = System.IO.File.ReadAllText(@"C:\UCMLDiagrams\Templates\" + template + ".xml");
            xml.LoadXml(text.Trim());

            XmlNodeList xnList = xml.SelectNodes("/template/" + type + "/property");
            foreach (XmlNode xn in xnList)
            {
                list.Add(new AdditionalProperty { name = xn.InnerText, value = "" });
            }

            return list;
        }

        public static int contObjectsIsSelected(List<object> users)
        {
            int cont = 0;
            foreach (var item in users)
            {
                if ((item as UcmlObject).IsSelected)
                {
                    cont++;
                }
            }
            return cont;
        }

        public static SolidColorBrush getColorFromSelectedUser(List<object> users)
        {
            foreach (var item in users)
            {
                UcmlObject ucml = item as UcmlObject;
                if (ucml.IsSelected)
                {
                    return ucml.myColor;
                }
            }
            return null;
        }

        public static List<object> VerifyListObject(List<object> users, List<object> usersFromDestiny = null)
        {
            List<object> returnList = new List<object>();

            foreach (QuantityCircle item in users)
            {
                if (item.IsSelected)
                {
                    QuantityCircle qcAux = new QuantityCircle();
                    QuantityCircle aux = item;
                    qcAux.IsSelected = true;
                    qcAux.Percentage = aux.Percentage;
                    qcAux.isAbleUserSelection = aux.isAbleUserSelection;
                    qcAux.Description = aux.Description;
                    qcAux.Id = aux.Id;
                    qcAux.myColor = aux.myColor;

                    returnList.Add(qcAux);
                }
            }

            if (usersFromDestiny != null)
            {
                foreach (QuantityCircle item in returnList)
                {
                    foreach (QuantityCircle item2 in usersFromDestiny)
                    {
                        if (item.Id != item2.Id)
                        {
                            item.IsSelected = false;
                        }
                        else
                        {
                            item.IsSelected = item2.IsSelected;
                            break;
                        }
                    }
                }
            }

            if (returnList.Count == 1)
            {
                (returnList[0] as QuantityCircle).IsSelected = true;
            }

            return returnList;
        }

        public static bool HasSameColor(List<object> users)
        {
            bool hasSameColor = false;
            SolidColorBrush color = new SolidColorBrush();

            if (users.Count > 0)
            {
                color = (users[0] as UcmlObject).myColor;
            }

            foreach (var item in users)
            {
                if (color == (item as UcmlObject).myColor)
                {
                    hasSameColor = true;
                }
                else
                {
                    hasSameColor = false;
                }
            }

            return hasSameColor;
        }

        public static void setPercentage(UcmlObject currentObject)
        {
            double percentage = 0.0;

            foreach (UcmlObject item in currentObject.myUsers.Keys)
            {
                if (item.IsSelected)
                {
                    percentage += item.Percentage;
                }
            }
        }

        public static void ClearLinesFromObject(UcmlObject currentObject)
        {
            #region Valida Objetos de Destino

            // Check each type of object and validate each one in particular
            switch (currentObject.GetType().Name)
            {
                case EnumTemplates.TempQuantityCircle:

                    break;
                case EnumTemplates.TempBranch:
                    Branch auxBranch = currentObject as Branch;

                    foreach (Connection item in auxBranch.StartLines)
                    {

                    }

                    break;
                case EnumTemplates.TempCondition:

                    break;
                case EnumTemplates.TempDescriptionLineActivity:

                    break;
                case EnumTemplates.TempExitPath:

                    break;
                case EnumTemplates.TempMerge:

                    break;
                case EnumTemplates.TempOptionBox:

                    break;
                case EnumTemplates.TempSyncPoint:

                    break;
            }
            #endregion
        }

        public static UcmlObject FindObjectById(ref Canvas myCanvas, int id)
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
    }
}
