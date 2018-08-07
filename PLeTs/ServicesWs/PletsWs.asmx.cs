using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Coc.Apps.PLeTs.ControlUnit;
using System.Xml;
using Coc.Data.ControlStructure;
using System.IO;
using SpreadsheetLight;
using System.Reflection;

namespace ServicesWs
{
    /// <summary>
    /// Summary description for PletsWs
    /// </summary>
   // [WebService(Namespace = "http://cepes.com/plets/")]

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PletsWs : System.Web.Services.WebService
    {
        private ControlUnit control;



        [WebMethod]
        public String LoadModelingStructure(String output, String parserType, String method, String caminho)
        {
            String aux = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] partialUrlPath = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
            String urlPath = partialUrlPath[0] + "//" + partialUrlPath[2] + "/";

            string projectPath = HttpRuntime.AppDomainAppPath;
            StreamWriter file = new StreamWriter(projectPath + "\\PCN.xml");
            file.WriteLine(output);
            file.Close();
            control = GetStructureType(method, ref control);
            control.LoadModelingStructure(projectPath + "\\PCN.xml", parserType);
            control = GenerateSequence(method);
            GenerateScript(projectPath);
            return urlPath + @"Plan.xlsx";

        }




        #region Public Methods


        private StructureType GetStructureType1(string parserType)
        {
            switch (parserType)
            {
                case "DFS":
                    return StructureType.DFS;

                case "HSI ":
                    return StructureType.HSI;

                case "Wp":
                    return StructureType.Wp;
                    break;

            }
            return StructureType.None;
        }

        private ControlUnit GetStructureType(string parserType, ref ControlUnit control)
        {
            switch (parserType)
            {
                case "DFS":
                    return control = new ControlUnit(StructureType.DFS);

                case "HSI ":
                    return control = new ControlUnit(StructureType.HSI);

                case "Wp":
                    return control = new ControlUnit(StructureType.Wp);
                    break;

            }
            return null;
        }
        // [WebMethod]
        public XmlDocument ExportParsedStructure()
        {
            // return control.ExportParsedStructure();
            return null;
        }
        //[WebMethod]
        //public List<KeyValuePair<String, Int32>> ValidateModel(String filename)
        //{
        //    return control.ValidateModel(filename);
        //}
        //  [WebMethod]
        public ControlUnit GenerateSequence(String type)
        {
            StructureType t = GetStructureType1(type);
            control.GenerateSequence(t);
            return control;
        }
        //  [WebMethod]
        public ControlUnit GenerateScript(String path)
        {
            control.GenerateScript(path);
            return control;
        }

        #endregion



    }
}
