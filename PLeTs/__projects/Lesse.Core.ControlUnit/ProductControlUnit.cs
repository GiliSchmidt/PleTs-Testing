using Coc.Data.Xmi;
using Lesse.Core.ControlAndConversionStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lesse.Core.ProductControlUnit
{

    public abstract class ProductControlUnit
    {
        protected StructureCollection structureCollection;
        protected List<GeneralUseStructure> listgeneralStructure;
        private XmiImporter xmiImporter;
        private XmiExporter xmiExporter;
        private int TestCaseCount;

        public ProductControlUnit()
        {
            this.xmiImporter = new XmiImporter();
            this.xmiExporter = new XmiExporter();
            this.structureCollection = new StructureCollection();
            this.listgeneralStructure = new List<GeneralUseStructure>();
            this.TestCaseCount = 0;
        }

        #region View related methods
        public abstract Dictionary<Enum, string> GetExportOptions(Enum parserTypeSelected);

        public abstract Dictionary<Enum, string> GetSequenceGeneratorOptions( );

        public abstract Dictionary<Enum, string> GetParserOptions();

        public abstract String GetImportFileFilter(Enum parserTypeSelected);

        public abstract String GetExportFileFilter(Enum parserTypeSelected);

        public abstract bool ExportIsFile(Enum parseparserTypeSelected);
        #endregion

        #region Action related methods
        public abstract void ParseFile(Enum parserTypeSelected, String path);

        public abstract void GenerateSequence(Enum generatorTypeSelected);

        public abstract void Validade(Enum parserTypeSelected);

        public abstract void SaveResult(Enum parserTypeSelected, string path);
        #endregion

        #region Protected methods (used by subclasses)
        protected void ExportXmi(String path)
        {
            XmlDocument doc = xmiExporter.ToXmi(listgeneralStructure);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            settings.CheckCharacters = true;

            using (XmlWriter writer = XmlWriter.Create(path, settings))
                doc.Save(writer);
        }

        protected void ImportXmi(String path, ref String name, Tuple<String, Object>[] args)
        {
            StructureCollection listUmlModels = xmiImporter.ParserMethod(path, ref name, args);

            ResetAttributes();

            listgeneralStructure.AddRange(listUmlModels.listGeneralStructure);
        }

        #endregion

        #region Private methods
        private void ResetAttributes()
        {
            TestCaseCount = 0;
            listgeneralStructure.Clear();
        }
        #endregion
    }
}

