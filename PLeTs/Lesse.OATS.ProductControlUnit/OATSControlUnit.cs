using Coc.Data.Xmi;
using Lesse.Core.Interfaces;
using Lesse.Core.ProductControlUnit;
using Lesse.OATS.OATSScriptGenerator;
using Lesse.OATS.OgmaJParser;
using System;
using System.Collections.Generic;

namespace Lesse.OATS.OATSProductControlUnit
{
    public class OATSControlUnit : ProductControlUnit
    {
        private ScriptGenerator scriptExporter;
        private Parser scriptImporter;
        private ScriptGenerator excelExporter;
        private string filePath;

        public OATSControlUnit()
        {
            this.scriptExporter = new XmiToOATS();
            this.scriptImporter = new OgmaJ();
            this.excelExporter = new ParserToExcelOATS();
        }

        #region View related methods
        public override String GetImportFileFilter(Enum parserTypeSelected)
        {
            switch (parserTypeSelected)
            {
                case OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT:
                    return "Open Java OpenScript file (*.java) |*.java|All files (*.*)|*.*";
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    return "Open Astah XMI project file (*.xmi, *.xml) |*.xmi;*.xml|All files (*.*)|*.*";
            }
            throw new Exception("Invalid ParseType!");
        }

        public override String GetExportFileFilter(Enum parserTypeSelected)
        {
            switch (parserTypeSelected)
            {
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    return "Save Astah XMI project file (*.xml) |*.xml|All files (*.*)|*.*";
            }
            throw new Exception("Invalid ParseType!");
        }

        public override bool ExportIsFile(Enum parserTypeSelected)
        {
            switch (parserTypeSelected)
            {
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    return true;
                case OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT:
                case OATSSupportedFileTypes.OATS_EXCEL_SCRIPT:
                    return false;
            }

            throw new Exception("Invalid ParseType!");
        }

        public override Dictionary<Enum, string> GetExportOptions(Enum parserTypeSelected)
        {
            Dictionary<Enum, string> exportOptions = new Dictionary<Enum, string>();

            switch (parserTypeSelected)
            {
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    exportOptions.Add(OATSSupportedFileTypes.OATS_EXCEL_SCRIPT, "OATS Excel Script");
                    exportOptions.Add(OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT, "OATS OpenScript Script");
                    break;
                case OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT:
                    exportOptions.Add(OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG, "OATS Astah SeqDiag XMI");
                    exportOptions.Add(OATSSupportedFileTypes.OATS_EXCEL_SCRIPT, "OATS Excel Script");
                    break;
            }

            return exportOptions;
        }

        public override Dictionary<Enum, string> GetSequenceGeneratorOptions()
        {
            return null;
        }

        public override Dictionary<Enum, string> GetParserOptions()
        {
            Dictionary<Enum, string> parseOptions = new Dictionary<Enum, string>();
            parseOptions.Add(OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG, "OATS Astah SeqDiag XMI");
            parseOptions.Add(OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT, "OATS OpenScript Script");

            return parseOptions;
        }
        #endregion

        #region Action related methods
        public override void ParseFile(Enum parserTypeSelected, String path)
        {
            string name = "";
            this.filePath = path;

            switch (parserTypeSelected)
            {
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    base.ImportXmi(path, ref name, null);
                    break;
                case OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT:
                    base.listgeneralStructure.AddRange(scriptImporter.ParserMethod(path, ref name, null).listGeneralStructure);
                    break;
            }
        }

        public override void GenerateSequence(Enum generatorTypeSelected)
        {
            return;
        }

        public override void SaveResult(Enum exportType, string path)
        {
            switch (exportType)
            {
                case OATSSupportedFileTypes.OATS_ASTAH_SEQDIAG:
                    base.ExportXmi(path);
                    break;
                case OATSSupportedFileTypes.OATS_EXCEL_SCRIPT:
                    excelExporter.GenerateScript(base.listgeneralStructure, path);
                    break;
                case OATSSupportedFileTypes.OATS_OPENSCRIPT_SCRIPT:
                    scriptExporter.GenerateScript(base.listgeneralStructure, path);
                    break;
            }
        }

        public override void Validade(Enum parserTypeSelected)
        {
            return;
        }
        #endregion
    }

    #region SupportedFileTypes and SequenceGeneratorTypes ENUMs
    public enum OATSSupportedFileTypes
    {
        OATS_ASTAH_SEQDIAG,
        OATS_OPENSCRIPT_SCRIPT,
        OATS_EXCEL_SCRIPT
    }

    public enum OATSSequenceGeneratorTypes
    {
        NONE
    }
    #endregion
}
