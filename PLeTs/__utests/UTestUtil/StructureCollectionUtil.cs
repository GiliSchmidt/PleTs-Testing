
using Coc.Data.ControlAndConversionStructures;
using Coc.Data.ControlStructure;
using Coc.Data.ConversionUnit;
using Coc.Data.Xmi;
using OgmaJOATSParser;
using System;
using System.Collections.Generic;

namespace UTestUtil
{
    public class StructureCollectionUtil
    {
        public static List<GeneralUseStructure> GetStructureCollectionActOats()
        {
            try
            {
                String name, path;
                OgmaJ parser;

                name = "test";
                path = "..\\..\\..\\TestFiles\\java oats script.java";
                parser = new OgmaJ();

                return parser.ParserMethod(path, ref name, null).listGeneralStructure;
            }
            catch (Exception e)
            {
                throw new Exception("There's a problem with OgmaJ parser: " + e.Message);
            }
        }

        public static List<GeneralUseStructure> GetStructureCollectionSeqJUnit()
        {
            try
            {
                String path, name;
                SequenceDiagramImporter importer;

                name = "test";
                path = "..\\..\\..\\TestFiles\\tcc edemar junit -sequence diag - astah.xml";
                importer = new SequenceDiagramImporter();

                return importer.ParserMethod(path, ref name, null).listGeneralStructure;
            }
            catch (Exception e)
            {
                throw new Exception("There's a problem with SequenceDiagramImporter: " + e.Message);
            }
        }

        public static List<GeneralUseStructure> GetFSMUmlActOats()
        {
            try
            {
                List<GeneralUseStructure> listModel;
                StructureType type;
                UmlToFsm converter;

                listModel = new List<GeneralUseStructure>();
                listModel = GetStructureCollectionActOats();

                converter = new UmlToFsm();

                //this parameter is not used
                type = StructureType.OATS;

                return converter.Converter(listModel, type);
            }
            catch (Exception e)
            {
                throw new Exception("There's a problem with UmlToFsm: " + e.Message);
            }
        }    }
}
